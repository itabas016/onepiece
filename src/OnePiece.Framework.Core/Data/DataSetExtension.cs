using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core.Data
{
    public static class DataSetExtension
    {
        /// <summary>
        /// The poroperty must have sql column attribute on it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static List<T> ToModel<T>(this DataSet ds)
            where T : new()
        {
            var models = new List<T>();
            if (ds != null && ds.Tables.Count == 1 && ds.Tables[0].Rows.Count > 0)
            {
                var props = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                    .Where(x => x.GetCustomAttributes(typeof(SqlColumnAttribute), true).Any());

                var dic = new Dictionary<string, PropertyInfo>();
                foreach (var p in props)
                {
                    var sqlCol = p.GetCustomAttributes(typeof(SqlColumnAttribute), true).First() as SqlColumnAttribute;
                    var name = p.Name;
                    if (sqlCol != null && !sqlCol.Name.IsNullOrEmpty()) name = sqlCol.Name;

                    dic[name] = p;
                }

                var switchType = GetSwitch();

                var columns = ds.Tables[0].Columns;

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    var model = new T();

                    foreach (var p in dic)
                    {
                        if (!columns.Contains(p.Key)) continue;

                        var dbValue = row[p.Key];
                        object modelValue = null;

                        if (p.Value.PropertyType.IsEnum)
                        {
                            modelValue = Enum.ToObject(p.Value.PropertyType, dbValue.ToString().ToInt32());
                        }
                        else
                        {
                            modelValue = switchType.Switch(p.Value.PropertyType, dbValue);
                        }

                        if (modelValue != null)
                            p.Value.SetValue(model, modelValue, null);
                    }

                    models.Add(model);
                }
            }

            return models;
        }

        private static TypeSwitch GetSwitch()
        {
            var @switch = new TypeSwitch();
            @switch.Case<int>(x => x.ToString().ToInt32());

            @switch.Case<long>(x => x.ToString().ToInt64());

            @switch.Case<float>(x =>
            {
                var f = 0f;
                float.TryParse(x.ToString(), out f);

                return f;
            });

            @switch.Case<double>(x =>
            {
                var d = 0d;
                double.TryParse(x.ToString(), out d);

                return d;
            });

            @switch.Case<string>(x => x.ToString());

            @switch.Case<bool>(x =>
            {
                var b = false;
                if (x.ToString() == "1") b = true;
                else
                {
                    bool.TryParse(x.ToString(), out b);
                }

                return b;
            });

            @switch.Case<DateTime>(x =>
            {
                var dt = DateTime.MinValue;
                DateTime.TryParse(x.ToString(), out dt);

                return dt;
            });

            return @switch;
        }
    }
}
