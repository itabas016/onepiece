using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class MapperExtensions
    {
        public static TOutput To<TIn, TOutput>(this TIn model, TOutput defaultT2Value = default(TOutput))
        {
            return EntityMapping.Auto<TIn, TOutput>(model, defaultT2Value);
        }

        /// <summary>
        /// If the model is null, and if you do not want to return the default output value,
        /// please use the upper method, To<TIn, TOutput>()
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static TOutput To<TOutput>(this object model)
        {
            if (model == null) return default(TOutput);

            return (TOutput)Mapper.Map(model, model.GetType(), typeof(TOutput));
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.Configuration.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }

    }
}
