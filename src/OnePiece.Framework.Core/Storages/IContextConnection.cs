using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public interface IContextConnection
    {
        string Name { get; set; }

        string ConnectionString { get; set; }

        string ProviderName { get; set; }
    }
}
