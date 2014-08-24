using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public interface IBasicController
    {
        /// <summary>
        /// Get the corresponding area name of the mvc area.
        /// </summary>
        string Area { get; }
    }
}
