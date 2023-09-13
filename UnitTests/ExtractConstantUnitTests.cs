using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System;

namespace UnitTests
{
    [TestClass]
    public class ExtractConstantUnitTests
    {
        [TestMethod]
        public void TestExtractConstant_ReplacesMagicNumberWithConstant()
        {
            string constant = "10";
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tfor(int = 0; i < 10; i++) {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\n\r\nvoid func()\r\n{\r\n\tfor(int = 0; i < MAGIC_NUMBER; i++) {}\r\n}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, inputText);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        [TestMethod]
        public void TestExtractConstant_ReplaceMagicNumberInArithmeticExample()
        {
            string constant = "10";
            string constantName = "MAGIC_NUMBER";
            string inputText = "void func()\r\n{\r\n\tx=10+1 {}\r\n}";
            string expectedOutput = "const int MAGIC_NUMBER = 10;\r\n\r\nvoid func()\r\n{\r\n\tx=MAGIC_NUMBER+1 {}\r\n}";

            var result = Refactorer2810.ExtractConstant(constant, constantName, inputText);

            Assert.AreEqual(expectedOutput, result, "The magic number was not replaced correctly.");
        }

        [TestMethod]
        public void TestExtractConstant_ReplaceMagicNumber_TryToFindAnoter()
        {
           // якщо у нас магічне число 3 і воно використовується для розміру масиву, щоб воно не замінювало в світчкейс цю цифру три
        }

        [TestMethod]
        public void TestExtractConstant_ReplaceMagicNumber_WithHashes()
        {
            string key = "text";
            string constantName = "MAGIC_CONSTANT";
            string inputText = "void func()\r\n{\r\n\tif(key == \"text\")  {}\r\n}";
            string expectedOutput = "const int MAGIC_CONSTANT = \"text\"\r\n\r\nvoid func()\r\n{\r\n\tif(key == MAGIC_CONSTANT) {}\r\n}";

            var result = Refactorer2810.ExtractConstant(key, constantName, inputText);

            Assert.AreEqual(expectedOutput, result, "The magic constant was not replaced correctly.");
        }
    }
}
