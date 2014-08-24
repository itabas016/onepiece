using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnePiece.Framework.Core
{
    internal class BasicControllerAreaRepo : SingletonBase<BasicControllerAreaRepo>
    {
        Dictionary<Type, string> areaRepos = new Dictionary<Type, string>();

        internal string GetArea<T>() where T : Controller, IBasicController
        {
            var type = typeof(T);
            var area = string.Empty;

            if (this.areaRepos.ContainsKey(type))
            {
                area = this.areaRepos[type];
            }
            else
            {
                var controller = ObjectFactory.GetInstance<T>();
                if (controller != null)
                {
                    this.areaRepos[type] = controller.Area.IsNullOrEmpty() ? string.Empty : controller.Area;
                }
            }

            return area;
        }

        internal string GetArea(Type type)
        {
            var area = string.Empty;

            if (this.areaRepos.ContainsKey(type))
            {
                area = this.areaRepos[type];
            }
            else
            {
                if (typeof(IBasicController).IsAssignableFrom(type))
                {
                    var controller = ObjectFactory.GetInstance(type);
                    if (controller != null)
                    {
                        this.areaRepos[type] = ((IBasicController)controller).Area;
                        area = this.areaRepos[type];
                    }
                }
            }

            return area;
        }
    }
}
