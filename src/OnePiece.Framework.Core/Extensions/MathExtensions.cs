using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class MathExtensions
    {
        public static int Round(this float val)
        {
            return (int)Math.Round((decimal)val);
        }

        public static double Negative(this double val)
        {
            if (val > 0)
            {
                return -1 * val;
            }

            return val;
        }
    }
}
