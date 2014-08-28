using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Caching
{
    /// <summary>
    /// The target is for service.
    /// </summary>
    public interface ICacheable
    {
        bool AllowCache { get; set; }
    }
}
