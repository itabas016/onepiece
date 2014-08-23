using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.RedisMapper
{
    public class RedisConfigKeys
    {
        internal const string REDIS_READ_WRITE_SERVERS = "redis_read_write_servers";
        internal const string REDIS_READONLY_SERVERS = "redis_readonly_servers";
        internal const string MAX_WRITE_POOL_SIZE = "MaxWritePoolSize";
        internal const string MAX_READ_POOL_SIZE = "MaxReadPoolSize";

        internal const string CACHE_REDIS_READ_WRITE_SERVERS = "cache_redis_read_write_servers";
        internal const string CACHE_REDIS_READONLY_SERVERS = "cache_redis_readonly_servers";
        internal const string CACHE_MAX_WRITE_POOL_SIZE = "cache_MaxWritePoolSize";
        internal const string CACHE_MAX_READ_POOL_SIZE = "cache_MaxReadPoolSize";

        internal const string LOG_REDIS_READ_WRITE_SERVERS = "log_redis_read_write_servers";
        internal const string LOG_REDIS_READONLY_SERVERS = "log_redis_readonly_servers";
        internal const string LOG_MAX_WRITE_POOL_SIZE = "log_MaxWritePoolSize";
        internal const string LOG_MAX_READ_POOL_SIZE = "log_MaxReadPoolSize";

        internal const string ALLOWED_FILE_TYPES = "AllowedFileTypes";
    }
}
