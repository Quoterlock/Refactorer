using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactorer
{
    public class FunctionHeader
    {
        public string Name { get; set; }
        public string ReturnValue { get; set; }
        public Dictionary<string, string> Parameters { get;set; }

        public int RowInText { get; set; }

        // public string ClassName {get;set;}

        public FunctionHeader Convert(string header, int row)
        {
            var separators = new char[] { '*', '/', ',', ' ', '.', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']', '\t', '\r', '\n' };
            var words = header.Split(separators).ToList();
            words.RemoveAll(i => i == string.Empty);

            var functionHeader = new FunctionHeader();
            functionHeader.RowInText = row;
            functionHeader.ReturnValue = words[0];
            functionHeader.Name = words[1];
            functionHeader.Parameters = new Dictionary<string, string>();
            for (int i = 2; i < words.Count; i += 2)
                functionHeader.Parameters.Add(words[i + 1], words[i]);

            return functionHeader;
        }

        public override string ToString()
        {
            
            string parameters = string.Empty;
            if(Parameters != null && Parameters.Count > 0)
            {
                foreach (var parameter in this.Parameters)
                    parameters += parameter.Value + " " + parameter.Key + ",";
                parameters = parameters.Substring(0, parameters.Length - 1); // remove last coma.
            }
            return this.ReturnValue + " " + this.Name + "(" + parameters + ")";
        }
    }
}
