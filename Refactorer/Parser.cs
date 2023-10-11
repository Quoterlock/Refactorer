using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return text;
        }


        public static FunctionHeader GetHeader(string stringHeader)
        {
            var func = new FunctionHeader();
            // get all info
            return func;
        }

        // Повертає рядок, де треба вставити оголошення константи "const int NAME = 10;"
        private static int FindPositionForLocalConstantDeclaration(List<string> lines, int row)
        {
            int position = 0;
            for (int i = row; i >= 0; i--)
            {
                if (lines[i].Contains("class"))
                {
                    position = i + 2;
                    return position;
                }
            }
            // if there is no classes
            position = 0; 
            return position;
        }

        internal static List<string> GetFunctionBody(FunctionHeader header, List<string> lines)
        {
            throw new NotImplementedException();
        }
    }
}
