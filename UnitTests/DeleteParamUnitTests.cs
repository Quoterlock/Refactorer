﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System.Runtime.Remoting.Messaging;

namespace UnitTests
{
    [TestClass]
    public class DeleteParamUnitTests
    {
        // 1
        [TestMethod]
        public void DeleteParam_BaseCase_Successfully()
        {
            string input = "void func(int param)\r\n{\r\n\r\n}";
            string expectedOutput = "void func()\r\n{\r\n\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        /*
        [TestMethod]
        public void DeleteParam_ParameterNotPresent_NoChange()
        {
            string input = "void someFunction(int param1, int param2)";
            string paramName = "param3"; // IncorrectParamName
           
            string expectedOutput = "void someFunction(int param2)";
            var res = Refactorer2810.RemoveUnusedParameters(paramName,  input);
            Assert.AreEqual(expectedOutput,res);
        }
        */

        /*
        [TestMethod]
        public void DeleterParam_NotFunction_ThrowException()
        {
            // Arrange
            string input = "int param1 = 10;";
            string paramName = "param1";
            var res = Refactorer2810.RemoveUnusedParameters(paramName, input);
            //Assert.ThrowsException<>(() => Refactorer2810.RemoveUnusedParameters(paramName, input));
        }
        */

        // 2
        [TestMethod]
        public void DeleteParam_FunctionNameEqualParamName_DeleteParamName()
        {
            string input = "void func(int test)\r\n{\r\n\tint res = test();\r\n}";
            string expectedOutput = "void func()\r\n{\r\n\tint res = test();\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput,res);
        }

        // 3
        [TestMethod]
        public void UnusedParam_with_default_value()
        {
            string input = "void func(int param = 1)\r\n{\r\n\r\n}";
            string expectedOutput = "void func()\r\n{\r\n\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        // 4
        [TestMethod]
        public void UnusedParam_used_in_string_constant()
        {
            string input = "void func(int param)\r\n{\r\n\tstring tmp = \"param is 0\";\r\n}\r\n";
            string expectedOutput = "void func()\r\n{\r\n\tstring tmp = \"param is 0\";\r\n}\r\n";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        // 5
        [TestMethod]
        public void UnusedParamName_same_as_classMethod_name()
        {
            string input = "void func(int test)\r\n{\r\n\tMyClass myClass;\r\n\tint tmp = myClass.test();\r\n}";
            string expectedOutput = "void func()\r\n{\r\n\tMyClass myClass;\r\n\tint tmp = myClass.test();\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        // 6
        [TestMethod]
        public void ClassObjectParam_used_in_funcion()
        {
            // якщо викликається метод класу (який передали через параметер),
            // то він вважається used.
            string input = "void func(MyClass obj)\r\n{\r\n\tobj.Func();\r\n}";
            string expectedOutput = "void func(MyClass obj)\r\n{\r\n\tobj.Func();\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        // 7
        [TestMethod]
        public void TwoParams_in_differet_cases()
        {
            string input = "void func(int param, int Param)\r\n{\r\n\tint res = Param + 1;\r\n}";
            string expectedOutput = "void func(int Param)\r\n{\r\n\tint res = Param + 1;\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        // 8
        [TestMethod]
        public void UnusedParam_has_same_name_as_className()
        {
            string input = "void func(int Test)\r\n{\r\n\tTest tmp;\r\n}";
            string expectedOutput = "void func()\r\n{\r\n\tTest tmp;\r\n}";
            var res = Refactorer2810.RemoveUnusedParameters(input);
            Assert.AreEqual(expectedOutput, res);
        }

        /*
        [TestMethod]
        public void DeleteParam_EmptyFunction_NoChange()
        {
            string input = "void someFunction()";
            string paramName = "param1";
            string expectedOutput = "void someFunction()";
            var res = Refactorer2810.RemoveUnusedParameters(paramName,  input);
            Assert.AreEqual(expectedOutput,res);
        }
        */
    }
}