using AutoMapper;
using OnePiece.Framework.Core;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Mappers;

namespace OnePiece.Framework.Core
{
    public class EntityMapping : SingletonBase<EntityMapping>
    {
        public void Register(string assemblyString)
        {
            var assembly = Assembly.Load(assemblyString);
            var mapable = typeof(IEntityMapper);

            var mappers = assembly.GetTypes().Where(x => mapable.IsAssignableFrom(x) && !x.IsAbstract).ToList();

            if (mappers.Any())
            {
                mappers.ForEach(t =>
                {
                    var instance = ObjectFactory.GetInstance(t) as IEntityMapper;

                    if (instance != null)
                    {
                        instance.Config();
                    }
                });

                Mapper.AssertConfigurationIsValid();
            }
        }

        public static void ResetMapper()
        {
            //upgrade v5.0 this method is obsolete
            //Mapper.Reset();
            MapperRegistry.Reset();
        }

        public static T2 Auto<T1, T2>(T1 source, T2 defaultT2Value = default(T2))
        {
            if (source == null)
            {
                if (defaultT2Value != null) return defaultT2Value;
            }

            return Mapper.Map<T1, T2>(source);

        }

        //public static object Map<T1>(T1 source, Type destType)
        //{
        //    return Mapper.Map(source, typeof(T1), destType);
        //}

        //public static T2 Assign<T1, T2>(T1 source, T2 dest)
        //{
        //    return Mapper.Map<T1, T2>(source, dest);
        //}
    }
}
