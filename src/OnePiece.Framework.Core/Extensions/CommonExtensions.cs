using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class CommonExtensions
    {
        public static string NormalzieFileName(this string fileName)
        {
            return fileName.Replace("*", string.Empty).Replace(@"\", string.Empty).Replace("/", string.Empty).Replace(":", string.Empty).Replace("?", string.Empty).Replace("\"", string.Empty).Replace("<", string.Empty).Replace(">", string.Empty).Replace("|", string.Empty);
        }

        public static object MakeSureNotNull(this object instance)
        {
            var ret = instance == null ? new object() : instance;
            return ret;
        }
    }
}
