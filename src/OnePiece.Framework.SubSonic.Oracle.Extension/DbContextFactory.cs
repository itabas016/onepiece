using OnePiece.Framework.Core;
using SubSonic.Oracle.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic.Oracle
{
    public class DbContextFactory
    {
        public const string AUTO_MIGRATION = "EnableAutoMigration";
        public static IRepository CreateSimpleRepository(string connectionStringName, SimpleRepositoryOptions option = SimpleRepositoryOptions.RunMigrations)
        {
            var autoMigration = AUTO_MIGRATION.ConfigValue().ToBoolean();

            if (autoMigration) option = SimpleRepositoryOptions.RunMigrations;
            else option = SimpleRepositoryOptions.None;

            return new SimpleRepository(connectionStringName, option);
        }
    }
}
