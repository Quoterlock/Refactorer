using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSharpProjectPractFirst.Model
{
    public class Refactor
    {
        public static string GetMethod(string code, string type, string name, List<string> parameters, string methodBody)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Code must not be null or empty");
            }

            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Type must not be null or empty");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null or empty");
            }

            if (parameters is null)
            {
                throw new ArgumentException("Parameters must not be null or empty");
            }

            string methodText = $"{type} {name}(";
            if (!parameters[0].Equals("void void"))
            {
                methodText += string.Join(",", parameters);
            }
            methodText += $"){{{Environment.NewLine}{methodBody}{Environment.NewLine}}}{Environment.NewLine}{Environment.NewLine}";


            List<string> splitParameters = new List<string>();

            foreach (var param in parameters)
            {
                splitParameters.Add(param.Split(' ')[1]);
            }

            string newCode = methodText + code.Replace(methodBody, $"{name}({string.Join(",", splitParameters)});");


            return newCode;
        }

        public static string EmbedMethod(string code, string methodName)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("Code must not be null or empty");
            }

            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentException("Method name must not be null or empty");
            }

            if (code.Equals(methodName))
            {
                throw new ArgumentException("Code and method name must bot be equal");
            }

            if (!code.Contains(methodName))
            {
                throw new ArgumentException("Code does not contain method name");
            }

            string pattern = $@"\b(void|int|char|double|float|string)\s+\b{methodName}\s*\([^)]*\)\s*{{[^}}]*}}";

            MatchCollection matches = Regex.Matches(code, pattern, RegexOptions.Singleline);

            string functionBody = ExtractFunctionBody(matches[0].Value);

            code = code.Replace(matches[0].Value, "");

           // code = code.Replace(methodName, functionBody);

            string pat = $@"{methodName}\s*\([^)]*\);\s*";
            MatchCollection mat = Regex.Matches(code, pat, RegexOptions.Singleline);
            foreach (Match match in mat)
            {
                if (match.Success)
                {
                    code = code.Replace(match.Value, functionBody);
                }
            }
            return code;
        }


        static string ExtractFunctionBody(string functionCode)
        {
            int openingBraceIndex = functionCode.IndexOf('{');
            int closingBraceIndex = functionCode.LastIndexOf('}');
            if (openingBraceIndex >= 0 && closingBraceIndex > openingBraceIndex)
            {
                return functionCode.Substring(openingBraceIndex + 1, closingBraceIndex - openingBraceIndex - 1);
            }

            return string.Empty;
        }
    }
}
