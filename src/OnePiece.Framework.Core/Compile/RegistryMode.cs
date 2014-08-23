using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    [Flags]
    public enum RegistryMode
    {
        None,

        Debug = 1,

        Release = 2,

        Live = 4
    }
}
