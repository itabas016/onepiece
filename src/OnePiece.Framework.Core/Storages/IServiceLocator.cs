using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    /// <summary>
    /// Get the service db context dynamically
    /// </summary>
    public interface IServiceLocator
    {
        IEnumerable<IContextConnection> GetConnections();
    }
}
