using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.RedisMapper
{
    public interface IRedisModelWithSubModel
    {
        Dictionary<string, object> CustomProperties { get; set; }
    }
}
