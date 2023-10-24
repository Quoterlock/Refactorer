using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;

namespace UnitTests
{

    [TestClass]
    public class AddConstDeclarationTest
    {
        /*        [TestMethod]
                public void BaseCase()
                {
                    string constValue = "10";
                    string constName = "Number";
                    int expectedConstantRow = 2;
                    string text = @"
                                    void func()
                                    {
                                        for(int i = 0; i < 10; i++) 
                                        { 
                                            int num10 = 10+10; 
                                        }      
                                    }";

                    string expectedResult = "const int " + constName + " = " + constValue + ";";
                    var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

                    // Перевірка, чи результат містить очікуваний рядок
                    Assert.IsTrue(updatedLines.Count > expectedConstantRow);
                    Assert.AreEqual(expectedResult, updatedLines[expectedConstantRow]);
                }*/

        /* [TestMethod]
         public void BaseCase()
         {
             string constValue = "10";
             string constName = "Number";
             int expectedConstantRow = 2;
             string text = @"
                     void func()
                     {
                         for(int i = 0; i < 10; i++) 
                         { 
                             int num10 = 10+10; 
                         }      
                     }";

             string expectedResult = "const int " + constName + " = " + constValue + ";";
             var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

             // Перевірка, чи результат містить очікуваний рядок
             Assert.IsTrue(updatedLines.Count > expectedConstantRow);
             string actualResult = updatedLines[expectedConstantRow].Trim();
             Assert.AreEqual(expectedResult, actualResult);
         }


         [TestMethod]
         public void DoubleTypeConstant()
         {
             string constValue = "10,4";
             string constName = "Number";
             string constType = "double";
             int expectedConstantRow = 2;
             string text = @"void func()
                             {
                                 for(int i = 0; i < 10; i++) 
                                 { 
                                     int num10 = 10+10; 
                                 }      
                             }";
             string expectedResult = "const " + constType + " " + constName + " = " + constValue + ";";
             var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

             // Перевірка, чи результат містить очікуваний рядок
             Assert.IsTrue(updatedLines.Count > expectedConstantRow);
             Assert.AreEqual(expectedResult, updatedLines[expectedConstantRow]);
         }

         [TestMethod]
         public void BaseCaseWithClass()
         {
             string constValue = "10,4";
             string constName = "Number";
             string constType = "double";
             int expectedConstantRow = 4;
             string text = @"
                             class test{
                                 void func()
                                 {
                                     for(int i = 0; i < 10; i++) 
                                     { 
                                         int num10 = 10+10; 
                                     }      
                                 }
                             }";
             string expectedResult = "const " + constType + " " + constName + " = " + constValue + ";";
             var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

             // Перевірка, чи результат містить очікуваний рядок
             Assert.IsTrue(updatedLines.Count > expectedConstantRow);
             Assert.AreEqual(expectedResult, updatedLines[expectedConstantRow]);
         }*/

        [TestMethod]
        public void BaseCase()
        {
            string constValue = "10";
            string constName = "Number";
            int expectedConstantRow = 2;
            string text = @"
        void func()
        {
            for(int i = 0; i < 10; i++) 
            { 
                int num10 = 10+10; 
            }      
        }";

            string expectedResult = "const int " + constName + " = " + constValue + ";";
            var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

            // Перевірка, чи результат містить очікуваний рядок
            Assert.IsTrue(updatedLines.Count > expectedConstantRow);
            string actualResult = updatedLines[expectedConstantRow].Trim();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void DoubleTypeConstant()
        {
            string constValue = "10.4"; // Замінено "," на "."
            string constName = "Number";
            string constType = "double";
            int expectedConstantRow = 2;
            string text = @"
        void func()
        {
            for(int i = 0; i < 10; i++) 
            { 
                int num10 = 10+10; 
            }      
        }";

            string expectedResult = "const " + constType + " " + constName + " = " + constValue + ";";
            var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

            // Перевірка, чи результат містить очікуваний рядок
            Assert.IsTrue(updatedLines.Count > expectedConstantRow);
            string actualResult = updatedLines[expectedConstantRow].Trim();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void BaseCaseWithClass()
        {
            string constValue = "10.4"; // Замінено "," на "."
            string constName = "Number";
            string constType = "double";
            int expectedConstantRow = 4;
            string text = @"
        class test{
            void func()
            {
                for(int i = 0; i < 10; i++) 
                { 
                    int num10 = 10+10; 
                }      
            }
        }";

            string expectedResult = "const " + constType + " " + constName + " = " + constValue + ";";
            var updatedLines = Refactorer2810.AddConstDeclaration(Parser.SplitOnLines(text), constValue, constName, 3);

            // Перевірка, чи результат містить очікуваний рядок
            Assert.IsTrue(updatedLines.Count > expectedConstantRow);
            string actualResult = updatedLines[expectedConstantRow].Trim();
            Assert.AreEqual(expectedResult, actualResult);
        }




    }
}