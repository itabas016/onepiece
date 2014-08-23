using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class ParameterErrorException : Exception
    {
        public ParameterErrorException(string msg)
            : base(msg)
        {

        }

        public ParameterErrorException()
        {

        }
    }
}
