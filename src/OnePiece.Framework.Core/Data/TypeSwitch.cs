using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Data
{
    internal class TypeSwitch
    {
        Dictionary<Type, Func<object, object>> matches = new Dictionary<Type, Func<object, object>>();

        public TypeSwitch Case<T>(Func<object, object> action)
        {
            matches.Add(typeof(T), x => action(x));
            return this;
        }

        public object Switch(Type type, object x)
        {
            var ret = default(object);

            if (x != null && x != DBNull.Value)
            {
                if (type.IsGenericType)
                {
                    type = type.GetGenericArguments()[0];
                }

                if (matches.ContainsKey(type))
                    ret = matches[type](x);
            }

            return ret;
        }

        public object Switch(object x)
        {
            var ret = default(object);

            if (x != null && x != DBNull.Value)
                ret = matches[x.GetType()](x);

            return ret;
        }
    }
}
