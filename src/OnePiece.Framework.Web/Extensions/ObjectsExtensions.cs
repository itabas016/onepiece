using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class ObjectsExtensions
    {
        #region object Serialize to Json
        public static string Json(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        #endregion
    }
}
