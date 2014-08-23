using OnePiece.Framework.Core;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.RedisMapper
{
    public class RedisClientManagerFactory : SingletonBase<RedisClientManagerFactory>
    {
        public IRedisClientsManager MixedClientManager
        {
            get
            {
                if (_mixedClientManager == null)
                {
                    _mixedClientManager = GetInstance();
                }

                return _mixedClientManager;
            }
        } IRedisClientsManager _mixedClientManager;

        public IRedisClientsManager MasterOnlyClientManager
        {
            get
            {
                if (_masterOnlyClientManager == null)
                {
                    _masterOnlyClientManager = GetInstance(true);
                }

                return _masterOnlyClientManager;
            }
        } IRedisClientsManager _masterOnlyClientManager;

        /// <summary>
        /// Cache client manager
        /// </summary>
        public IRedisClientsManager CacheClientManager
        {
            get
            {
                if (_cacheClientManager == null)
                {
                    _cacheClientManager = GetInstance(RedisConfigKeys.CACHE_MAX_WRITE_POOL_SIZE, RedisConfigKeys.CACHE_MAX_READ_POOL_SIZE, RedisConfigKeys.CACHE_REDIS_READ_WRITE_SERVERS, RedisConfigKeys.CACHE_REDIS_READONLY_SERVERS);
                }

                return _cacheClientManager;
            }
        } IRedisClientsManager _cacheClientManager;

        private IRedisClientsManager GetInstance(bool masterOnly = false)
        {
            #region Old
            //var redisClientManager = default(IRedisClientsManager);
            //var redisConfig = new RedisClientManagerConfig
            //{
            //    MaxWritePoolSize = AppConfigKeys.MAX_WRITE_POOL_SIZE.ConfigValue().ToInt32(),
            //    MaxReadPoolSize = AppConfigKeys.MAX_READ_POOL_SIZE.ConfigValue().ToInt32(),
            //    AutoStart = true
            //};


            //string[] readWriteHosts = AppConfigKeys.REDIS_READ_WRITE_SERVERS.ConfigValue().Split(';');
            //string[] readOnlyHosts = AppConfigKeys.REDIS_READONLY_SERVERS.ConfigValue().Split(';');

            //if (masterOnly)
            //{
            //    redisClientManager = new PooledRedisClientManager(readWriteHosts, readWriteHosts, redisConfig);
            //}
            //else
            //{
            //    redisClientManager = new PooledRedisClientManager(readWriteHosts, readOnlyHosts, redisConfig);
            //}

            //return redisClientManager; 
            #endregion

            if (masterOnly)
            {
                return GetInstance(RedisConfigKeys.MAX_WRITE_POOL_SIZE, RedisConfigKeys.MAX_READ_POOL_SIZE,
                    RedisConfigKeys.REDIS_READ_WRITE_SERVERS, RedisConfigKeys.REDIS_READ_WRITE_SERVERS);
            }

            return GetInstance(RedisConfigKeys.MAX_WRITE_POOL_SIZE, RedisConfigKeys.MAX_READ_POOL_SIZE,
                    RedisConfigKeys.REDIS_READ_WRITE_SERVERS, RedisConfigKeys.REDIS_READONLY_SERVERS);

        }


        public IRedisClientsManager GetInstance(string writePoolSizeKey, string readPoolSizeKey, string readWriteHostKey, string readOnlyHostKey)
        {
            var redisClientManager = default(IRedisClientsManager);
            var redisConfig = new RedisClientManagerConfig
            {
                MaxWritePoolSize = writePoolSizeKey.ConfigValue().ToInt32(),
                MaxReadPoolSize = readPoolSizeKey.ConfigValue().ToInt32(),
                AutoStart = true
            };


            string[] readWriteHosts = readWriteHostKey.ConfigValue().Split(ASCII.SEMICOLON_CHAR);
            string[] readOnlyHosts = readOnlyHostKey.ConfigValue().Split(ASCII.SEMICOLON_CHAR);

            redisClientManager = new PooledRedisClientManager(readWriteHosts, readWriteHosts, redisConfig);

            return redisClientManager;
        }

    }
}
