using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Refactorer
{
    public static class Parser
    {
        public static List<string> SplitOnLines(string text)
        {
            return text.Split('\n').ToList();
        }

        public static string ConnectLines(List<string> lines)
        {
            string text = string.Empty;
            foreach (var line in lines)
            {
                text += line + "\n";
            }
            text = text.Remove(text.Length-1, 1);
            return text;
        }

        // Повертає рядок, де треба вставити оголошення константи "const int NAME = 10;"
        public static int FindPositionForLocalConstantDeclaration(List<string> lines, int rowConst)
        {
            string keyWord = "class";
            string keyWord2 = "#include";
            int position = 0;
            for (int i = rowConst; i >= 0; i--)
            {
                position = i;
                if (lines[i].Contains(keyWord))
                {
                    int index = lines[i].IndexOf(keyWord, StringComparison.Ordinal);
                    if (Char.IsWhiteSpace(lines[i][index+keyWord.Length]))
                    {
                        if (index !=0)
                        {
                            if (Char.IsWhiteSpace(lines[i][index - 1]))
                            {
                                if (!lines[i].Contains("{"))
                                    position++;
                                return position;
                            }
                        }
                        else
                        {
                            if (!lines[i].Contains("{"))
                                position++;
                            return position;
                        }
                            
                    }
                }

                if (lines[i].Contains(keyWord2))
                {
                    position = i + 1;
                    return position;
                }
            }
            position = 0; 
            return position;
        }

        public static List<string> GetFunctionBody(int row, List<string> lines)
        {
            int openCount = 0;
            int closeCount = 0;
            StringBuilder functionBody = new StringBuilder();


            for (int i = row+1; i < lines.Count; i++)
            {
                openCount += lines[i].Count(c => c == '{');
                closeCount += lines[i].Count(c => c == '}');

                // Додаємо рядок до тіла функції
                functionBody.AppendLine(lines[i] + '\n');

                // Якщо кількість відкриваючих та закриваючих дужок зрівнялася
                if (openCount == closeCount)
                {
                    // Повертаємо тіло функції у вигляді списку рядків
                    return SplitOnLines(functionBody.ToString());
                }
            }
            // Якщо кількість відкриваючих дужок більше за кількість закриваючих
            if (openCount > closeCount)
                throw new Exception("Error: Unbalanced curly braces");

            // Якщо кількість відкриваючих та закриваючих дужок не зрівнялася, повертаємо порожній рядок або null
            return new List<string>();
        }
        
        public static List<string> RemoveComments(List<string> lines)
        {
            var newLines = DeleteLineComments(lines);
            newLines = DeleteMultiLineComments(newLines);
            return newLines;
        }

        private static List<string> DeleteMultiLineComments(List<string> lines)
        {
            bool isComment = false;
            List<string> linesNoComments = new List<string>();
            foreach (var line in lines)
            {
                string newLine = string.Empty;
                for (int i = 1; i < line.Length; i++)
                {
                    char[] arr = new char[] { line[i - 1], line[i] };
                    
                    if (arr[0] == '/' && arr[1] == '*') 
                        isComment = true;

                    if (!isComment)
                    {
                        newLine += arr[0];
                        if (i == line.Length - 1)
                            newLine += arr[1];
                    }

                    if (arr[0] == '*' && arr[1] == '/')
                        isComment = false;
                }
                linesNoComments.Add(newLine);
            }
            return linesNoComments;
        }

        private static List<string> DeleteLineComments(List<string> lines)
        {
            bool isPrevIsDash = false, isStringConstant = false, isAdded = false;
            List<string> linesNoComments = new List<string>();

            foreach (var line in lines)
            {
                isAdded = false;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '"')
                    {
                        if (isStringConstant) isStringConstant = false;
                        else isStringConstant = true;
                    }

                    if (!isStringConstant && isPrevIsDash && line[i] == '/')
                    {
                        linesNoComments.Add(line.Remove(i - 1, line.Length - i + 1));
                        isAdded = true;
                        isPrevIsDash = false;
                        break;
                    }

                    if (line[i] == '/') isPrevIsDash = true;
                    else isPrevIsDash = false;
                }
                if (!isAdded) linesNoComments.Add(line);
            }
            return linesNoComments;
        }

        public static bool IsComment(List<string> lines, int row, int coln)
        {
            bool isMultiLineComment = false;
            for (int i = row; i >= 0; i--)
            {
                int closeIndex = lines[i].IndexOf("*/");
                int openIndex = lines[i].IndexOf("/*");
                if(i == row)
                {
                    if (openIndex != -1 && openIndex < coln && (closeIndex < 0 || closeIndex > coln)) // /* point .... || /* point */ 
                        isMultiLineComment = true;
                    if (closeIndex < coln && (openIndex > coln || openIndex == -1)) // */ point /* || */ point ....
                        isMultiLineComment = false;

                    int lineCommentIndex = lines[i].IndexOf("//");
                    if ((lineCommentIndex != -1 && lineCommentIndex < coln) || isMultiLineComment) // // text || multiline
                        return true;
                }
                else
                {
                    if (closeIndex != -1 && openIndex == -1) // ... */ point
                        return false;
                    if(openIndex != -1 && closeIndex == -1) // /*... point
                        return true;

                    if (openIndex != -1 && closeIndex != -1)
                    {
                        if (openIndex < closeIndex)  // /* ... */ ... point
                            return false;
                        else if (openIndex > closeIndex) // */ ... /* ... point
                            return true;
                    }
                }
            }
            return false;
        }

        public static bool IsStringConst(List<string> funcBody, int i, int index)
        {
            var pattern = "\"[^\"]*\"";
            MatchCollection matches = Regex.Matches(funcBody[i], pattern);
            foreach(Match match in matches)
            {
                if (match.Index <= index && index <= (match.Index + match.Length))
                    return true;
            }
            return false;
        }

        public static bool IsReservedWord(string str)
        {
            // List of C++ reserved words
            List<string> reservedWords = new List<string>
            {
                "asm", "auto", "bool", "break", "case", "catch", "char", "class", "const",
                "const_cast", "continue", "default", "delete", "do", "double", "dynamic_cast",
                "else", "enum", "explicit", "export", "extern", "false", "float", "for", "friend",
                "goto", "if", "inline", "int", "long", "mutable", "namespace", "new", "operator",
                "private", "protected", "public", "register", "reinterpret_cast", "return", "short",
                "signed", "sizeof", "static", "static_cast", "struct", "switch", "template", "this",
                "throw", "true", "try", "typedef", "typeid", "typename", "union", "unsigned", "using",
                "virtual", "void", "volatile", "wchar_t", "while"
            };

            return reservedWords.Contains(str);
        }

        public static bool IsSeparator(char ch)
        {
            char[] separators = { ' ', ',', ';', '.', '(', ')', '{', '}', '[', ']', '<', '>', '=', '+', '-', '*', '/', '%', '&', '|', '^', '!', '?', ':', '\t', '\n', '\r' };
            //var separators = new char[] { ' ', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']', '\t', '\r' };
            return separators.Contains(ch);
        }
    }
}
