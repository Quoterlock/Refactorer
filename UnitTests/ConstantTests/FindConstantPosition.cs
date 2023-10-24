using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;

namespace UnitTests.AdditionalTests
{
    
        [TestClass]
        public class FindConstantPositionTest
        {
            [TestMethod]
            public void BaseCase()
            {
                string text = @"int num10 = 10+10;";
                string constValue = "10";
                List<int> result = Refactorer2810.FindConstPosition(text,constValue);
                List<int> expectedResult = new List<int>() { 12, 15 };
                bool areEqual = result.SequenceEqual(expectedResult);
                Assert.IsTrue(areEqual);

            }
            
        }
    
}