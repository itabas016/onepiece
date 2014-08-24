using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnePiece.Framework.Web
{
    public static class SelectListExtensions
    {
        public static SelectList GetSelectList<T>(T selected = default(T), Func<Dictionary<T, string>> i18nAction = null)
            where T : struct
        {
            var enumNames = Enum.GetNames(typeof(T));
            var items = new List<BasicSelectItem>();

            foreach (var name in enumNames)
            {
                var item = new BasicSelectItem();
                item.Id = name;
                item.Name = name;

                var enumItem = (T)Enum.Parse(typeof(T), name);

                var memberInfo = enumItem.GetType().GetMember(name).FirstOrDefault();
                if (memberInfo == null) continue;

                var willAdd = true;
                var attribute = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                if (attribute != null)
                {
                    if (attribute.GetAutoGenerateField() != null) willAdd = attribute.GetAutoGenerateField().GetValueOrDefault();

                    item.Name = attribute.Name;
                }

                if (willAdd) items.Add(item);

            }
            if (i18nAction != null)
            {
                #region replace names
                var dic = i18nAction();
                if (dic != null)
                {
                    foreach (var m in items)
                    {
                        var t = (T)Enum.Parse(typeof(T), m.Id);

                        if (dic.ContainsKey(t))
                        {
                            m.Name = dic[t];
                        }
                    }
                }
                #endregion
            }

            var list = new SelectList(items, "Id", "Name", Convert.ToInt32(selected));

            return list;
        }

        public static SelectList SelectList<T>(this T selected, Func<Dictionary<T, string>> i18nAction = null) where T : struct
        {
            return GetSelectList(selected, i18nAction);
        }

        public class BasicSelectItem
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }

        #region IEnumerable => SelectList

        public static SelectList SelectList<T>(this IEnumerable<T> collection, string keyField = "Id", string nameField = "Name", object selectedValue = null) where T : class, new()
        {
            if (collection == null) collection = new List<T>();

            return new SelectList(collection, keyField, nameField, selectedValue);
        }

        #endregion
    }
}
