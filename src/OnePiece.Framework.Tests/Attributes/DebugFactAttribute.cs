using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace OnePiece.Framework.Tests.Attributes
{
    [XunitTestCaseDiscoverer("OnePiece.Framework.Tests.XunitExtensions.SkippableFactDiscoverer", "OnePiece.Framework.Tests")]
    public class DebugFactAttribute : FactAttribute
    {
        bool isDebugMode = false;

#if DEBUG
        public DebugFactAttribute()
        {
            isDebugMode = true;
        }
#endif

    }
}
