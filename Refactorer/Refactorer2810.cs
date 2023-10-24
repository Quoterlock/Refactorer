using Refactorer.Exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Refactorer
{
    public static class Refactorer2810
    {
        /*
            Користувач виділяж назву методу, натискає праву кнопку і обирає Rename
            виділений текст одразу вводиться в поле зі старою назвою,
            Користувач вводить нову назву в іншому полі та обирає назву класу (опціонально)
         */
        public static string ExtractConstant(string constantValue, string constantName, int rowNumber, string text, bool extractAll)
        {
            Dictionary<int,List<int>> constants = new Dictionary<int, List<int>>();
            var lines = Parser.SplitOnLines(text);

            if (!extractAll)
                constants.Add(rowNumber, FindConstPosition(lines[rowNumber], constantValue));
            else
                constants = FindAllConstantPositions(lines, constantValue);

            lines = ReplaceConst(lines, constants, constantValue, constantName); // constat pos => KeyValuePair<int, List<int>>

            lines = AddConstDeclaration(lines, constantValue, constantName, constants.First().Key);

            return Parser.ConnectLines(lines);
        }

        public static string RenameMethod(string oldName, string newName, string className, string text)
        {
            // (!) Не враховує класи. Просто переіменовує за назвою.
            // (!) Не враховує коментарі (також буде замінювати у рядкових константах)
            // треба це допрацювати
            var resultLines = new List<string>();

            var lines = Parser.SplitOnLines(text);
            foreach (var line in lines)
            {
                var indexes = FindAllInLine(line, oldName + "(");
                string newLine = line;
                foreach (var index in indexes)
                {
                    if (IsPreviousCharIsSeparator(line, index))
                    {
                        string part1 = newLine.Substring(0, index);
                        string part3 = newLine.Substring(index + oldName.Length);
                        newLine = part1 + newName + part3;
                    }
                }
                resultLines.Add(newLine);
            }

            return Parser.ConnectLines(resultLines);
        }

        public static string RemoveUnusedParameters(string text)
        {
            var lines = Parser.SplitOnLines(text);

            List<FunctionHeader> functionHeaders = FindFunctionHeaders(lines);

            foreach (var header in functionHeaders)
            {
                List<string> funcBody = Parser.GetFunctionBody(header, lines);

                foreach (var param in header.Parameters) 
                {
                    if (!ParamIsUsed(param, funcBody))
                    {
                        header.Parameters.Remove(param.Key); // Key -> назва параметра, value - тип (int/float...)
                        lines[header.RowInText] = Parser.ConvertToStringHeader(header); // replace header with new one
                    }
                }
            }
            return Parser.ConnectLines(lines);
        }

        // =============== LOW LVL ====================
        private static List<FunctionHeader> FindFunctionHeaders(List<string> lines)
        {
            var headers = new List<FunctionHeader>();
            string pattern = @"(\w+)\s+(\w+)\s*\(([^)]*)\);";

            foreach(var line in lines)
            {
                Match match = Regex.Match(line, pattern);
                if (match.Success)
                {
                    var header = new FunctionHeader();
                    header.ReturnValue = match.Groups[1].Value;
                    header.Name = match.Groups[2].Value;

                    //TODO: add params correctly 
                    //string parameters = match.Groups[3].Value;
                    headers.Add(header);
                }
            }
            return headers;            
        }

        private static bool ParamIsUsed(KeyValuePair<string, string> parameter, List<string> funcBody)
        {
            bool isUsed = false;
            bool isMultilineComment = false;
            foreach(var line in funcBody)
            {
                // це погано використовувати ref, але поки так.
                isUsed |= IsParamUsedInLine(parameter.Key, line, ref isMultilineComment);
            }
            return isUsed;
        }
        
        private static bool IsParamUsedInLine(string parameterName, string line, ref bool isMultiLineComment)
        {
            bool isUsed = false, isLineComment = false;

            line = Parser.RemoveStringConstant(line);
            
            var words = line.Split(' ', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']').ToList();

            foreach(var word in words)
            {
                isLineComment |= word.Contains("//");
                if (word.Contains("/*")) isMultiLineComment = true;
                else if (word.Contains("*/")) isMultiLineComment = false;

                var subWords = word.Split('*', '/');

                // TODO: check the situation when "TextHere_paramName.DoSomething()"
                // or "TextHere.paramName"
                // Напевно треба зробити все через індекси
                foreach(var subWord in subWords)
                    if(subWord == parameterName || subWord.Contains(parameterName + "."))
                        if(!isLineComment && !isMultiLineComment)
                            isUsed = true;
            }

            return isUsed;
        }

        private static List<string> ReplaceConst(List<string> lines, Dictionary<int, List<int>> constants, string constantValue, string constantName)
        {
            foreach(var constantInLine in constants)
            {
                constantInLine.Value.Reverse();
                foreach (var constantIndex in constantInLine.Value)
                {
                    int startIndex = constantIndex, endIndex = constantValue.Length;
                    if (isStringConstant(lines[constantInLine.Key], constantValue, constantIndex))
                    {
                        startIndex = constantIndex - 1; endIndex = constantValue.Length + 2;
                    }

                    lines[constantInLine.Key] = lines[constantInLine.Key].Remove(startIndex, endIndex);
                    lines[constantInLine.Key] = lines[constantInLine.Key].Insert(startIndex, constantName);
                }
            }
            return lines;
        }

        private static bool isStringConstant(string line, string constantValue, int constantIndex)
        {
            return constantIndex > 0 && line[constantIndex-1].Equals('"') && line[constantIndex + constantValue.Length].Equals('\"');
        }

        public static List<string> AddConstDeclaration(List<string> lines, string constantValue, string constantName, int index)
        {
            List<string> updatedLines = new List<string>(lines);

            // Знаходимо позицію для вставки оголошення константи, використовуючи метод з класу Parser
            var position = Parser.FindPositionForLocalConstantDeclaration(updatedLines, index);
            string constantType = string.Empty;

            // Спробуємо визначити тип константи за допомогою TryParse
            if (TryParseConstantType(constantValue, out string inferredType))
            {
                constantType = inferredType;
                if (inferredType.Equals("string")) constantValue = '"' + constantValue + '"';
                if (inferredType.Equals("char")) constantValue = "'" + constantValue + "'";
            }

            // Формуємо оголошення константи
            string declaration;
            if (position > 0)
            {
                declaration = $"\nconst {constantType} {constantName} = {constantValue};\r";
                updatedLines[position] += declaration;
            }
            else
            {
                declaration = $"const {constantType} {constantName} = {constantValue};\r\n";
                updatedLines[position] = updatedLines[position].Insert(0, declaration);
            }
            return updatedLines;
        }

        private static bool TryParseConstantType(string value, out string type)
        {
            if (int.TryParse(value, out _)) type = "int";
            else if (double.TryParse(value, out _)) type = "double";
            else if (float.TryParse(value, out _)) type = "float";
            else if (bool.TryParse(value, out _)) type = "bool";
            else if (char.TryParse(value, out _)) type = "char";
            else type = "string";
            return true;
        }

        public static Dictionary<int, List<int>> FindAllConstantPositions(List<string> linesInput, string constantValue)
        {
            var lines = new List<string>(linesInput);
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();
            bool isMultiCommented = false;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("//"))
                {
                    int index = lines[i].IndexOf("//", StringComparison.Ordinal);
                    lines[i] = lines[i].Remove(index);
                }
                List<int> indexList = new List<int>();
                int offsetValue = 0;
                if (lines[i].Contains("*/"))
                {
                    isMultiCommented = false;
                }
                if (!isMultiCommented )
                {
                    if (lines[i].Contains("/*") && !lines[i].Contains("*/"))
                    {
                        isMultiCommented = true;
                    }

                    while (lines[i].Contains(constantValue))
                    {
                        int firstCommentIndex = lines[i].IndexOf("/*", StringComparison.Ordinal);
                        int secondCommentIndex = lines[i].IndexOf("*/", StringComparison.Ordinal);
                        int index = lines[i].IndexOf(constantValue, StringComparison.Ordinal);
                        if (!Char.IsLetterOrDigit(lines[i][index - 1]) &&
                            !Char.IsLetterOrDigit(lines[i][index + constantValue.Length]))
                        {
                            if (isConstantInMultilineComment(lines[i],index))
                            {
                                indexList.Add(index + constantValue.Length * offsetValue);
                            }
                        }
                        lines[i] = lines[i].Remove(index, constantValue.Length);
                        offsetValue++;
                    }
                }
                if(indexList.Count!=0)
                {
                    result.Add(i,indexList);
                }
            }

            if (result.Count == 0)
                return null;
            return result;
        }

        private static bool isConstantInMultilineComment(string line, int index)
        {
            int firstCommentIndex = line.IndexOf("/*", StringComparison.Ordinal);
            int secondCommentIndex = line.IndexOf("*/", StringComparison.Ordinal);
            if ((firstCommentIndex == -1 && secondCommentIndex == -1))
            {
                return true;
            }

            else if ((firstCommentIndex != -1 && secondCommentIndex != -1))
            {
                if (index < firstCommentIndex || index > secondCommentIndex)
                {
                    return true;
                }
            }
            else if (firstCommentIndex != -1)
            {
                if (index < firstCommentIndex)
                {
                    return true;
                }
            }
            else if (secondCommentIndex != -1)
            {
                if (index > secondCommentIndex)
                {
                    return true;
                }
            }

            return false;
        }

        public static List<int> FindConstPosition(string line, string constantValue)
        {
            List<int> indexList = new List<int>();
            int offsetValue = 0;
            if (line.Contains("//"))
            {
                int index = line.IndexOf("//", StringComparison.Ordinal);
                line = line.Remove(index);
                
            }
            while (line.Contains(constantValue))
            {
                int index = line.IndexOf(constantValue, StringComparison.Ordinal);
                if (index != 0 && !Char.IsLetterOrDigit(line[index - 1]) &&
                    !Char.IsLetterOrDigit(line[index + constantValue.Length]))
                {
                    if (isConstantInMultilineComment(line,index))
                    {
                        indexList.Add(index + constantValue.Length * offsetValue);
                    }
                }
                line = line.Remove(index, constantValue.Length);
                offsetValue++;
            }

            return indexList;
        }

        private static List<int> FindAllInLine(string line, string str)
        {
            var indexes = new List<int>();
            int currentIndex = line.IndexOf(str);

            while (currentIndex >= 0)
            {
                indexes.Add(currentIndex);
                currentIndex = line.IndexOf(str, currentIndex + str.Length);
            }
            return indexes;
        }

        private static bool IsPreviousCharIsSeparator(string line, int index)
        {
            if (index - 1 < 0) return true;
            var prevChar = line.ElementAt(index - 1);
            var separators = new char[] { ' ', '.', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']','\t','\r' };
            return separators.Contains(prevChar);
        }

        private static bool IsNextCharIsSeparator(string line, int index)
        {
            if (index + 1 <= line.Length) return true;
            var nextChar = line.ElementAt(index + 1);
            var separators = new char[] { ' ', '.', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']' };
            return separators.Contains(nextChar);
        }
    }
}
