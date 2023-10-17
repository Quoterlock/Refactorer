﻿using Refactorer.Exceptions;
using System;
using System.Collections.Generic;
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
            if (className == null || className == string.Empty)
            { //....
            }

            // OldName + ( => NewName + ( // codeLine.Replace(OldName, NewName, text)

            // in one line (OldName() + OldName()) => just divide each line on list of words

            // example -> "NameOldName( // check in is " " before OldName (or another symbol
            // that can't be a part of method name"/", ":", "(")=> " OldName(";

            return string.Empty;




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
            throw new NotImplementedException();
        }

        private static bool ParamIsUsed(KeyValuePair<string, string> param, List<string> funcBody)
        {
            //!!!! БЕЗ УРАХУВАННЯ КОМЕНТАРІВ
            string paramName = param.Key;
            var separators = new char[] { ' ', '=', '+', '-', '*', '/', '(', ')', '{', '}', ';', '[', ']' };
            bool isUsed = false;

            foreach (var line in funcBody)
            {
                string tmpLine = RemoveStringConst(line);
                List<string> words = tmpLine.Split(separators).ToList();

                foreach (var word in words)
                    if (word == paramName || word.Contains(paramName + "."))
                        isUsed = true;
            }
            return isUsed;
        }

        private static string RemoveStringConst(string line)
        {
            var pattern = "\"[^\"]*\"";
            return Regex.Replace(line, pattern, string.Empty);
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