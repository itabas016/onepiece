using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class BootHeart : SingletonBase<BootHeart>
    {
        internal const string WEB_ASSEMBLY = "OnePiece.Framework.Web";
        public void BootSelf()
        {
            Boot(WEB_ASSEMBLY);
        }

        public void Boot(string assemblyString)
        {
            if (assemblyString.IsNullOrEmpty()) return;

            try
            {
                var assembly = Assembly.Load(assemblyString);
                var bootable = typeof(IBootable);
                var bootItems = assembly.GetTypes().Where(x => bootable.IsAssignableFrom(x) && !x.IsAbstract).ToList();

                if (bootItems.Any())
                {
                    var bootsInOrder = new List<IBootable>();
                    bootItems.ForEach(t =>
                    {
                        var instance = ObjectFactory.GetInstance(t) as IBootable;

                        if (instance != null)
                        {
                            bootsInOrder.Add(instance);
                        }
                    });

                    bootsInOrder.OrderBy(x => x.Order).ToList()
                        .ForEach(s => s.Startup());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
