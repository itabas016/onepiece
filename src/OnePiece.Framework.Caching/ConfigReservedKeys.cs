using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Caching
{
    /// <summary>
    /// Store the config keys for the framework level using
    /// </summary>
    public class ConfigReservedKeys
    {
        public const string SNAP_CACHE = "SnapCache";

        public const string SNAP_CACHE_TIMEOUT_SECOND = "SnapCacheTimeoutInSeconds";

        public const string MEMORY_CACHE_TIMEOUT_IN_SECONDS = "MemCacheTimeOutSeconds";
    }
}
