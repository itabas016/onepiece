using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class EnumExtensionsTest
    {
        [Fact]
        public void should_return_correct_display_name()
        {
            Assert.Equal("DisplayA", EnumLetter.A.GetDisplay<EnumLetter>());
            Assert.Equal("DisplayB", EnumLetter.B.GetDisplay<EnumLetter>());
            Assert.Equal("C", EnumLetter.C.GetDisplay<EnumLetter>());
            Assert.Equal("DisplayA", EnumLetter.A.GetDisplay());
        }

        [Fact]
        public void enum_to_dictionary_test()
        {
            var dic = new Dictionary<int, string>();
            var dic_display = new Dictionary<int, string>();

            dic_display.Add(0, "DisplayA");
            dic_display.Add(1, "DisplayB");
            dic_display.Add(2, "C");

            dic.Add(0, "A");
            dic.Add(1, "B");
            dic.Add(2, "C");

            Assert.Equal(dic_display, EnumLetter.A.ToDictionary());
            Assert.Equal(dic, EnumLetter.A.ToDictionary(false));
        }

        public enum EnumLetter
        {
            [Display(Name = "DisplayA")]
            A = 0,
            [Display(Name = "DisplayB")]
            B = 1,
            C = 2
        }
    }
}
