using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnePiece.Framework.Core
{
    public static class BasicControllerExtension
    {
        public static string AreaName<T>(this T controller) where T : Controller, IBasicController
        {
            return SingletonBase<BasicControllerAreaRepo>.Instance.GetArea<T>();
        }
    }
}
