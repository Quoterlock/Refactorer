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

        public static string RemoveStringConstant(string line)
        {
            var pattern = "\"[^\"]*\"";
            return Regex.Replace(line, pattern, string.Empty);
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
    }
}
