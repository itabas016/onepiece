using OnePiece.Framework.Core;
using SubSonic.SqlGeneration.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    public class IndexAssistant
    {
        public static string Generate(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var models = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(EntityBase)) && !x.IsAbstract).ToList();

            var indexes = new List<SubSonicIndexItem>();
            if (models.Any())
            {
                models.ForEach(t =>
                {
                    var tblName = GetTableName(t);
                    var dbName = GetDatabaseName(t);

                    var columns = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    columns.ToList().ForEach(c =>
                    {
                        var attri = c.GetCustomAttributes(typeof(SubSonicIndexAttribute), true).FirstOrDefault() as SubSonicIndexAttribute;
                        if (attri != null)
                        {
                            var sb = new StringBuilder();
                            var colName = GetColumnName(c);

                            var indexName = "ix_{0}_{1}".FormatWith(tblName, colName).Lower();
                            if (!attri.Name.IsNullOrEmpty()) indexName = attri.Name.Lower();

                            var objName = tblName;
                            if (!attri.Schema.IsNullOrEmpty()) objName = attri.Schema + "." + tblName;

                            sb.AppendFormat("IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = object_id('{0}') AND NAME ='{1}')", objName, indexName);
                            sb.AppendLine();

                            sb.AppendFormat("CREATE INDEX {2} ON [{0}]({1});", objName, colName, indexName);
                            sb.AppendLine();
                            sb.AppendFormat("GO");

                            sb.AppendLine();
                            sb.AppendLine();

                            var index = new SubSonicIndexItem();
                            index.Database = dbName;
                            index.IndexName = indexName;
                            index.Script = sb.ToString();
                            index.TableName = tblName;

                            indexes.Add(index);
                        }
                    });
                });

            }

            indexes = indexes.OrderBy(x => x.Database).ThenBy(x => x.TableName).ToList();

            var scripts = new StringBuilder();
            var dbname = string.Empty;
            foreach (var i in indexes)
            {
                if (dbname != i.Database)
                {
                    scripts.AppendFormat("---------{0}---------", i.Database);
                    scripts.AppendLine();
                    scripts.AppendLine("use {0};".FormatWith(i.Database));

                    dbname = i.Database;
                }

                scripts.AppendLine(i.Script);
            }

            return scripts.ToString();
        }

        private static string GetTableName(Type t)
        {
            var tableName = t.Name;

            var tblAttri = t.GetCustomAttributes(typeof(SubSonicTableNameOverrideAttribute), false).FirstOrDefault() as SubSonicTableNameOverrideAttribute;
            if (tblAttri != null)
            {
                tableName = tblAttri.TableName;
            }

            return tableName;
        }

        private static string GetDatabaseName(Type t)
        {
            var db = string.Empty;

            var tblAttri = t.GetCustomAttributes(typeof(SubSonicDatabaseAttribute), false).FirstOrDefault() as SubSonicDatabaseAttribute;
            if (tblAttri != null)
            {
                db = tblAttri.Database;
            }

            return db;
        }

        private static string GetColumnName(PropertyInfo c)
        {
            var colName = c.Name;

            var colAttri = c.GetCustomAttributes(typeof(SubSonicColumnNameOverrideAttribute), true).FirstOrDefault() as SubSonicColumnNameOverrideAttribute;
            if (colAttri != null)
            {
                colName = colAttri.ColumnName;
            }

            return colName;
        }
    }
}
