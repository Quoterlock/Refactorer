using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using Refactorer.Exceptions;
using System;

namespace UnitTests
{
    [TestClass]
    public class ExtractConstantUnitTests
    {
        // 1 - Виніс магічного числа у константу (звичайний випадок)
        [TestMethod]
        public void Extract_SingleMagicNumber()
        {
            string constant = "10";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tfor(int = 0; i < 10; i++) {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\nvoid func()\r\n{\r\n\tfor(int = 0; i < MAGIC_NUMBER; i++) {}\r\n}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText, false);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        // 2 - Виніс магічного числа у константу в арифметичних виразах
        [TestMethod]
        public void Replace_MagicNumber_In_ArithmeticExample()
        {
            string constant = "10";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tx=10+1 {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\nvoid func()\r\n{\r\n\tx=MAGIC_NUMBER+1 {}\r\n}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText, false);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        // 3 - Виніс магічного тексту у рядкову константу
        [TestMethod]
        public void Extract_StringConstant()
        {
            string key = "text";
            int selectedRow = 2;
            string constantName = "MAGIC_CONSTANT";
            string inputText = "void func()\r\n{\r\n\tif(key == \"text\") {}\r\n}";
            string expectedOutput = "const string MAGIC_CONSTANT = \"text\";\r\nvoid func()\r\n{\r\n\tif(key == MAGIC_CONSTANT) {}\r\n}";

            var result = Refactorer2810.ExtractConstant(key, constantName, selectedRow, inputText, false);

            Assert.AreEqual(expectedOutput, result, "The magic constant was not replaced correctly.");
        }

        // 4 - Заміщення частини рядка символьною константою.
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
            string result = Refactorer2810.ExtractConstant(key, constantName, selectedRow, inputText, false);

            // Assert
            Assert.AreEqual(expectedOutput, result, "The magic constant should be used in a resource path.");
        }

        // 5 - Виніс магічного числа у константу у поля класу (не глобальну)
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
                "const int CONST_NAME = 10;\r\n" +
                "\tpublic: void AnotherFunc()\r\n\t{\r\n" +
                "\t\tint res = CONST_NAME + 5;\r\n\t}\r\n}";

            var result = Refactorer2810.ExtractConstant(constantValue, constantName, selectedRow, inputText, false);

            Assert.AreEqual(expectedOutput, result);
        }

        // 6 - Виніс константи, якщо вона зустрічається 2 рази в рядку
        [TestMethod]
        public void Two_Constants_In_A_Row()
        {
            int selectedRow = 4;
            string constantValue = "10";
            string constantName = "CONST_NAME";
            string inputText = "class MyClass\r\n{\r\n" +
                "\tpublic: void Func()\r\n\t{\r\n" +
                "\t\tint res = 10 + 10;\r\n\t}\r\n}";
            string expectedOutput = "class MyClass\r\n" +
                "{\r\nconst int CONST_NAME = 10;\r\n" +
                "\tpublic: void Func()\r\n\t{\r\n" +
                "\t\tint res = CONST_NAME + CONST_NAME;\r\n\t}\r\n}";

            var result = Refactorer2810.ExtractConstant(constantValue, constantName, selectedRow, inputText, false);

            Assert.AreEqual(expectedOutput, result);
        }

        // 7 - Якщо константа використовується у повідомленні до виключення
        [TestMethod]
        public void Constant_Used_In_Exception_Message()
        {
            // Arrange
            int selectedRow = 6;
            string constantValue = "\"An error occurred.\"";
            string constantName = "ERROR_MESSAGE";
            string inputText = "try\r\n{\r\n\t// Some code that may throw an exception\r\n}\r\ncatch (Exception ex)\r\n{\r\n\tConsole.WriteLine(\"An error occurred.\");\r\n}";
            string expectedOutput = "const string ERROR_MESSAGE = \"An error occurred.\";\r\ntry\r\n{\r\n\t// Some code that may throw an exception\r\n}\r\ncatch (Exception ex)\r\n{\r\n\tConsole.WriteLine(ERROR_MESSAGE);\r\n}";

            // Act
            var result = Refactorer2810.ExtractConstant(constantValue, constantName, selectedRow, inputText, false);

            // Assert
            Assert.AreEqual(expectedOutput, result);
        }

        // 8 - Якщо назва функції/змінної у цьому ж рядку
        //     містить константу у назві -> не виносити
        [TestMethod]
        public void Constant_In_OtherName()
        {
            string input = "void func()\r\n{\r\n\tint res = funcName10() + 10;\r\n}";
            string expectedOutput = "const int CONST_NAME = 10;\r\nvoid func()\r\n{\r\n\tint res = funcName10() + CONST_NAME;\r\n}";
            string constantName = "CONST_NAME"; string constantValue = "10"; int row = 2;

            var res = Refactorer2810.ExtractConstant(constantValue, constantName, row, input, false);

            Assert.AreEqual(expectedOutput, res);
        }

        // 9 - Якщо константа з такою назвою вже існує -> виключення
        [TestMethod]
        public void ConstantName_Already_Exist()
        {
            // Arrange
            string input = "const int CONST_NAME = 10;\r\nfor(int i = 0; i < 4; i++) \r\n{\r\n}";
            string constantName = "CONST_NAME";
            string constantValue = "4";
            int row = 2;

            // Act + Assert
            Assert.ThrowsException<NameAlreadyExistException>(() 
                => Refactorer2810.ExtractConstant(constantValue, constantName, row, input, false));
        }

        // 10 - Якщо зустрічається у рядковому коментарі
        [TestMethod]
        public void Constant_In_LineComment()
        {
            string input = @"            void func()
            {
                int res = funcName10() + 10; //10
            }";
            string expectedOutput = @"const int CONST_NAME = 10;
            void func()
            {
                int res = funcName10() + CONST_NAME; //10
            }";
            string constantName = "CONST_NAME"; string constantValue = "10";

            var res = Refactorer2810.ExtractConstant(constantValue, constantName, 4, input, true);

            Assert.AreEqual(expectedOutput, res);
        }

        // 11 - Якщо зустрічається у рядковому коментарі
        [TestMethod]
        public void Constant_In_MultilineComment()
        {
            string input = @"            void func()
            {
                int res = funcName10() + 10;
                /*
                    10
                */
            }";
            string expectedOutput = @"const int CONST_NAME = 10;
            void func()
            {
                int res = funcName10() + CONST_NAME;
                /*
                    10
                */
            }";
            string constantName = "CONST_NAME"; string constantValue = "10";

            var res = Refactorer2810.ExtractConstant(constantValue, constantName, 0, input, true);


            Assert.AreEqual(expectedOutput, res);
        }

        // 12 - Виніс магічного числа у константу (в усьому тексті програми)
        public void Extract_MagicNumber()
        {
            string constant = "10";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = @"void func()
{
    for(int = 0; i < 10; i++) 
    { 
        int num = 10 + 3;
    }
}";
            string expectedOutput = @"const int MAGIC_NUMBER = 10;
void func()
{
    for(int = 0; i < MAGIC_NUMBER; i++) 
    { 
        int num = MAGIC_NUMBER + 3;
    }
}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText, true);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        // 13 - Виніс понстанти з плаваючою крапкою
        [TestMethod]
        public void Extract_DoubleConstant()
        {
            string constant = "10.4";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num = 10.4 + 3;
                    }
                }";
            string expectedOutput = @"const double MAGIC_NUMBER = 10.4;
void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num = MAGIC_NUMBER + 3;
                    }
                }";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText, true);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        // 14 - якщо назва константи фігурує тільки у коментарі
        [TestMethod]
        public void ConstantName_AlreadyExist_InComment()
        {
            string constant = "10.4";
            int selectedRow = 2;
            string constantName = "MAGIC_NUMBER";
            string inputText = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num = 10.4 + 3; //MAGIC_NUMBER
                    }
                }";
            string expectedOutput = @"const double MAGIC_NUMBER = 10.4;
void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num = MAGIC_NUMBER + 3; //MAGIC_NUMBER
                    }
                }";

            var result = Refactorer2810.ExtractConstant(constant, constantName, selectedRow, inputText, true);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }
    }
}
