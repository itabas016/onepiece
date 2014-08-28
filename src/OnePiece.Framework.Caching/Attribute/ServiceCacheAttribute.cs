using OnePiece.Framework.Core;
using Snap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Caching
{
    public class ServiceCacheAttribute : MethodInterceptAttribute
    {
        public ServiceCacheAttribute()
        {

        }

        public ServiceCacheAttribute(int seconds)
            : this()
        {
            this._timeoutInSeconds = seconds;
        }

        private readonly int _timeoutInSeconds = ConfigReservedKeys.SNAP_CACHE_TIMEOUT_SECOND.ConfigValue().ToInt32();

        public int TimeoutInSeconds
        {
            get
            {
                return _timeoutInSeconds;
            }
        }

        public bool HasNoCache
        {
            get
            {
                return this.TimeoutInSeconds <= 0;
            }
        }
    }
}
