using OnePiece.Framework.Core;
using SubSonic.SqlGeneration.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    public class CommentAssistant
    {
        public static string Generate(string assemblyName)
        {
            var assembly = Assembly.Load(assemblyName);
            var models = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(EntityBase)) && !x.IsAbstract).ToList();

            var comments = new List<SubSonicColumnComment>();
            if (models.Any())
            {
                models.ForEach(t =>
                {
                    var tblName = GetTableName(t);
                    var dbName = GetDatabaseName(t);

                    var columns = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    columns.ToList().ForEach(c =>
                    {
                        var attri = c.GetCustomAttributes(typeof(DisplayAttribute), true).FirstOrDefault() as DisplayAttribute;
                        if (attri != null)
                        {
                            var sb = new StringBuilder();
                            var colName = GetColumnName(c);

                            var content = colName;
                            if (!attri.Name.IsNullOrEmpty()) content = attri.Name;

                            sb.AppendFormat("EXEC sp_dropextendedproperty @name = N'MS_Description',@level0type = N'Schema', @level0name = 'dbo',@level1type = N'Table', @level1name = '{0}', @level2type = N'Column', @level2name = '{1}'", tblName, colName);
                            sb.AppendLine();

                            sb.AppendFormat("EXEC sp_addextendedproperty @name = N'MS_Description', @value = '{0}',@level0type = N'Schema', @level0name = 'dbo',@level1type = N'Table', @level1name = '{1}', @level2type = N'Column', @level2name = '{2}'", content, tblName, colName);
                            sb.AppendLine();
                            sb.AppendFormat("GO");

                            sb.AppendLine();

                            var comment = new SubSonicColumnComment();
                            comment.Database = dbName;
                            comment.TableName = tblName;
                            comment.ColumnName = colName;
                            comment.Comment = sb.ToString();

                            comments.Add(comment);
                        }
                    });
                });

            }

            comments = comments.OrderBy(x => x.Database).ThenBy(x => x.TableName).ThenBy(x => x.ColumnName).ToList();

            var scripts = new StringBuilder();
            var dbname = string.Empty;
            foreach (var i in comments)
            {
                if (dbname != i.Database)
                {
                    scripts.AppendFormat("---------{0}---------", i.Database);
                    scripts.AppendLine();
                    scripts.AppendLine("use {0};".FormatWith(i.Database));

                    dbname = i.Database;
                }

                scripts.AppendLine(i.Comment);
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
