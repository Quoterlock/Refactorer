using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorer
{
    public class FunctionHeader
    {
        public string Name { get; set; }
        public string ReturnValue { get; set; }
        public Dictionary<string, string> Parameters { get;set; }

        public int RowInText { get; set; }

        // public string ClassName {get;set;}
    }
}
