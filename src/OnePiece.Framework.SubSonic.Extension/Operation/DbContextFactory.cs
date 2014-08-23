using OnePiece.Framework.Core;
using SubSonic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    public class DbContextFactory
    {
        public const string MIGRATION_CONFIG_KEY = "EnableMigration";

        public static IRepository CreateSimpleRepository(string connectionStringName, SimpleRepositoryOptions option = SimpleRepositoryOptions.RunMigrations)
        {
            var enableMigration = MIGRATION_CONFIG_KEY.ConfigValue().ToBoolean();

            if (!enableMigration) option = SimpleRepositoryOptions.Default;

            var provider = SqlQuery.GetProvider(connectionStringName);

            return new SimpleRepository(provider, option);
        }

        public static DbType GetDbType(string connectionStringName)
        {
            var providerName = ContextConnectionFactory.GetProviderName(connectionStringName).Lower();

            var type = DbType.MSSql;

            switch (providerName)
            {
                case "mysql.data.mysqlclient":
                    type = DbType.MySql;
                    break;
                case "oracle.dataaccess.client":
                    type = DbType.Oracle;
                    break;
                case "postgresql ole db provider":
                    type = DbType.PgSql;
                    break;
                case "system.data.sqlclient":
                default:
                    type = DbType.MSSql;
                    break;
            }

            return type;
        }
    }
}
