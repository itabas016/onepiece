using SubSonic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic
{
    public interface IDbContext// : ICacheable
    {
        string ConnectionStringName { get; }

        IRepository DbContext { get; }
    }
}
