using Microsoft.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnePiece.Framework.Core
{
    public static class ControllerExtensions
    {
        public static RedirectToRouteResult RedirectToAction<T>(this T controller, Expression<Action<T>> action)
            where T : Controller, IBasicController
        {
            return RedirectToActionInternal<T>(action, null);
        }

        public static RedirectToRouteResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action)
            where T : Controller, IBasicController
        {
            return RedirectToActionInternal<T>(action, null);
        }

        public static RedirectToRouteResult RedirectToAction<T>(this T controller, Expression<Action<T>> action, object values)
            where T : Controller, IBasicController
        {
            return RedirectToActionInternal<T>(action, new RouteValueDictionary(values));
        }

        public static RedirectToRouteResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action, object values)
            where T : Controller, IBasicController
        {
            return RedirectToActionInternal<T>(action, new RouteValueDictionary(values));
        }

        public static RedirectToRouteResult RedirectToAction<T>(this T controller, Expression<Action<T>> action, RouteValueDictionary values)
            where T : Controller, IBasicController
        {
            return RedirectToActionInternal<T>(action, values);
        }

        public static RedirectToRouteResult RedirectToAction<T>(this Controller controller, Expression<Action<T>> action, RouteValueDictionary values)
            where T : Controller, IBasicController
        {
            return RedirectToActionInternal<T>(action, values);
        }

        private static RedirectToRouteResult RedirectToActionInternal<T>(Expression<Action<T>> action, RouteValueDictionary values)
        {
            MethodCallExpression body = action.Body as MethodCallExpression;
            if (body == null)
                throw new InvalidOperationException("Expression must be a method call.");

            if (body.Object != action.Parameters[0])
                throw new InvalidOperationException("Method call must target lambda argument.");

            var actionName = body.Method.Name;
            var controllerName = typeof(T).Name;
            var area = SingletonBase<BasicControllerAreaRepo>.Instance.GetArea(typeof(T));

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);

            var parameters = LinkBuilder.BuildParameterValuesFromExpression(body);

            values = values ?? new RouteValueDictionary();
            values.Add("controller", controllerName);
            values.Add("action", actionName);

            if (!area.IsNullOrEmpty())
            {
                values.Add("area", area);
            }

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    values.Add(parameter.Key, parameter.Value);
                }
            }

            return new RedirectToRouteResult(values);

        }

        /// <summary>
        /// Determines whether the specified type is a controller
        /// </summary>
        /// <param name="type">Type to check</param>
        /// <returns>True if type is a controller, otherwise false</returns>
        public static bool IsController(Type type)
        {
            return type != null
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !type.IsAbstract
                   && typeof(IController).IsAssignableFrom(type);
        }
    }
}
