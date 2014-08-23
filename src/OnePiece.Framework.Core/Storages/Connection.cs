using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class Connection : IContextConnection
    {
        public string Name { get; set; }

        public string ConnectionString { get; set; }

        public string ProviderName { get; set; }
    }
}
