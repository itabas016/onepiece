using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;

namespace OnePiece.Framework.Web
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString DisplayEnumTextFor<TModel, TResult>(this HtmlHelper<TModel> html, Expression<Func<TModel, TResult>> expression)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var enumType = metaData.ModelType;
            var enumValueName = metaData.Model.ToString();
            var display = metaData.SimpleDisplayText;

            var enumValue = Enum.Parse(enumType, enumValueName);
            var memberInfo = enumValue.GetType().GetMember(enumValueName).FirstOrDefault();

            if (memberInfo != null)
            {
                var attribute = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                if (attribute != null)
                {
                    display = attribute.Name;
                }
            }

            return MvcHtmlString.Create(display);
        }

        #region Dropdown List
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
           where TModel : class
        {
            return html.DropDownListFor(expression, null);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
            where TModel : class
        {
            TProperty value = html.ViewData.Model == null
                ? default(TProperty)
                : expression.Compile()(html.ViewData.Model);

            var selected = value == null ? String.Empty : value.ToString();

            return html.DropDownListFor(expression, CreateSelectList(expression.ReturnType, selected), htmlAttributes);
        }

        private static IEnumerable<SelectListItem> CreateSelectList(Type enumType, string selectedItem)
        {
            var list = (from object item in Enum.GetValues(enumType)
                        let fi = enumType.GetField(item.ToString())
                        let attribute = fi.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault() as DisplayAttribute
                        let title = attribute == null ? item.ToString() : attribute.Name
                        let willDisplay = attribute == null ? true : attribute.GetAutoGenerateField() == null ? true : attribute.GetAutoGenerateField().GetValueOrDefault()
                        where willDisplay == true
                        select new SelectListItem
                        {
                            Value = item.ToString(),
                            Text = title,
                            Selected = selectedItem == item.ToString()
                        }).ToList();

            return list;
        }
        #endregion

        public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        {
            var serializer = new JavaScriptSerializer();

            return MvcHtmlString.Create(serializer.Serialize(obj));
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, object obj, int recursionDepth)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;

            return MvcHtmlString.Create(serializer.Serialize(obj));
        }

        public static MvcHtmlString ToMambaTable(this Enum @enum, Func<string> keyTitle, Func<string> valueTitle = null, string tblStyle = "mambaTable")
        {
            var dic = @enum.ToDictionary();
            var sb = new StringBuilder();
            var titleOfValue = valueTitle == null ? "值" : valueTitle();
            var titleOfKey = keyTitle == null ? "描述" : keyTitle();

            sb.AppendLine(@"<div>
    <table class='{1}'>
        <tr>
            <td style='background-color:gray'>{0}</td>".FormatWith(titleOfKey, tblStyle));

            foreach (var kv in dic)
            {
                sb.AppendLine("<td>{0}</td>".FormatWith(kv.Value));
            }

            sb.AppendLine(@"</tr>
        <tr>
            <td style='background-color:gray'>{0}</td>".FormatWith(titleOfValue));

            foreach (var kv in dic)
            {
                sb.AppendLine("<td>{0}</td>".FormatWith(kv.Key));
            }

            sb.AppendLine(@"</tr>
    </table>
</div>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}
