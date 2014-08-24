using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class RetrieveValueWithDefaultHelper
    {
        public static T TryGet<T>(Func<T> tryGet, Func<T> defalutValueFunc)
        {
            var value = default(T);

            try
            {
                value = tryGet();
            }
            catch
            {
            }
            if (value == null && defalutValueFunc != null)
            {
                value = defalutValueFunc();
            }

            return value;
        }
    }
}
