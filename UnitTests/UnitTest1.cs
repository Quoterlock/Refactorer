using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
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
