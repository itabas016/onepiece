using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.RedisMapper
{
    public class MasterOnlyRedisService : RedisService, IMasterOnlyRedisService
    {
        public override IRedisClientsManager RedisClientManager
        {
            get
            {
                return RedisClientManagerFactory.Instance.MasterOnlyClientManager;
            }
        }
    }
}
