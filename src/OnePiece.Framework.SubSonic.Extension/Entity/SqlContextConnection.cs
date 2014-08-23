using OnePiece.Framework.Core;
using SubSonic.SqlGeneration.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    [SubSonicTableNameOverride("SqlContextItem")]
    public class SqlContextConnection : EntityBase, IContextConnection
    {
        public SqlContextConnection()
        {
            this.ProviderName = SqlQuery.SQL_PROVIDER_NAME;
        }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "连接字符串")]
        public string ConnectionString { get; set; }

        [Display(Name = "连接类型")]
        public string ProviderName { get; set; }
    }
}
