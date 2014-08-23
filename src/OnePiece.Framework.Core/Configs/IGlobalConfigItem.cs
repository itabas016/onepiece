using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public interface IGlobalConfigItem
    {
        string Module { get; set; }

        string Name { get; set; }

        string Key { get; set; }

        string Value { get; set; }
    }
}
