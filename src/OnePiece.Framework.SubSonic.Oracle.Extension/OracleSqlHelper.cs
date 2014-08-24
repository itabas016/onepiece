using OnePiece.Framework.Core;
using OnePiece.Framework.Core.Data;
using SubSonic.Oracle.DataProviders;
using SubSonic.Oracle.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic.Oracle
{
    public static class OracleSqlHelper
    {
        public static List<T> Execute<T>(this IDbContext context, string sql, Action<QueryCommand> addParams = null)
            where T : new()
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return Execute<T>(context.ConnectionStringName, sql, addParams);
            }

            return new List<T>();
        }

        public static List<T> Execute<T>(string connectionStringName, string sql, Action<QueryCommand> addParams)
            where T : new()
        {
            var provider = ProviderFactory.GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);

            if (addParams != null)
            {
                addParams(queryCommand);
            }

            var ds = provider.ExecuteDataSet(queryCommand);

            var models = ds.ToModel<T>();

            return models;
        }

        public static int ExecuteQuery(this IDbContext context, string sql, Action<QueryCommand> addParams = null)
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return ExecuteQuery(context.ConnectionStringName, sql, addParams);
            }

            return -1;
        }

        public static int ExecuteQuery(string connectionStringName, string sql, Action<QueryCommand> addParams)
        {
            var provider = ProviderFactory.GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);

            if (addParams != null)
            {
                addParams(queryCommand);
            }

            var ret = provider.ExecuteQuery(queryCommand);

            return ret;
        }

        public static T ExecuteSingle<T>(this IDbContext context, string sql, Action<QueryCommand> addParams = null) where T : new()
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return ExecuteSingle<T>(context.ConnectionStringName, sql, addParams);
            }

            return default(T);
        }

        public static T ExecuteSingle<T>(string connectionStringName, string sql, Action<QueryCommand> addParams = null) where T : new()
        {
            var provider = ProviderFactory.GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);

            if (addParams != null)
            {
                addParams(queryCommand);
            }

            var ret = provider.ExecuteSingle<T>(queryCommand);

            return ret;
        }

        public static object ExecuteScala(this IDbContext context, string sql, Action<QueryCommand> addParams = null)
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return ExecuteScala(context.ConnectionStringName, sql, addParams);
            }

            return default(object);
        }

        public static object ExecuteScala(string connectionStringName, string sql, Action<QueryCommand> addParams = null)
        {
            var provider = ProviderFactory.GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);
            if (addParams != null)
            {
                addParams(queryCommand);
            }

            var ret = provider.ExecuteScalar(queryCommand);

            return ret;
        }
    }
}
