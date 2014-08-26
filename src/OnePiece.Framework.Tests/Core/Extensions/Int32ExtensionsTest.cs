using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class Int32ExtensionsTest
    {
        [Fact]
        public void ToBoolean_Tests()
        {
            Assert.True("true".ToBoolean());
            Assert.True("True ".ToBoolean());
            Assert.True("tRue".ToBoolean());
            Assert.True("TRUE".ToBoolean());
            Assert.False("true2".ToBoolean());
            Assert.True(1.ToBoolean());
            Assert.False(0.ToBoolean());
            Assert.False("False".ToBoolean());
            Assert.False("fAlse".ToBoolean());
            Assert.False("FALSE".ToBoolean());
            Assert.False("false ".ToBoolean());
            Assert.False("1111".ToBoolean());
            Assert.True("1".ToBoolean());
            Assert.True("1 ".ToBoolean());
            Assert.False("0".ToBoolean());
        }

        [Fact]
        public void ToEnum_test()
        {
            Assert.Equal(ConsoleColor.Black, 0.ToEnum<ConsoleColor>(ConsoleColor.Blue));
            Assert.Equal(ConsoleColor.Blue, 111110.ToEnum<ConsoleColor>(ConsoleColor.Blue));
        }

        [Fact]
        public void CheckRange_Test()
        {
            Assert.Equal(3, ((int)3).CheckRange(null, 0));
            Assert.Equal(1, ((int)3).CheckRange(1, 0));

            Assert.Equal(8, ((int)3).CheckRange(null, 8));
            Assert.Equal(8, ((int)3).CheckRange(10, 8));
            Assert.Equal(3, ((int)3).CheckRange(3, 2));
        }

        [Fact]
        public void GetSize_Test()
        {
            string B = " B";
            string KB = " KB";
            string MB = " MB";
            int size = 0;
            var test = size.ToFileSize();
            Assert.Equal(size + B, test);

            size = 1023;
            test = size.ToFileSize();
            Assert.Equal("1,023" + B, test);

            size = 12034;
            test = size.ToFileSize();
            Assert.Equal("11.75" + KB, test);

            size = 1248576;
            test = size.ToFileSize();
            Assert.Equal("1.19" + MB, test);

            size = -100;
            test = size.ToFileSize();
            Assert.Equal(string.Empty, test);
        }

        [Fact]
        public void ToUInt32Test()
        {
            Assert.Equal((uint)1, 1.ToUInt32());
            Assert.Equal((uint)1111, 1111.ToUInt32());
            Assert.Equal((uint)0, 0.ToUInt32());

            Assert.Throws<OverflowException>(() => int.MinValue.ToUInt32());

            int? nullable = 11;
            Assert.Equal((uint)11, nullable.ToUInt32());

            nullable = null;
            Assert.Equal((uint)0, nullable.ToUInt32());
        }
    }
}
