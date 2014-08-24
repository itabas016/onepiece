using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class CoreRegistry : BasicRegistry
    {
        public override void Register()
        {
            For<IRequestRepository>().Use<RequestRepository>();
            //For<IRandomService>().Use<RandomService>();
            //For<IRedisService>().Use<RedisService>();
        }
    }
}
