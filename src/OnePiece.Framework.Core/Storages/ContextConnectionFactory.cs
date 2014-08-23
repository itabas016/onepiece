using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class ContextConnectionFactory : SingletonBase<ContextConnectionFactory>
    {
        internal Dictionary<string, Connection> ConnectionRepository
        {
            get
            {
                if (_connectionRepository == null)
                {
                    _connectionRepository = new Dictionary<string, Connection>(StringComparer.OrdinalIgnoreCase);
                }

                return _connectionRepository;
            }
        } private Dictionary<string, Connection> _connectionRepository;

        /// <summary>
        /// First it will get the value from configuration file.
        /// If it is not exist, then find it from the database.
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            return Instance.Get(connectionStringName).ConnectionString;
        }

        public static string GetProviderName(string connectionStringName)
        {
            return Instance.Get(connectionStringName).ProviderName;
        }

        internal Connection Get(string connectionStringName)
        {
            var conn = new Connection();
            var config = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (config != null)
            {
                conn.Name = config.Name;
                conn.ConnectionString = config.ConnectionString;
                conn.ProviderName = config.ProviderName;
            }
            else if (this.ConnectionRepository.ContainsKey(connectionStringName))
            {
                conn = this.ConnectionRepository[connectionStringName];
            }
            else
            {
                var container = SingletonBase<IocFacotry>.Instance.Container;
                if (container != null)
                {
                    var locator = container.GetInstance<IServiceLocator>();

                    if (locator != null)
                    {
                        var conns = locator.GetConnections();

                        if (conns != null)
                        {
                            foreach (var item in conns)
                            {
                                if (item.Name.EqualsOrdinalIgnoreCase(connectionStringName))
                                {
                                    conn.Name = item.Name;
                                    conn.ConnectionString = item.ConnectionString;
                                    conn.ProviderName = item.ProviderName;

                                    this.ConnectionRepository[item.Name] = conn;
                                }

                            }
                        }
                    }
                }
            }

            return conn;
        }

        public static void Clear()
        {
            Instance.ConnectionRepository.Clear();
        }
    }
}
