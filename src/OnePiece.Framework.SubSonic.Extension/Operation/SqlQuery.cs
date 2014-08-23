using OnePiece.Framework.Core;
using OnePiece.Framework.Core.Data;
using SubSonic.DataProviders;
using SubSonic.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    public static class SqlQuery
    {
        public const string SQL_PROVIDER_NAME = "System.Data.SqlClient";

        public static List<T> Execute<T>(this IDbContext context, string sql)
            where T : new()
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return Execute<T>(context.ConnectionStringName, sql);
            }

            return new List<T>();
        }

        public static List<T> Execute<T>(string connectionStringName, string sql)
            where T : new()
        {
            var provider = GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);
            var ds = provider.ExecuteDataSet(queryCommand);

            var models = ds.ToModel<T>();

            return models;
        }

        public static int ExecuteQuery(this IDbContext context, string sql)
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return ExecuteQuery(context.ConnectionStringName, sql);
            }

            return -1;
        }

        public static int ExecuteQuery(string connectionStringName, string sql)
        {
            var provider = GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);
            var ret = provider.ExecuteQuery(queryCommand);

            return ret;
        }

        public static T ExecuteSingle<T>(this IDbContext context, string sql) where T : new()
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return ExecuteSingle<T>(context.ConnectionStringName, sql);
            }

            return default(T);
        }

        public static T ExecuteSingle<T>(string connectionStringName, string sql) where T : new()
        {
            var provider = GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);
            var ret = provider.ExecuteSingle<T>(queryCommand);

            return ret;
        }

        public static object ExecuteScala(this IDbContext context, string sql)
        {
            if (context != null && !sql.IsNullOrEmpty())
            {
                return ExecuteScala(context.ConnectionStringName, sql);
            }

            return default(object);
        }

        public static object ExecuteScala(string connectionStringName, string sql)
        {
            var provider = GetProvider(connectionStringName);

            var queryCommand = new QueryCommand(sql, provider);
            var ret = provider.ExecuteScalar(queryCommand);

            return ret;
        }

        public static IDataProvider GetProvider(string connectionStringName)
        {
            var connectionString = ContextConnectionFactory.GetConnectionString(connectionStringName);
            var providerName = ContextConnectionFactory.GetProviderName(connectionStringName);

            var provider = ProviderFactory.GetProvider(connectionString, providerName);

            return provider;
        }
    }
}
