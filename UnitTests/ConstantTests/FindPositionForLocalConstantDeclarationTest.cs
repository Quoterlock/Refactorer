using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;

namespace UnitTests
{


    [TestClass]
    public class FindPositionForLocalConstantDeclarationTest
    {
        [TestMethod]
        public void BaseCase()
        {
            string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }
                    class{}      

                }";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),3);
            int expectedResult = 0;
            Assert.AreEqual(expectedResult, expectedResult);

        }
        [TestMethod]
        public void BaseCase2()
        {
            string text = @"class test
                {
                void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }}";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),4);
            int expectedResult = 2;
            Assert.AreEqual(expectedResult, expectedResult);

        }
        [TestMethod]
        public void LocalVariableNameContainsClass()
        {
            string text = @"class test
                {
                void func()
                {
                    string className = test;
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }}";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),5);
            int expectedResult = 2;
            Assert.AreEqual(expectedResult, expectedResult);

        }
        
        [TestMethod]
        public void LocalVariableNameContainsClass2()
        {
            string text = @"class test
                {
                void func()
                {
                    string className = test;
className = test2;
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      

                }}";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),6);
            int expectedResult = 2;
            Assert.AreEqual(expectedResult, expectedResult);

        }
        
        [TestMethod]
        public void ClassContainsAnotherClass()
        {
            string text = @"class test
                {
                class test2{
                void func()
                {
                    string TestclassName = test;
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      
                }
                }}";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),6);
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, expectedResult);

        }

        [TestMethod]
        public void ClassContainsAnotherClass2()
        {
            string text = @"class test
                {
                class test2
                {
                void func()
                {
                    string TestclassName = test;
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      
                }
                }}";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),7);
            int expectedResult = 4;
            Assert.AreEqual(expectedResult, expectedResult);

        }
        
        [TestMethod]
        public void BaseCaseWithInclude()
        {
            string text = @"#include
#include
#include
                void func()
                {
                    string TestclassName = test;
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10; 
                    }      
                }
class{}
                }}";
            var result = Parser.FindPositionForLocalConstantDeclaration(Parser.SplitOnLines(text),7);
            int expectedResult = 3;
            Assert.AreEqual(expectedResult, expectedResult);

        }

    }
}