using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.RedisMapper
{
    public interface IRedisCustomProperty
    {
        object Value { get; set; }
        bool IsQueriable { get; set; }
    }
}
