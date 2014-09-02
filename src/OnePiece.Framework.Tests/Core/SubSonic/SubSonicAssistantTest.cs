using OnePiece.Framework.SubSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.SubSonic
{
    public class SubSonicAssistantTest
    {
        const string TestAssemblyName = "OnePiece.Framework.Tests";

        [Fact]
        public void index_generate_test()
        {
            var sql = IndexAssistant.Generate(TestAssemblyName);
            Console.WriteLine(sql);
        }

        [Fact]
        public void comment_generate_test()
        {
            var sql = CommentAssistant.Generate(TestAssemblyName);
            Console.WriteLine(sql);
        }
    }
}
