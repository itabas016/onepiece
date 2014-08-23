using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Cache
{
    /// <summary>
    /// For entity model, if you want to use SNAP,
    /// we need to build one cache key for the model
    /// </summary>
    public interface ISnapCache
    {
        string BuildCacheKey();
    }
}
