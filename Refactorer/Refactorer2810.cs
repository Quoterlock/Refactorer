using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Refactorer
{
    public static class Refactorer2810
    {
        public static string ExtractConstant(int constantValue, string constantName, string text)
        {
            return string.Empty;
        }

        public static string RenameMethod(string oldName, string newName, string text)
        {
            // OldName + ( => NewName + ( // codeLine.Replace(OldName, NewName, text)

            // in one line (OldName() + OldName()) => just divide each line on list of words

            // example -> "NameOldName( // check in is " " before OldName (or another symbol
            // that can't be a part of method name"/", ":", "(")=> " OldName(";

            return string.Empty;
        }

        public static string RemoveUnusedParameters(string text)
        {
            return string.Empty;
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