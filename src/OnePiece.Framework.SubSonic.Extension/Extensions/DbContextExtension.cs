using OnePiece.Framework.Core;
using SubSonic.Query;
using SubSonic.Schema;
using SubSonic.SqlGeneration.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    public static class DbContextExtension
    {
        public static IQueryable<T> All<T>(this IDbContext dbContext)
            where T : EntityBase, new()
        {
            return dbContext.DbContext.All<T>();
        }

        /// <summary>
        /// This method will return one queryable result instead of a result data after executing the sql text command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IDbContext dbContext, Expression<Func<T, bool>> expression)
            where T : EntityBase, new()
        {
            return dbContext.DbContext.All<T>().Where<T>(expression);
        }

        public static IList<T> Where<T>(this IDbContext dbContext, Expression<Func<T, bool>> dbSearchExpression, Predicate<T> inMemorySearchExpression)
            where T : EntityBase, new()
        {
            var resultSet = dbContext.DbContext.Find<T>(dbSearchExpression);
            if (resultSet != null)
            {
                return resultSet.ToList().FindAll(inMemorySearchExpression);
            }

            return new List<T>();
        }

        /// <summary>
        /// Be careful to use this method if the table has lots of records.
        /// If you want to filter by the expression, you can try Where<T> method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IList<T> All<T>(this IDbContext dbContext, Expression<Func<T, bool>> expression) where T : EntityBase, new()
        {
            return dbContext.DbContext.Find<T>(expression);
        }

        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="id"></param>
        /// <returns>Will return null if the entity with the id does not exist.</returns>
        public static T Single<T>(this IDbContext dbContext, int id) where T : EntityBase, new()
        {
            return dbContext.DbContext.Single<T>(id);
        }

        /// <summary>
        /// Get one entity by condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static T Single<T>(this IDbContext dbContext, Expression<Func<T, bool>> expression) where T : EntityBase, new()
        {
            return dbContext.DbContext.Single<T>(expression);
        }

        /// <summary>
        /// Add the entity to db.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbContext"></param>
        /// <param name="model"></param>
        /// <param name="addedAction"></param>
        /// <returns></returns>
        public static object Add<T>(this IDbContext dbContext, T model, Action<T> addedAction = null) where T : EntityBase, new()
        {
            if (model != null)
            {
                model.CreatedDate = DateTime.Now;
                var ret = dbContext.DbContext.Add<T>(model);

                if (addedAction != null)
                {
                    addedAction(model);
                }

                return ret;
            }

            return default(T);
        }

        public static void Add<T>(this IDbContext dbContext, IEnumerable<T> models) where T : EntityBase, new()
        {
            dbContext.DbContext.AddMany<T>(models);
        }

        public static int Update<T>(this IDbContext dbContext, T model, Action<T> updatedAction) where T : EntityBase, new()
        {
            var ret = dbContext.DbContext.Update<T>(model);

            if (updatedAction != null)
            {
                updatedAction(model);
            }

            return ret;
        }

        public static int Update<T>(this IDbContext dbContext, T model, bool getOrigin = true) where T : EntityBase, new()
        {
            if (model != null)
            {
                if (getOrigin)
                {
                    var origin = dbContext.Single<T>(model.Id);
                    if (origin != null)
                    {
                        model.LastModifiedDate = DateTime.Now;
                        model.CreatedDate = origin.CreatedDate;
                        return dbContext.DbContext.Update<T>(model);
                    }
                }
                else
                {
                    model.LastModifiedDate = DateTime.Now;
                    return dbContext.DbContext.Update<T>(model);
                }
            }
            return -1;
        }

        public static int Update<T>(this IDbContext dbContext, IEnumerable<T> models) where T : EntityBase, new()
        {
            if (models != null && models.Any())
            {
                foreach (var model in models)
                {
                    model.LastModifiedDate = DateTime.Now;
                }
                return dbContext.DbContext.UpdateMany<T>(models);
            }

            return -1;
        }

        public static int Delete<T>(this IDbContext dbContext, int id, Action deletedAction = null) where T : EntityBase, new()
        {
            var ret = dbContext.DbContext.Delete<T>(id);

            if (deletedAction != null) deletedAction();

            return ret;
        }

        public static int Delete<T>(this IDbContext dbContext, IEnumerable<T> models) where T : EntityBase, new()
        {
            return dbContext.DbContext.DeleteMany<T>(models);
        }

        public static int Delete<T>(this IDbContext dbContext, Expression<Func<T, bool>> expression) where T : EntityBase, new()
        {
            return dbContext.DbContext.DeleteMany<T>(expression);
        }

        public static bool Exists<T>(this IDbContext context, Expression<Func<T, bool>> expression)
            where T : EntityBase, new()
        {
            return context.DbContext.Exists<T>(expression);
        }


        public static IList<T> Find<T>(this IDbContext context, Expression<Func<T, bool>> expression)
            where T : EntityBase, new()
        {
            return context.DbContext.Find<T>(expression);
        }

        public static PagedList<T> GetPaged<T>(this IDbContext context, int pageIndex, int pageSize)
            where T : EntityBase, new()
        {
            return context.DbContext.GetPaged<T>(pageIndex, pageSize);
        }

        public static PagedList<T> GetPaged<T>(this IDbContext context, string sortBy, int pageIndex, int pageSize)
            where T : EntityBase, new()
        {
            return context.DbContext.GetPaged<T>(sortBy, pageIndex, pageSize);
        }

        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int Truncate<T>(this IDbContext context) where T : EntityBase, new()
        {
            var provider = SqlQuery.GetProvider(context.ConnectionStringName);

            var tableName = typeof(T).Name;

            var tblAttri = typeof(T).GetCustomAttributes(typeof(SubSonicTableNameOverrideAttribute), false).FirstOrDefault() as SubSonicTableNameOverrideAttribute;
            if (tblAttri != null)
            {
                tableName = tblAttri.TableName;
            }
            var sqlCmd = string.Empty;

            switch (provider.Name.ToLower())
            {
                case "mysql.data.mysqlclient":
                    sqlCmd = string.Format("truncate table {0};", tableName);
                    break;
                default:
                    sqlCmd = string.Format("IF OBJECT_ID('{0}') IS NOT NULL truncate table [{0}]", tableName);
                    break;
            }

            var queryCommand = new QueryCommand(sqlCmd, provider);
            var ret = 0;
            try
            {
                ret = provider.ExecuteQuery(queryCommand);
            }
            catch { }

            return ret;
        }

        public static int Count<T>(this IDbContext context) where T : EntityBase, new()
        {
            var provider = SqlQuery.GetProvider(context.ConnectionStringName);

            var tableName = typeof(T).Name;

            var tblAttri = typeof(T).GetCustomAttributes(typeof(SubSonicTableNameOverrideAttribute), false).FirstOrDefault() as SubSonicTableNameOverrideAttribute;
            if (tblAttri != null)
            {
                tableName = tblAttri.TableName;
            }

            var sqlCmd = string.Format("SELECT COUNT(*) FROM {0}", tableName);
            var queryCommand = new QueryCommand(sqlCmd, provider);
            var result = provider.ExecuteDataSet(queryCommand);
            int count = 0;
            if (result != null)
            {
                if (result.Tables.Count >= 0 && result.Tables[0].Rows.Count >= 0 && result.Tables[0].Rows[0].ItemArray.Length >= 0)
                {
                    int.TryParse(result.Tables[0].Rows[0][0].ToString(), out count);
                }
            }
            return count;
        }

        public static List<T> Top<T, TOrder>(this IDbContext dbContext, int topCount, Expression<Func<T, bool>> where, bool isDesc, params Expression<Func<T, TOrder>>[] orderByColumnExps)
            where T : EntityBase, new()
        {
            var result = dbContext.DbContext.All<T>().Where<T>(where);
            if (result != null)
            {
                var isFirst = true;
                foreach (var orderByColumnExp in orderByColumnExps)
                {
                    if (isFirst)
                    {
                        result = isDesc ? result.OrderByDescending(orderByColumnExp) : result.OrderBy(orderByColumnExp);
                        isFirst = false;
                    }
                    else
                    {
                        result = isDesc ? ((IOrderedQueryable<T>)result).ThenByDescending(orderByColumnExp)
                            : ((IOrderedQueryable<T>)result).ThenBy(orderByColumnExp);
                    }
                }
                result = result.Take(topCount);
                return result.ToList();
            }
            else
            {
                return new List<T>();
            }
        }

        public static T Save<T>(this IDbContext dbContext, Expression<Func<T, bool>> expression, T model)
            where T : EntityBase, new()
        {
            var existed = dbContext.DbContext.Single<T>(expression);
            if (existed == null)
            {
                dbContext.DbContext.Add<T>(model);
            }
            else
            {
                model.LastModifiedDate = DateTime.Now;
                model.Id = existed.Id;
                dbContext.DbContext.Update<T>(model);
            }

            return model;
        }

        public static IList<T> SelectByIds<T>(this IDbContext dbContext, IEnumerable<int> ids, string tblName, string colName, int queryCount = 30)
            where T : new()
        {
            var ret = new List<T>();
            if (ids != null && ids.Any())
            {
                var totalCount = ids.Count();

                var batches = totalCount / queryCount + 1;
                for (int i = 0; i < batches; i++)
                {
                    var skipCount = i * queryCount;

                    var idStr = ids.Skip(skipCount).Take(50).Concatenate(x => x.ToString());
                    var models = dbContext.Execute<T>(@"select * from {1} where {2} in ({0})".FormatWith(idStr, tblName, colName));

                    ret.AddRange(models);
                }
            }

            return ret;
        }

        /// <summary>
        /// Get to know what it is, MySql or ms sql?
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static DbType GetDbType(this IDbContext dbContext)
        {
            var type = DbType.MSSql;

            if (dbContext != null)
            {
                type = DbContextFactory.GetDbType(dbContext.ConnectionStringName);
            }

            return type;
        }
    }
}
