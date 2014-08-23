using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class ProjectConfigHelper
    {
        public static bool IsInDebugMode()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        public static bool IsInReleaseMode()
        {
#if RELEASE
            return true;
#else
            return false;
#endif
        }

        public static bool IsInLiveMode()
        {
#if LIVE
            return true;
#else
            return false;
#endif
        }
    }
}
