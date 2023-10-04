using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorer.Exceptions
{
    [Serializable]
    public class MoreThanOneMethodExistException : Exception
    {
        public MoreThanOneMethodExistException() { }

        public MoreThanOneMethodExistException(string name)
            : base(String.Format("There are two classes that contan method {0}", name))
        {

        }
    }
}
