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
            string text = @"
                    void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }";
            string expectedResult = "const int " + constName + " = " + constValue + ";";
            var result = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text),constValue,constName)[0];
            
            Assert.AreEqual(expectedResult,result);

        }
        [TestMethod]
        public void BaseCase2()
        {
            string constValue = "10,4";
            string constName = "Number";
            string constType = "double";
            string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }";
            string expectedResult = "const "+ constType + " " + constName + " = " + constValue + ";";
            var result = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text),constValue,constName)[0];
            
            Assert.AreEqual(expectedResult,result);

        }
    }
}