using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public abstract class BootItem : IBootable
    {
        public virtual void Startup()
        {

        }

        /// <summary>
        /// Default value is 1000;
        /// </summary>
        public virtual int Order
        {
            get { return 1000; }
        }
    }
}
