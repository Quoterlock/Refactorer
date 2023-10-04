using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using Refactorer.Exceptions;
using System;
using System.Runtime.CompilerServices;

namespace UnitTests
{
    [TestClass]
    public class RenameMethodTests
    {
        // 1 - Переіменування методу (звичайний випадок)
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

        // 2 - Назва класу містить назву методу
        //    -> Не змінювати назву класу 
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

        // 3 - Переіменування двох методів у одному рядку
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

        // 4 - Назва методу містить у собі назву методу (на кінці), який переіменовуємо
        //    -> не змінюємо назву першого
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

        // 5 - Назва методу містить у собі назву методу (в середині), який переіменовуємо
        //    -> не змінюємо назву першого
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

        // 6 - Два однакових методи у різних класах, якщо користувач не обрав клас
        //    -> виключення
        [TestMethod]
        public void Two_Same_Methods_In_Different_Classes()
        {
            string oldName = "OldName"; string newName = "NewName";
            string inputText = "class FirstClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}\r\n" +
                "\r\nclass SecondClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}";

            Assert.ThrowsException<MoreThanOneMethodExistException>(() => Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText));
        }

        // 7 - Два однакових методи у різних класах
        //    -> переіменування тільки у тому класі, який вказав користувач
        [TestMethod]
        public void Two_Same_Methods_In_Different_Classes_With_SelectedClass()
        {
            // Arrange
            string oldName = "OldName"; string newName = "NewName"; string className = "FirstClass";
            string inputText = "class FirstClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}\r\n" +
                "\r\nclass SecondClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}";
            string expectedResult = "class FirstClass\r\n{\r\n\tpublic:\r\n" +
                "\tvoid NewName()\r\n\t{\r\n\t}\r\n}\r\n\r\nclass SecondClass\r\n" +
                "{\r\n\tpublic:\r\n\tvoid OldName()\r\n\t{\r\n\t}\r\n}";
            // Act
            var result = Refactorer2810.RenameMethod(oldName, newName, className, inputText);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        // 8 - Назва методу міститься у символьній константі
        //    -> Не змінювати символьну константу
        [TestMethod]
        public void MethodName_As_String_Constant()
        {
            // Arrange
            string oldName = "OldName"; string newName = "NewName";
            string inputText = "OldName()\r\n{\r\n}\r\n" +
                "\r\nstring str = \"some text OldName() text\";";
            string expectedResult = "NewName()\r\n{\r\n}\r\n\r\n" +
                "string str = \"some text OldName() text\";";
            // Act
            var result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);
            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        // 9 - Назва методу, що обрав користувач відповідає зарезервованому слову -> виключення
        [TestMethod]
        public void MethodName_Equal_To_ReservedWord()
        {
            // Arrange
            string oldName = "OldName"; string newName = "class";
            string inputText = "void OldName()\r\n{\r\n}";
            // Act + Assert
            Assert.ThrowsException<NameAlreadyExistException>(() => Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText));
        }

        // 10 - Назва методу, яку вписав користувач вже існує -> виключення
        [TestMethod]
        public void MethodName_Is_Already_Exist()
        {
            // Arrange
            string oldName = "OldName"; string newName = "MethodName";
            string className = "MyClass";
            string inputText = "class MyClass\r\n{\r\n" +
                "\tvoid MethodName()\r\n\t{\r\n\t}\r\n" +
                "\tvoid OldName()\r\n\t{\r\n\t}\r\n}";
            // Act + Assert
            Assert.ThrowsException<NameAlreadyExistException>(() => Refactorer2810.RenameMethod(oldName, newName, className, inputText));
        }

        //11
        //назва змінної відповідає назві методу
        [TestMethod]
        public void MethodName_Same_As_VarName()
        {
            string oldName = "func"; string newName = "newFunc";
            string inputText = @"
                void someFunc()
                {
                    int func = 1;
                    func();
                }

                void func() 
                {
                }";
            string expectedOutput = @"
                void someFunc()
                {
                    int func = 1;
                    newFunc();
                }

                void newFunc() 
                {
                }";

            // Act
            string result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, inputText);

            // Assert
            Assert.AreEqual(result, expectedOutput);
        }

        //12
        //якщо метод використовується у коментарі
        [TestMethod]
        public void MethodName_In_LineComment()
        {
            string oldName = "oldName";
            string newName = "newName";
            string input = @"
                void someFunc()
                {
                    //oldName();
                }

                void oldName() 
                {
                }";
            string expectedOutput = @"
                void someFunc()
                {
                    //func();
                }

                void newName() 
                {
                }";

            string result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, input);

            Assert.AreEqual(expectedOutput, result);
        }

        //13
        //якщо метод використовується у коментарі
        [TestMethod]
        public void MethodName_In_MultilineComment()
        {
            string oldName = "oldName";
            string newName = "newName";
            string input = @"
                void someFunc()
                {
                    /*
                    oldName();
                    */
                }

                void oldName() 
                {
                }";
            string expectedOutput = @"
                void someFunc()
                {
                    /*
                    oldName();
                    */
                }

                void newName() 
                {
                }";

            string result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, input);

            Assert.AreEqual(expectedOutput, result);
        }

        //14
        // випадок "func(); //func()"
        [TestMethod]
        public void MethodName_Before_LineComment()
        {
            string oldName = "oldName";
            string newName = "newName";
            string input = @"
                void someFunc()
                {
                    oldName(); //oldName()
                }

                void oldName() 
                {
                }";
            string expectedOutput = @"
                void someFunc()
                {
                    newName(); //oldName()
                }

                void newName() 
                {
                }";

            string result = Refactorer2810.RenameMethod(oldName, newName, string.Empty, input);

            Assert.AreEqual(expectedOutput, result);
        }

    }
}
