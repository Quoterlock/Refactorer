using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Refactorer
{
    public class Parameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string DefaultValue { get; set; }

        public static Parameter Convert(string str)
        {
            var words = str.Split(new char[] { ' ', '='}).ToList();
            words.Remove(string.Empty);
            return new Parameter()
            {
                Type = words[0],
                Name = words[1],
                DefaultValue = (words.Count == 3)? words[2] : null
            };
        }
            
        public override string ToString()
        {
            string str = Type + " " + Name;
            if(DefaultValue != null)
                str += " = " + DefaultValue;
            return str;
        }
    }

    public class FunctionHeader
    {
        public string Name { get; set; }

        public string ReturnValue { get; set; }

        public List<Parameter> Parameters { get;set; }

        public int RowInText { get; set; }

        public string ClassName {get;set;}

        public FunctionHeader Convert(string header, int row)
        {
            var separators = new char[] { '*', '/', ',', ' ', '.', '=', '+', '-', '(', ')', '{', '}', ';', '[', ']', '\t', '\r', '\n' };
            var words = header.Split(separators).ToList();
            words.RemoveAll(i => i == string.Empty);

            var patternForFunc = @"(\w+)\s+(\w+)\(";
            Match match = Regex.Match(header, patternForFunc);

            var functionHeader = new FunctionHeader();
            functionHeader.RowInText = row;
            functionHeader.ReturnValue = match.Groups[1].Value;
            functionHeader.Name = match.Groups[2].Value;
            functionHeader.Parameters = new List<Parameter>();

            var patternForParams = @"\(([^)]*)\)";
            match = Regex.Match(header, patternForParams);
            var parameters = match.Value.Split(new char[] { ',', '(', ')'});
            foreach(var parameter in parameters)
            {
                if(parameter != string.Empty)
                    functionHeader.Parameters.Add(Parameter.Convert(parameter));
            }
            return functionHeader;
        }

        public override string ToString()
        {
            string parameters = string.Empty;
            if(Parameters != null && Parameters.Count > 0)
            {
                foreach (var parameter in this.Parameters)
                    parameters += parameter.ToString() + ", ";
                parameters = parameters.Substring(0, parameters.Length - 2); // remove last coma and space.
            }
            return this.ReturnValue + " " + this.Name + "(" + parameters + ")";
        }
    }
}
