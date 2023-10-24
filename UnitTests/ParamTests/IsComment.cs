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
    public class IsComment
    {
        [TestMethod]
        public void InMultilineComment()
        {
            var lines = new List<string>{"/*", "text", "*/"};
            var result = Parser.IsComment(lines, 1, 2);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotInMultilineComment()
        {
            var lines = new List<string> { "/*", "*/", "text", "/*", "*/" };
            var result = Parser.IsComment(lines, 2, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotInMultilineComment2()
        {
            var lines = new List<string> { "*/", "text"};
            var result = Parser.IsComment(lines, 1, 2);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InMultilineCommentInOneLine()
        {
            var lines = new List<string> { "/*text*/" };
            var result = Parser.IsComment(lines, 0, 4);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InMultilineCommentInOneLine2()
        {
            var lines = new List<string> { "/* text" };
            var result = Parser.IsComment(lines, 0, 4);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void InMultilineCommentInOneLine3()
        {
            var lines = new List<string> { "*/ text" };
            var result = Parser.IsComment(lines, 0, 4);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InMultilineCommentInOneLine4()
        {
            var lines = new List<string> { "*/ text /*" };
            var result = Parser.IsComment(lines, 0, 4);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void InLineComment()
        {
            var lines = new List<string> { "// text" };
            var result = Parser.IsComment(lines, 0, 5);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotInLineComment()
        {
            var lines = new List<string> { "//","text //" };
            var result = Parser.IsComment(lines, 1, 2);
            Assert.IsFalse(result);
        }
    }
}
