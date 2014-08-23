using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class EnumExtensions
    {
        public static Dictionary<int, string> ToDictionary(this Enum @enum, bool useDisplayName = true)
        {
            var type = @enum.GetType();
            var dic = Enum.GetValues(type).Cast<int>().ToDictionary(e => e, e => Enum.GetName(type, e));

            if (useDisplayName)
            {
                var enumNames = Enum.GetNames(type);

                foreach (var name in enumNames)
                {
                    var enumItem = Enum.Parse(type, name);

                    var memberInfo = enumItem.GetType().GetMember(name).FirstOrDefault();
                    if (memberInfo == null) continue;

                    var attribute = memberInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                    if (attribute != null)
                    {
                        //if (attribute.GetAutoGenerateField() != null) willAdd = attribute.GetAutoGenerateField().GetValueOrDefault();
                        dic[(int)enumItem] = attribute.Name;
                    }
                }
            }

            return dic;
        }

        public static string GetDisplay<T>(int key) where T : struct
        {
            return SingletonBase<EnumRepository>.Instance.GetDisplay<T>(key);
        }

        public static string GetDisplay<T>(this T key) where T : struct
        {
            if (typeof(T).IsEnum)
                return GetDisplay<T>(Convert.ToInt32(key));

            return string.Empty;
        }
    }
}
