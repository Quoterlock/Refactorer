﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System;

namespace UnitTests
{
    [TestClass]
    public class ExtractConstantUnitTests
    {
        //1
        [TestMethod]
        public void Extract_MagicNumber()
        {
            string constant = "10";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tfor(int = 0; i < 10; i++) {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\n\r\nvoid func()\r\n{\r\n\tfor(int = 0; i < MAGIC_NUMBER; i++) {}\r\n}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        //2
        [TestMethod]
        public void Replace_MagicNumber_In_ArithmeticExample()
        {
            string constant = "10";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tx=10+1 {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\n\r\nvoid func()\r\n{\r\n\tx=MAGIC_NUMBER+1 {}\r\n}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        //4
        [TestMethod]
        public void Extract_StringConstant()
        {
            string key = "text";
            int selectedRow = 2;
            string constantName = "MAGIC_CONSTANT";
            string inputText = "void func()\r\n{\r\n\tif(key == \"text\")  {}\r\n}";
            string expectedOutput = "const int MAGIC_CONSTANT = \"text\"\r\n\r\nvoid func()\r\n{\r\n\tif(key == MAGIC_CONSTANT) {}\r\n}";

            var result = Refactorer2810.ExtractConstant(key, constantName, selectedRow, inputText);

            Assert.AreEqual(expectedOutput, result, "The magic constant was not replaced correctly.");
        }

        //7
        // тест перевіряє ситуацію, коли магічна константа використовується для побудови рядка, який представляє шлях до ресурсу
        [TestMethod]
        public void ExtractConstant_UseInResourcePath()
        {
            // Arrange
            string key = "resourcePath";
            int selectedRow = 0;
            string constantName = "MAGIC_CONSTANT";
            string inputText = "string filePath = \"/resources/resurcePath.txt\";";
            string expectedOutput = "string filePath = \"/resources/\" + MAGIC_CONSTANT + \".txt\";";

            // Act
            string result = Refactorer2810.ExtractConstant(key, constantName, selectedRow, inputText);

            // Assert
            Assert.AreEqual(expectedOutput, result, "The magic constant should be used in a resource path.");
        }

        [TestMethod]
        public void ExtractConstant_In_Its_Class()
        {
            int selectedRow = 12;
            string constantValue = "10";

            string constantName = "CONST_NAME";
            string inputText = "class MyClass\r\n{\r\n" +
                "\tpublic: void Func()\r\n\t{\r\n" +
                "\t\tint res = 10 + 5;\r\n\t}\r\n}\r\n" +
                "\r\nclass AnotherClass\r\n{\r\n" +
                "\tpublic: void AnotherFunc()\r\n\t{\r\n" +
                "\t\tint res = 10 + 5;\r\n\t}\r\n}";
            string expectedOutput = "class MyClass\r\n{\r\n" +
                "\tpublic: void Func()\r\n\t{\r\n" +
                "\t\tint res = 10 + 5;\r\n\t}\r\n}\r\n" +
                "\r\nclass AnotherClass\r\n{\r\n" +
                "\tconst int CONST_NAME = 10;\r\n" +
                "\tpublic: void AnotherFunc()\r\n\t{\r\n" +
                "\t\tint res = CONST_NAME + 5;\r\n\t}\r\n}";

            var result = Refactorer2810.ExtractConstant(constantValue, constantName, selectedRow, inputText);

            Assert.AreEqual(result, expectedOutput);
        }

        [TestMethod]
        public void Two_Constants_In_A_Row()
        {
            int selectedRow = 12;
            string constantValue = "10";
            string constantName = "CONST_NAME";
            string inputText = "class MyClass\r\n{\r\n" +
                "\tpublic: void Func()\r\n\t{\r\n" +
                "\t\tint res = 10 + 10;\r\n\t}\r\n}";
            string expectedOutput = "class MyClass\r\n" +
                "{\r\n\tconst int CONST_NAME = 10;\r\n" +
                "\tpublic: void Func()\r\n\t{\r\n" +
                "\t\tint res = CONST_NAME + CONST_NAME;\r\n\t}\r\n}";

            var result = Refactorer2810.ExtractConstant(constantValue, constantName, selectedRow, inputText);

            Assert.AreEqual(result, expectedOutput);
        }

        [TestMethod]
        public void Constant_Out_Of_Func()
        {
            int selectedRow = 2;
            string constantValue = "10";
            string constantName = "CONST_NAME";
            string inputText = "void Func()\r\n{\r\n\tint res = 10 + 1;\r\n}";
            string expectedOutput = "const int CONST_NAME = 10\r\nvoid Func()\r\n{\r\n\tint res = CONST_NAME + 1;\r\n}";

            var result = Refactorer2810.ExtractConstant(constantValue, constantName, selectedRow, inputText);

            Assert.AreEqual(result, expectedOutput);
        }


        //5 !!!! For / if / аргумент функції це можна вважати одним випадком (як на мене)!!!!!!
        // використання магічної константи в агрументі методу
        // якщо враховувати що користувач обирає певний рядок - то тест не валідний
        [TestMethod]
        public void ExtractConstant_UseInMethodArgument()
        {
            // Arrange
            string key = "methodArg";
            string constantName = "MAGIC_CONSTANT";
            string inputText = "SomeMethod(MAGIC_CONSTANT);";
            string expectedOutput = "SomeMethod(methodArg);";

            // Act
            //string result = Refactorer2810.ExtractConstant(key, constantName, inputText);

            // Assert
            //Assert.AreEqual(expectedOutput, result, "The magic constant should be used as a method argument.");
        }

        //6
        // як розмір масиву (не впевнена щодо цього)
        // якщо враховувати що користувач обирає певний рядок - то тест не валідний
        [TestMethod]
        public void ExtractConstant_UseInArraySize()
        {
            // Arrange
            string key = "arraySize";
            string constantName = "MAGIC_CONSTANT";
            string inputText = "int[] arr = new int[MAGIC_CONSTANT];";
            string expectedOutput = "int[] arr = new int[arraySize];";

            // Act
            //string result = Refactorer2810.ExtractConstant(key, constantName, inputText);

            // Assert
            //Assert.AreEqual(expectedOutput, result, "The magic constant should be used as an array size.");
        }


        //3 - якщо враховувати що користувач обирає певний рядок - то тест не валідний
        [TestMethod]
        public void Same_Constant_In_Switch_Case()
        {
            string constant = "5";
            string constantName = "MAGIC_CONST";
            string inputText = "for(int i = 0; i < 5; i++)\r\n{\r\n}\r\n\r\nint ch = 2;\r\nswitch(ch)\r\n{\r\n\tcase 2: break;\r\n\tcase 5: break;\r\n}";
            string expectedOutput = "const int MAGIC_CONST = 5;\r\nfor(int i = 0; i < MAGIC_CONST; i++)\r\n{\r\n}\r\n\r\nint ch = 2;\r\nswitch(ch)\r\n{\r\n\tcase 2: break;\r\n\tcase 5: break;\r\n}";

            //var result = Refactorer2810.ExtractConstant(constant, constantName, inputText);

            //Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

    }
}
