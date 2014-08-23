using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class IocFacotry : SingletonBase<IocFacotry>
    {
        public IocContainer Container { get; set; }
    }
}
