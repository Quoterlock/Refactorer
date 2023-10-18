using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;

namespace UnitTests
{


    [TestClass]
    public class AddConstDeclarationTest
    {
        [TestMethod]
        public void BaseCase()
        {
            string constValue = "10";
            string constName = "Number";
            int expectedConstantRow = 0;
            string text = @"
                    void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }";
            string expectedResult = "const int " + constName + " = " + constValue + ";";
            var result = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text),constValue,constName,3)[expectedConstantRow];
            
            Assert.AreEqual(expectedResult,result);

        }
        [TestMethod]
        public void DoubleTypeConstant()
        {
            string constValue = "10,4";
            string constName = "Number";
            string constType = "double";
            int expectedConstantRow = 0;
            string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }";
            string expectedResult = "const "+ constType + " " + constName + " = " + constValue + ";";
            var result = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text),constValue,constName,3)[expectedConstantRow];
            
            Assert.AreEqual(expectedResult,result);

        }
        [TestMethod]
        public void BaseCaseWithClass()
        {
            string constValue = "10,4";
            string constName = "Number";
            string constType = "double";
            int expectedConstantRow = 2;
            string text = @"
            class test{

            void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }";
            string expectedResult = "const "+ constType + " " + constName + " = " + constValue + ";";
            var result = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text),constValue,constName,3)[expectedConstantRow];
            
            Assert.AreEqual(expectedResult,result);

        }
    }
}