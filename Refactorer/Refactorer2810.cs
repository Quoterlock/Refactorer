using Refactorer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            List<int> constants = new List<int>();
            var lines = Parser.SplitOnLines(text);

            if (!extractAll)
                constants.Add(FindConstPosition(lines, rowNumber, constantValue));
            else
                constants.AddRange(FindAllConstantPositions(lines, constantValue));

            lines = AddConstDeclaration(lines, constantValue, constantName);

            foreach (var constantPos in constants)
                lines = ReplaceConst(lines, constantPos, constantValue, constantName);

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
            throw new NotImplementedException();
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

        private static List<string> AddConstDeclaration(List<string> lines, string constantValue, string constantName)
        {
            /*
            var position = Parser.FindPositionForLocalConstant(text, row);
            string type = GetType(value);
            string spaces;
            foreach (int row in position.Coln)
                spaces += " ";
            string declaration = "\n" + tab + "const " + type + " " + name + " = " + value + ";\n";
            return Insert(position, declaration);
            */
            return null;
        }

        private static IEnumerable<int> FindAllConstantPositions(List<string> lines, string constantValue)
        {
            throw new NotImplementedException();
        }

        private static int FindConstPosition(List<string> lines, int rowNumber, string constantValue)
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