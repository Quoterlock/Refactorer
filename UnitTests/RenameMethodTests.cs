using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System;

namespace UnitTests
{
    [TestClass]
    public class RenameMethodTests
    {
        [TestMethod]
        public void RenameMethodTest()
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
        public void Class_name_contains_MethodName()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = 
                "class OldNameClass\r\n{\r\n" +
                "\tpublic:\r\n\tOldNameClass()\r\n" +
                "\t{\r\n\t\tOldName();\r\n\t}\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t\t\t\r\n\t}\r\n}";
            string expectedResult = 
                "class OldNameClass\r\n{\r\n" +
                "\tpublic:\r\n\tOldNameClass()\r\n\t{\r\n" +
                "\t\tNewName();\r\n\t}\r\n\tvoid NewName()\r\n" +
                "\t{\r\n\t\t\t\r\n\t}\r\n}";

            var result = Refactorer2810.RenameMethod(oldName, newName, inputText);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void Two_methods_in_one_line()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = 
                "class MyClass\r\n{\r\n\tpublic:\r\n" +
                "\tMyClass()\r\n\t{\r\n" +
                "\t\tint result = OldName() + OldName();\r\n\t}\r\n" +
                "\tint OldName()\r\n\t{\r\n\t\treturn 1;\t\t\r\n\t}\r\n}";

            string expectedResult = "class MyClass\r\n{\r\n" +
                "\tpublic:\r\n\tMyClass()\r\n\t{\r\n" +
                "\t\tint result = NewName() + NewName();\r\n\t}\r\n" +
                "\tint NewName()\r\n\t{\r\n\t\treturn 1;\t\t\r\n\t}\r\n}";

            var result = Refactorer2810.RenameMethod(oldName, newName, inputText);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void MethodName_contains_another_MethodName_in_the_end()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = 
                "class MyClass\r\n{\r\n\tpublic:\r\n" +
                "\tMyClass()\r\n\t{\r\n\t\tAnotherMethodOldName();\r\n" +
                "\t\tOldName();\r\n\t}\r\n\r\n\tvoid OldName()\r\n" +
                "\t{\t\t\r\n\t}\r\n\r\n\tvoid AnotherMethodOldName()\r\n" +
                "\t{\r\n\t}\r\n}";
            string expectedResult = "class MyClass\r\n" +
                "{\r\n\tpublic:\r\n\tMyClass()\r\n" +
                "\t{\r\n\t\tAnotherMethodOldName();\r\n" +
                "\t\tNewName();\r\n\t}\r\n\r\n" +
                "\tvoid NewName()\r\n\t{\t\t\r\n\t}\r\n" +
                "\r\n\tvoid AnotherMethodOldName()\r\n\t{\r\n" +
                "\t}\r\n}";

            var result = Refactorer2810.RenameMethod(oldName, newName, inputText);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void MethodName_contains_another_MethodName_in_the_middle()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = 
                "class MyClass\r\n{\r\n\tpublic:\r\n" +
                "\tMyClass()\r\n\t{\r\n\t\tOldName();\r\n" +
                "\t\tSomeOldNameMethod();\r\n\t}\r\n\r\n" +
                "\tvoid OldName()\r\n\t{\t\t\r\n\t}\r\n\r\n" +
                "\tvoid SomeOldNameMethod()\r\n\t{\t\t\r\n\t}\r\n}";

            string expectedResult = 
                "class MyClass\r\n{\r\n\tpublic:\r\n" +
                "\tMyClass()\r\n\t{\r\n\t\tNewName();\r\n" +
                "\t\tSomeOldNameMethod();\r\n\t}\r\n\r\n" +
                "\tvoid NewName()\r\n\t{\t\t\r\n\t}\r\n\r\n" +
                "\tvoid SomeOldNameMethod()\r\n\t{\t\t\r\n\t}\r\n}";

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
          
            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.1");
        }
    }
}
