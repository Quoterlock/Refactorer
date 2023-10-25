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
using System.Xml.Linq;

namespace Refactorer
{
    public static class Refactorer2810
    {
        public static string ExtractConstant(string constantValue, string constantName, int rowNumber, string text, bool extractAll)
        {
            if (Parser.IsReservedWord(constantName) || IsExist(constantName, text))
                throw new NameAlreadyExistException(constantName);

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
            if (Parser.IsReservedWord(newName) || IsMethodExist(newName + "(", text))
                throw new NameAlreadyExistException(newName);


            // TODO: щось зробити з класами

            var lines = Parser.SplitOnLines(text);

            for(int i = 0; i < lines.Count; i++)
            {
                var indexes = FindAllInLine(lines[i], oldName + "(");
                string newLine = lines[i];
                foreach (var index in indexes)
                {
                    if (!Parser.IsComment(lines, i, index) && !Parser.IsStringConst(lines, i, index))
                        if (index == 0 || Parser.IsSeparator(lines[i][index-1]))
                            newLine = ReplaceByIndex(index, oldName.Length, newName, newLine);
                }
                lines[i] = newLine;
            }
            return Parser.ConnectLines(lines);
        }

        public static string RemoveUnusedParameters(string text)
        {
            var lines = Parser.SplitOnLines(text);

            List<FunctionHeader> functionHeaders = FindFunctionHeaders(lines);

            foreach (var header in functionHeaders)
            {
                List<string> funcBody = Parser.GetFunctionBody(header.RowInText, lines);
                var parameters = new Dictionary<string, string>(header.Parameters);
                foreach (var param in parameters) 
                {
                    if (!ParamIsUsed(param.Key, funcBody))
                    {
                        header.Parameters.Remove(param.Key); // Key -> назва параметра, value - тип (int/float...)
                        lines[header.RowInText] = header.ToString() + '\r'; // replace header with new one
                    }
                }
            }
            return Parser.ConnectLines(lines);
        }

        // =============== LOW LVL ====================
        private static List<FunctionHeader> FindFunctionHeaders(List<string> lines)
        {
            var headers = new List<FunctionHeader>();
            string pattern = @"(\w+)\s+(\w+)\s*\(([^)]*)\)";

            for(int i = 0; i < lines.Count; i++)
            {
                Match match = Regex.Match(lines[i], pattern);
                if (match.Success && !Parser.IsComment(lines, i, 0))
                {
                    var header = new FunctionHeader();
                    header = header.Convert(lines[i], i);
                    headers.Add(header);
                }
            }
            return headers;            
        }

        private static bool ParamIsUsed(string parameter, List<string> funcBody)
        {
            for (int i = 0; i < funcBody.Count; i++)
            {
                List<int> indexes = FindAllInLine(funcBody[i], parameter);

                foreach (var index in indexes)
                {
                    if (!Parser.IsComment(funcBody, i, index) && !Parser.IsStringConst(funcBody, i, index))
                    {
                        char prev = funcBody[i][index - 1];
                        char next = funcBody[i][index + parameter.Length];

                        //if() // Test var; -> Test var;
                        int curIndex = index + parameter.Length;
                        while(funcBody[i][curIndex].Equals(' '))
                        {
                            curIndex++;
                        }
                        if(!Char.IsLetterOrDigit(funcBody[i][curIndex]))
                        {
                            if (Char.IsSeparator(prev) && !prev.Equals('.') && Char.IsSeparator(next) && !next.Equals('('))
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        private static List<string> ReplaceConst(List<string> lines, Dictionary<int, List<int>> constants, string constantValue, string constantName)
        {
            foreach(var constantInLine in constants)
            {
                constantInLine.Value.Reverse();
                foreach (var constantIndex in constantInLine.Value)
                {
                    int startIndex = constantIndex, count = constantValue.Length;
                    if (isStringConstant(lines[constantInLine.Key], constantValue, constantIndex))
                    {
                        startIndex = constantIndex - 1; count = constantValue.Length + 2;
                    }

                    lines[constantInLine.Key] = ReplaceByIndex(startIndex, count, constantName, lines[constantInLine.Key]);
                }
            }
            return lines;
        }

        private static string ReplaceByIndex(int startIndex, int count, string value, string str)
        {
            str = str.Remove(startIndex, count);
            str = str.Insert(startIndex, value);
            return str;
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
                if (inferredType.Equals("string"))
                {
                    if (!constantValue[0].Equals('"')) constantValue = constantValue.Insert(0, "\"");
                    if (!constantValue[constantValue.Length-1].Equals('"')) constantValue += "\"";
                }
                if (inferredType.Equals("char"))
                {
                    if (!constantValue[0].Equals('\'')) constantValue = constantValue.Insert(0, "\'");
                    if (!constantValue[constantValue.Length - 1].Equals('\'')) constantValue += "\'";
                }
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

        public static bool IsMethodExist(string name, string text)
        {
            var result = FindAllInLine(text, name + "(");
            if (result != null && result.Count > 0)
            {
                foreach (var itemIndex in result)
                {
                    if (Char.IsSeparator(text[itemIndex - 1]))
                        return true;
                }
            }
            return false;
        }
        private static bool IsExist(string name, string text)
        {
            var result = FindAllInLine(text, name);
            if (result != null && result.Count > 0)
            {
                foreach (var itemIndex in result)
                {
                    if (Char.IsSeparator(text[itemIndex - 1]) && Char.IsSeparator(text[itemIndex + name.Length]))
                        return true;
                }
            }
            return false;
        }
    }
}
