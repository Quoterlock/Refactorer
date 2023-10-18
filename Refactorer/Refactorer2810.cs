using Refactorer.Exceptions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            
            lines = AddConstDeclaration(lines, constantValue, constantName, constants.First().Key);

            foreach(KeyValuePair<int, List<int>> constantPos in constants)
            {
                //lines = ReplaceConst(lines, constantPos, constantValue, constantName); constat pos => KeyValuePair<int, List<int>>
            }

            return Parser.ConnectLines(lines);
        }

        public static string RenameMethod(string oldName, string newName, string className, string text)
        {
            // (!) Не враховує класи. Просто переіменовує за назвою.
            // (!) Не враховує коментарі (також буде замінювати у рядкових константах)
            // треба це допрацювати
            var resultLines = new List<string>();

            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var indexes = FindAllInLine(line, oldName + "(");

                foreach (var index in indexes)
                {
                    if (index == -1)
                    {
                        resultLines.Add(line);
                    }
                    else if (IsPreviousCharIsSeparator(line, index))
                    {
                        string part1 = line.Substring(0, index);
                        string part3 = line.Substring(index + oldName.Length);
                        resultLines.Add(part1 + newName + part3);
                    }
                }
            }

            string result = string.Empty;
            foreach (var line in resultLines)
            {
                result += line + "\n";
            }
            return result;
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
                        lines[header.RowInText] = ConvertToStringHeader(header); // replace header with new one
                    }
                }
            }
            return Parser.ConnectLines(lines);
        }

        // =============== LOW LVL ====================
        private static List<FunctionHeader> FindFunctionHeaders(List<string> lines)
        {
            /*
            string codeLine = "int add(int a, int b);";

            string pattern = @"(\w+)\s+(\w+)\s*\(([^)]*)\);";

            Match match = Regex.Match(codeLine, pattern);

            if (match.Success)
            {
                string returnType = match.Groups[1].Value;
                string functionName = match.Groups[2].Value;
                string parameters = match.Groups[3].Value;

                Console.WriteLine("Return Type: " + returnType);
                Console.WriteLine("Function Name: " + functionName);
                Console.WriteLine("Parameters: " + parameters);
            }
            else
            {
                Console.WriteLine("No function header found in the line.");
            }
            */
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

        private static string ConvertToStringHeader(FunctionHeader header)
        {
            // from params
            string parameters = string.Empty;
            foreach (var param in header.Parameters)
                parameters += param.Value + " " + param.Key + ",";
            parameters = parameters.Substring(0, parameters.Length - 1); // remove last coma.

            return header.ReturnValue + " " + header.Name + "(" + parameters + ")\n";
        }

        private static List<string> ReplaceConst(List<string> lines, int constantPos, string constantValue, string constantName)
        {
            throw new NotImplementedException();
        }

        public static List<string> AddConstDeclaration(List<string> lines, string constantValue, string constantName, int firstConstIndex)
        {
            var position = Parser.FindPositionForLocalConstantDeclaration(lines, firstConstIndex);
            if (int.TryParse(constantValue, out int intValue))
            {
                string declaration = "const int " + constantName + " = " + intValue + ";";
                lines.Insert(position, declaration);
            }
            else if (double.TryParse(constantValue, out double doubleValue))
            {
                string declaration = "const double " + constantName + " = " + doubleValue + ";";
                lines.Insert(position, declaration);
            }
            else
            {
                string declaration = "const string " + constantName + " = " + constantValue + ";";
            }
            return lines;
        }

        public static Dictionary<int, List<int>> FindAllConstantPositions(List<string> lines, string constantValue)
        {
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
        private static int FindPositionFor(List<string> lines, int rowNumber, string constantValue)
        {
            throw new NotImplementedException();
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
            var separators = new char[] { ' ', '.', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']' };
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


/*public static string ExtractConstant(string text)
{
    const string SymbolicConstant = "Symbolic_constant";
    return SymbolicConstant;
}

public static string ReplaceMagicNumberWithSymbolicConstant(string text, string magicNumber, string symbolicConstant)
{
    // Замінюємо всі входження магічного числа на символічну константу у тексті
    string replacedText = text.Replace(magicNumber, symbolicConstant);
    return replacedText;
}*/