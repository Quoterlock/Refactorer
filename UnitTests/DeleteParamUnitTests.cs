using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;

namespace UnitTests
{
    public class DeleteParamUnitTests
    {
        [TestMethod]
        public void DeleteParam_BaseCase_Successfully()
        {
            string input = "void someFunction(int param1, int param2)";
            string paramName = "param1";
            string expectedOutput = "void someFunction(int param2)";
            // Arrange
            var res = Refactorer2810.RemoveUnusedParameters(paramName,input);
            Assert.AreEqual(expectedOutput, res);
        }

        [TestMethod]
        public void DeleteParam_ParameterNotPresent_NoChange()
        {
            string input = "void someFunction(int param1, int param2)";
            string paramName = "param3"; // IncorrectParamName
           
            string expectedOutput = "void someFunction(int param2)";
            var res = Refactorer2810.RemoveUnusedParameters(paramName,  input);
            Assert.AreEqual(expectedOutput,res);
        }

        [TestMethod]
        public void DeleterParam_NotFunction_ThrowException()
        {
            // Arrange
            string input = "int param1 = 10;";
            string paramName = "param1"; 
            var res = Refactorer2810.RemoveUnusedParameters(paramName, input);
            Assert.ThrowsException</*SomeException*/>(()=> Refactorer2810.RemoveUnusedParameters(paramName,input));
        }

        [TestMethod]
        public void DeleteParam_FunctionNameEqualParamName_DeleteParamName()
        {
            string input = "int test(int test, int param2)";
            string paramName = "test";
            string expectedOutput = "int test(int param2)";
            var res = Refactorer2810.RemoveUnusedParameters(paramName,  input);
            Assert.AreEqual(expectedOutput,res);
        }

        [TestMethod]
        public void DeleteParam_EmptyFunction_NoChange()
        {
            string input = "void someFunction()";
            string paramName = "param1";
            string expectedOutput = "void someFunction()";
            var res = Refactorer2810.RemoveUnusedParameters(paramName,  input);
            Assert.AreEqual(expectedOutput,res);
        }
    }
}