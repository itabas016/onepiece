using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Helper
{
    public class AuthHelperTest
    {
        [Fact]
        public void auth_verification_text_generate_test()
        {
            var code = AuthHelper.VerificationText(4);
            Console.WriteLine(code);
        }
    }
}
