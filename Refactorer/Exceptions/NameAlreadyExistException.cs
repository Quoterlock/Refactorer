using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactorer.Exceptions
{
    [Serializable]
    public class NameAlreadyExistException : Exception
    {
        public NameAlreadyExistException() { }

        public NameAlreadyExistException(string name)
            : base(String.Format("Name is alredy exist: {0}", name))
        {

        }
    }
}
