using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public interface IBootable
    {
        void Startup();

        int Order { get; }
    }
}
