using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnePiece.Framework.Core
{
    public class BootStructureMap : BootItem
    {
        public override void Startup()
        {
            DependencyResolver.SetResolver(
                t =>
                {
                    try
                    {
                        return ObjectFactory.GetInstance(t);
                    }
                    catch
                    {
                        return null;
                    }

                },
                t => ObjectFactory.GetAllInstances<object>().Where(s => s.GetType() == t));



            ObjectFactory.Configure(x => x.AddRegistry(new CoreRegistry()));

        }

        public override int Order
        {
            get
            {
                return 1;
            }
        }
    }
}
