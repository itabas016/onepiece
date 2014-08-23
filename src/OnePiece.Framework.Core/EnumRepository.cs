using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    internal class EnumRepository : SingletonBase<EnumRepository>
    {
        public Dictionary<Type, Dictionary<int, string>> Repository { get; set; }

        public EnumRepository()
        {
            this.Repository = new Dictionary<Type, Dictionary<int, string>>();
        }

        /// <summary>
        /// If the type is not enum will get string.Empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetDisplay<T>(int key)
        {
            var type = typeof(T);
            if (!type.IsEnum) return string.Empty;
            var ret = key.ToString();

            if (!this.Repository.ContainsKey(type))
            {
                var dic = new Dictionary<int, string>();
                var enumKey = key.ToEnum<T>(default(T));
                var enumNames = Enum.GetNames(typeof(T));

                foreach (var name in enumNames)
                {
                    var enumItem = (T)Enum.Parse(typeof(T), name);
                    var dicValue = name;

                    var memberInfo = enumItem.GetType().GetMember(name).FirstOrDefault();
                    if (memberInfo == null) continue;

                    var willAdd = true;
                    var attribute = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    if (attribute != null)
                    {
                        if (attribute.GetAutoGenerateField() != null) willAdd = attribute.GetAutoGenerateField().GetValueOrDefault();

                        dicValue = attribute.Name;
                    }

                    if (willAdd) dic[Convert.ToInt32(enumItem)] = dicValue;

                }

                this.Repository[type] = dic;

                dic.TryGetValue(key, out ret);

            }
            else
            {
                var dic = this.Repository[type];
                dic.TryGetValue(key, out ret);
            }

            return ret;
        }
    }
}
