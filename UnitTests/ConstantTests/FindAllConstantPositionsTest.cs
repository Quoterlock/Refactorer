using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;

namespace UnitTests.AdditionalTests
{
    public static class Helper
    {
        public static bool AreDictionariesEqual(Dictionary<int, List<int>> dict1, Dictionary<int, List<int>> dict2)
        {
            if (dict1.Count != dict2.Count)
                return false;

            foreach (var key in dict1.Keys)
            {
                if (!dict2.ContainsKey(key))
                    return false;

                var list1 = dict1[key];
                var list2 = dict2[key];

                if (list1.Count != list2.Count || !list1.All(list2.Contains))
                    return false;
            }

            return true;
        }
    }
    
        [TestClass]
        public class FindAllConstantPositionsTest
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

                }";
                string constValue = "10";
                Dictionary<int,List<int>> result = Refactorer2810.FindAllConstantPositions(Parser.SplitOnLines(text),constValue);
                Dictionary<int,List<int>> expectedResult =  new Dictionary<int,List<int>>();
                expectedResult.Add(key: 2,value: new List<int>() {37});
                expectedResult.Add(key: 4,value: new List<int>() { 36, 39 });
                bool areEqual = Helper.AreDictionariesEqual(expectedResult, result);
                Assert.IsTrue(areEqual);

            }
            [TestMethod]
            public void MultilineComment()
            {
                string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10;
                        /*int num10 = 10+10;*/
                    }      

                }";
                string constValue = "10";
                Dictionary<int,List<int>> result = Refactorer2810.FindAllConstantPositions(Parser.SplitOnLines(text),constValue);
                Dictionary<int,List<int>> expectedResult =  new Dictionary<int,List<int>>();
                expectedResult.Add(key: 2,value: new List<int>() {37});
                expectedResult.Add(key: 4,value: new List<int>() { 36, 39 });
                bool areEqual = Helper.AreDictionariesEqual(expectedResult, result);
                Assert.IsTrue(areEqual);

            }
            [TestMethod]
            public void MultilineComment2()
            {
                string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10;
                        /*
                        int num10 = 10+10;
                        */
                    }      

                }";
                string constValue = "10";
                Dictionary<int,List<int>> result = Refactorer2810.FindAllConstantPositions(Parser.SplitOnLines(text),constValue);
                Dictionary<int,List<int>> expectedResult =  new Dictionary<int,List<int>>();
                expectedResult.Add(key: 2,value: new List<int>() {37});
                expectedResult.Add(key: 4,value: new List<int>() { 36, 39 });
                bool areEqual = Helper.AreDictionariesEqual(expectedResult, result);
                Assert.IsTrue(areEqual);

            }
            [TestMethod]
            public void MultilineComment3()
            {
                string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10;
                        /*
                        int num10 = 10+10;
                        */int num10 = 10+10;
                    }      

                }";
                string constValue = "10";
                Dictionary<int,List<int>> result = Refactorer2810.FindAllConstantPositions(Parser.SplitOnLines(text),constValue);
                Dictionary<int,List<int>> expectedResult =  new Dictionary<int,List<int>>();
                expectedResult.Add(key: 2,value: new List<int>() {37});
                expectedResult.Add(key: 4,value: new List<int>() { 36, 39 });
                expectedResult.Add(key: 7,value: new List<int>() { 38, 41 });
                bool areEqual = Helper.AreDictionariesEqual(expectedResult, result);
                Assert.IsTrue(areEqual);

            }
            
            [TestMethod]
            public void MultilineComment4()
            {
                string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int num10 = 10+10 /*10+10*/;
                    }      

                }";
                string constValue = "10";
                Dictionary<int,List<int>> result = Refactorer2810.FindAllConstantPositions(Parser.SplitOnLines(text),constValue);
                Dictionary<int,List<int>> expectedResult =  new Dictionary<int,List<int>>();
                expectedResult.Add(key: 2,value: new List<int>() {37});
                expectedResult.Add(key: 4,value: new List<int>() { 36, 39 });
                bool areEqual = Helper.AreDictionariesEqual(expectedResult, result);
                Assert.IsTrue(areEqual);

            }
            [TestMethod]
            public void LineComment()
            {
                string text = @"void func()
                {
                    for(int = 0; i < 10; i++) 
                    { 
                        int nu10m10 = 10+10; // 10
                        
                    }      

                }";
                string constValue = "10";
                Dictionary<int,List<int>> result = Refactorer2810.FindAllConstantPositions(Parser.SplitOnLines(text),constValue);
                Dictionary<int,List<int>> expectedResult =  new Dictionary<int,List<int>>();
                expectedResult.Add(key: 2,value: new List<int>() {37});
                expectedResult.Add(key: 4,value: new List<int>() { 38, 41 });
                bool areEqual = Helper.AreDictionariesEqual(expectedResult, result);
                Assert.IsTrue(areEqual);

            }
        }
    
}