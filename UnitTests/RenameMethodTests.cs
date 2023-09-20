using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using Refactorer.Exceptions;
using System;

namespace UnitTests
{
    [TestClass]
    public class RenameMethodTests
    {
        //1
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

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            Assert.AreEqual(expectedResult, result);
        }
        //2
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

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            Assert.AreEqual(expectedResult, result);
        }
        //3
        [TestMethod]
        public void Two_Methods_In_One_Line()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText =
                "class MyClass\r\n{\r\n\tpublic:\r\n" +
                "\tMyClass()\r\n\t{\r\n" +
                "\t\tint result = OldName()+OldName();\r\n\t}\r\n" +
                "\tint OldName()\r\n\t{\r\n\t\treturn 1;\t\t\r\n\t}\r\n}";

            string expectedResult = "class MyClass\r\n{\r\n" +
                "\tpublic:\r\n\tMyClass()\r\n\t{\r\n" +
                "\t\tint result = NewName()+NewName();\r\n\t}\r\n" +
                "\tint NewName()\r\n\t{\r\n\t\treturn 1;\t\t\r\n\t}\r\n}";

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            Assert.AreEqual(expectedResult, result);
        }
        //4
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

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            Assert.AreEqual(expectedResult, result);
        }
        //5
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

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            Assert.AreEqual(expectedResult, result);
        }

        private int a;
       
        [TestMethod]
        public void test ()
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

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty,inputText);

            Assert.AreEqual(expectedResult, result);
        }
        //6
        [TestMethod]
        public void Two_Same_Methods_In_Different_Classes()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = "class FirstClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}\r\n" +
                "\r\nclass SecondClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}";

            //!!!!!!ТИМЧАСОВЕ РІШЕННЯ!!!!!!!
            try
            {
                var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);
                Assert.IsTrue(false);
            }
            catch (NameAlreadyExistException ex)
            {
                Assert.IsTrue(true);
            }
        }
        //7
        [TestMethod]
        public void Two_Same_Methods_In_Different_Classes_With_SelectedClass()
        {
            string oldName = "OldName"; string newName = "NewName"; string className = "FirstClass";
            string inputText = "class FirstClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}\r\n" +
                "\r\nclass SecondClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}";


            string expectedResult = "class FirstClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid NewName()\r\n\t{\r\n\t}\r\n}\r\n\r\nclass SecondClass\r\n" +
                "{\r\n\tpublic:\r\n\tvoid OldName()\r\n\t{\r\n\t}\r\n}";


            var result = Refactorer2810.RenameMethod(oldName, newName, className, inputText);
        }
        //8
        [TestMethod]
        public void MethodName_As_String_Constant()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = "OldName()\r\n{\r\n}\r\n" +
                "\r\nstring str = \"some text OldName() text\";";

            string expectedResult = "NewName()\r\n{\r\n}\r\n\r\n" +
                "string str = \"some text OldName() text\";";

            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            Assert.AreEqual(expectedResult, result);
        }
        //9
        [TestMethod]
        public void MethodName_Equal_To_ReservedWord()
        {
            string oldName = "OldName"; string newName = "class";
            string inputText = "void OldName()\r\n{\r\n}";
            bool result = false;
            try
            {
                Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);
                result = false;
            }
            catch (NameAlreadyExistException ex)
            {
                result = true;
            }

            Assert.IsTrue(result);
        }
        //10
        [TestMethod]
        public void MethodName_Is_Already_Exist()
        {
            string oldName = "OldName"; string newName = "MethodName";
            string className = "MyClass";
            string inputText = "class MyClass\r\n{\r\n" +
                "\tvoid MethodName()\r\n\t{\r\n\t}\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}";

            bool result = false;
            try
            {
                Refactorer2810.RenameMethod(oldName, newName, className, inputText);
            }
            catch (NameAlreadyExistException ex) 
            {
                result = true;
            }

            Assert.IsTrue(result);
        }
    }
}
