using Castle.DynamicProxy;
using OnePiece.Framework.Core;
using Snap;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Cache
{
    public class ServiceCacheInterceptor : MethodInterceptor
    {
        public override void InterceptMethod(IInvocation invocation, System.Reflection.MethodBase method, Attribute attribute)
        {
            var svcCacheAttribute = attribute as ServiceCacheAttribute;

            var cacheable = invocation.InvocationTarget as ICacheable;
            var allowCache = true;
            if (cacheable != null) { allowCache = cacheable.AllowCache; }

            if (svcCacheAttribute.HasNoCache || !allowCache)
            {
                invocation.Proceed();
                return;
            }

            var cacheKey = this.GetCacheKey(invocation);
            var cacheManager = ObjectFactory.GetInstance<ICacheManager>();

            var parameters = invocation.Method.GetParameters();
            if (cacheManager.Contains(cacheKey))
            {
                // get the value from the cache
                invocation.ReturnValue = cacheManager.Get(cacheKey, invocation.Method.ReturnType);

                GetParameter(invocation, cacheKey, cacheManager, parameters);
            }
            else
            {
                // handle the real method
                invocation.Proceed();

                // add to the cache
                cacheManager.Add(cacheKey, invocation.ReturnValue, svcCacheAttribute.TimeoutInSeconds);

                // Cache the argument as well
                AddParameter(invocation, svcCacheAttribute, cacheKey, cacheManager, parameters);
            }
        }

        #region Parameter Cache

        private static void AddParameter(IInvocation invocation, ServiceCacheAttribute svcCacheAttribute, string cacheKey, ICacheManager cacheManager, System.Reflection.ParameterInfo[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef && invocation.Arguments[i] != null)
                {
                    cacheManager.Add(cacheKey + parameters[i].Name, invocation.Arguments[i], svcCacheAttribute.TimeoutInSeconds);
                }
            }
        }

        private static void GetParameter(IInvocation invocation, string cacheKey, ICacheManager cacheManager, System.Reflection.ParameterInfo[] parameters)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType.IsByRef)
                {
                    var argVal = cacheManager.Get(cacheKey + parameters[i].Name, parameters[i].ParameterType.GetElementType());
                    invocation.SetArgumentValue(i, argVal);
                }
            }
        }
        #endregion

        public string GetCacheKey(IInvocation invocation)
        {
            var sb = new StringBuilder();
            var parameters = invocation.Method.GetParameters();
            var args = invocation.Arguments;
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var p = args[i];
                    var cacheName = string.Empty;
                    if (p != null)
                    {
                        cacheName = p.ToString();

                        var cacheObj = p as ISnapCache;
                        if (cacheObj != null)
                            cacheName = cacheObj.BuildCacheKey();
                    }

                    sb.AppendFormat("{0}[{1}]", parameters[i].Name, cacheName);
                }
            }

            var cachekey = string.Format("SVC:{0}.{1}:{2}", invocation.TargetType.Name, invocation.Method.Name, sb.ToString().SHA1Hash());

            return cachekey;
        }
    }
}
