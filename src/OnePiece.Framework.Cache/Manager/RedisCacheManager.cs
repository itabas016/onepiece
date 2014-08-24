using OnePiece.Framework.Core;
using OnePiece.Framework.RedisMapper;
using ServiceStack.Redis;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Cache
{
    /// <summary>
    /// remember:
    ///     not the cache client, mixed client instead.
    /// </summary>
    public sealed class RedisCacheManager : ICacheManager
    {
        internal const string NULL_DATA = "$$NULL$$";
        internal const string NULL_DATA_GET = "\"$$NULL$$\"";

        #region Redis Client instance

        internal static IRedisClientsManager _redisClientManager;
        public static IRedisClientsManager RedisClientManager
        {
            get
            {
                if (_redisClientManager == null)
                {
                    // force to use the framework's cache client manager
                    _redisClientManager =
                        RedisClientManagerFactory.Instance.MixedClientManager;
                }

                return _redisClientManager;
            }
        }

        public ILogService LogService
        {
            get
            {
                if (_logService == null)
                    _logService = ObjectFactory.GetInstance<ILogService>();

                return _logService;
            }
            set
            {
                _logService = value;
            }
        } private ILogService _logService;

        #endregion

        public void Add(string key, object value)
        {
            if (value == null) value = NULL_DATA;

            RedisAction(x => x.Add(key, value));
        }

        public void Add(string key, object value, int timeoutSeconds)
        {
            RedisAction(x =>
            {
                var span = new TimeSpan(0, 0, timeoutSeconds);

                if (value == null) value = NULL_DATA;

                if (timeoutSeconds <= 0) x.Add(key, value);
                else x.Add(key, value, span);
            });
        }

        public void Add(string key, object value, DateTime? absoluteExpiration)
        {
            Add(key, value, absoluteExpiration - DateTime.Now);
        }

        public void Add(string key, object value, TimeSpan? slidingExpiration)
        {
            RedisAction(x =>
            {
                if (slidingExpiration != null)
                {
                    x.Add(key, value, slidingExpiration.GetValueOrDefault());
                }
                else
                {
                    x.Add(key, value);
                }
            });
        }

        public bool Contains(string key)
        {
            var ret = RedisAction<bool>(x => x.ContainsKey(key), () => RedisClientManager.GetReadOnlyClient());

            return ret;
        }

        public object Get(string key, Type dataType)
        {
            var ret = RedisAction<object>(x =>
            {
                var value = default(object);
                var rawValue = x.GetValue(key);
                if (!rawValue.EqualsOrdinalIgnoreCase(NULL_DATA_GET))
                {
                    value = ServiceStack.Text.JsonSerializer.DeserializeFromString(rawValue, dataType);
                }

                return value;
            },
                () => RedisClientManager.GetReadOnlyClient());

            return ret;
        }

        public T Get<T>(string key)
        {
            return (T)Get(key, typeof(T));
        }

        public void Remove(string key)
        {
            RedisAction(x =>
            {
                x.Remove(key);
                //x.DecrementValue(COUNT);
            });
        }

        public void Clear(string prefix = "SVC")
        {
            RedisAction(x => x.RemoveAll(x.SearchKeys(prefix + "*")));
        }

        public void Flush()
        {
            RedisAction(x => x.FlushAll());
        }

        public void RedisAction(Action<IRedisClient> action, Func<IRedisClient> client = null)
        {
            try
            {
                using (var redisClient = client == null ? RedisClientManager.GetClient() : client())
                {
                    action(redisClient);
                }
            }
            catch (RedisException ex)
            {
                LogService.Error(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public T RedisAction<T>(Func<IRedisClient, T> action, Func<IRedisClient> client = null)
        {
            var ret = default(T);
            try
            {
                using (var redisClient = client == null ? RedisClientManager.GetClient() : client())
                {
                    ret = action(redisClient);
                }
            }
            catch (RedisException ex)
            {
                LogService.Error(string.Format("{0}\r\n{1}", ex.Message, ex.StackTrace));
            }

            return ret;
        }
    }
}
