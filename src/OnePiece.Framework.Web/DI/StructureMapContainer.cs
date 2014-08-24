using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class StructureMapContainer : IocContainer
    {
        public T GetInstance<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}
