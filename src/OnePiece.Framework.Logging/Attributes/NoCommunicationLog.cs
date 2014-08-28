using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Logging
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoCommunicationLog : Attribute
    {
    }
}
