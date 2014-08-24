using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace OnePiece.Framework.Tests.Attributes
{
    public class DebugFactAttribute : FactAttribute
    {
        bool isDebugMode = false;

#if DEBUG
        public DebugFactAttribute()
        {
            isDebugMode = true;
        }
#endif

        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            var command = default(ITestCommand);
            if (isDebugMode)
            {
                command = new FactCommand(method);
            }
            else
            {
                command = new SkipCommand(method, "DebugSkipped_" + method.Name, "Only available in debug mode!");
            }

            yield return command;
        }
    }
}
