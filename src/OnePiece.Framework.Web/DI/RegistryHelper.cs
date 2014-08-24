using OnePiece.Framework.Cache;
using OnePiece.Framework.Core;
using Snap;
using Snap.StructureMap;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class RegistryHelper
    {
        public static void Register(string assemblyName, string snapIncludeNamespaces = "")
        {
            // register self ioc
            if (SingletonBase<IocFacotry>.Instance.Container == null)
                SingletonBase<IocFacotry>.Instance.Container = new StructureMapContainer();

            // boot itself
            SingletonBase<BootHeart>.Instance.BootSelf();

            // structure map dependency injection registry
            RegisterDIRegistry(assemblyName);

            SingletonBase<BootHeart>.Instance.Boot(assemblyName);

            // auto mapper
            RegisterMapper(assemblyName);

            // snap
            RegisterSnap(snapIncludeNamespaces);

        }

        internal static void RegisterDIRegistry(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var bootItems = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(BasicRegistry)) && !x.IsAbstract).ToList();

            if (bootItems.Any())
            {
                bootItems.ForEach(t =>
                {
                    var instance = Activator.CreateInstance(t) as Registry;

                    if (instance != null)
                    {
                        ObjectFactory.Configure(x => x.AddRegistry(instance));
                    }
                });
            }
        }

        internal static void RegisterMapper(string assembly)
        {
            SingletonBase<EntityMapping>.Instance.Register(assembly);
        }

        internal static void RegisterSnap(string nameSpace)
        {
            if (!nameSpace.IsNullOrEmpty() && ConfigReservedKeys.SNAP_CACHE.ConfigValue().ToBoolean())
            {
                SnapConfiguration.For<StructureMapAspectContainer>(c =>
                {
                    c.IncludeNamespace(nameSpace);
                    c.Bind<ServiceCacheInterceptor>().To<ServiceCacheAttribute>();
                });
            }
        }
    }
}
