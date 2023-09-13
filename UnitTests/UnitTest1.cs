using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RenameMethodTest1()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText =
                "class Name\r\n{\r\n\tpublic:\r\n" +
                "\tName()\r\n\t{\r\n\t\tOldName();\r\n" +
                "\t}\r\n\tvoid OldName()\r\n\t{\r\n\t}\r\n}";
            string expectedResult = 
                "class Name\r\n{\r\n\tpublic:\r\n" +
                "\tName()\r\n\t{\r\n\t\tNewName();\r\n" +
                "\t}\r\n\tvoid NewName()\r\n\t{\r\n\t}\r\n}";

            var result = Refactorer2810.RenameMethod(oldName, newName, inputText);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestExtractConstant_ReplacesMagicNumberWithConstant()
        {
            int constant = 10;
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tfor(int = 0; i < 10; i++) {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\n\r\nvoid func()\r\n{\r\n\tfor(int = 0; i < MAGIC_NUMBER; i++) {}\r\n}";
            
            var result = Refactorer2810.ExtractConstant(constant, constantName, inputText);
          
            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.2");
        }
    }
}
