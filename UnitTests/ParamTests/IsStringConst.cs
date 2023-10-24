using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refactorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ParamTests
{
    [TestClass]
    public class IsStringConst
    {
        [TestMethod]
        public void InStringConst()
        {
            var body = new List<string> { "string str = \"text here\";" };
            var result = Parser.IsStringConst(body, 0, 17);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotInStringConst()
        {
            var body = new List<string> { "string str = \"text here\";" };
            var result = Parser.IsStringConst(body, 0, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotInStringConst2()
        {
            var body = new List<string> { "string str = \"text here\";  " };
            var result = Parser.IsStringConst(body, 0, 28);
            Assert.IsFalse(result);
        }
    }
}
