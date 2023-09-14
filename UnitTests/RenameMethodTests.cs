using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System;

namespace UnitTests
{
    [TestClass]
    public class RenameMethodTests
    {
        [TestMethod]
        public void Rename_Method_Test()
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
        public void Class_Name_Contains_MethodName()
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
        public void Two_Methods_In_One_Line()
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
        public void MethodName_Contains_Another_MethodName_In_The_End()
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
        public void MethodName_Contains_Another_MethodName_In_The_Middle()
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
    }
}
