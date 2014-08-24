using SubSonic.Oracle.SqlGeneration.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.SubSonic.Oracle
{
    public abstract class LogBase : EntityBase
    {
        /// <summary>
        /// Log id should not be Int32, Int64 instead.
        /// </summary>
        [SubSonicIgnore]
        //[Obsolete]
        public override int Id
        {
            get { return base.Id; }
            set { base.Id = value; }
        }

        [SubSonicPrimaryKey]
        public long ID { get; set; }
    }
}
