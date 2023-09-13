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
          
            string inputText = "This is a sample text with a magic number: 42";
            string expectedOutput = "This is a sample text with a magic number: MAGIC_NUMBER";
            
            var result = YourClassName.ExtractConstant(inputText);
          
            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }
    }
}
