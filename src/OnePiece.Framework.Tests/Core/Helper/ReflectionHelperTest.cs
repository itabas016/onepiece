using OnePiece.Framework.Core;
using OnePiece.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Helper
{
    public class ReflectionHelperTest
    {
        #region property translation
        [Fact]
        public void should_return_same_key_value_if_no_param_specified()
        {
            var dic = ReflectionHelper.PropertyTraslation<NoAttributeWithoutTranslation>(null, null, new List<string> { "Name" });

            Assert.Equal(2, dic.Count);
            Assert.Equal("Id", dic.First().Key);
            Assert.Equal("Id", dic.First().Value);
        }

        public class NoAttributeWithoutTranslation
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
        }

        [Fact]
        public void should_return_key_translation_value_if_no_param_specified()
        {
            var dic = ReflectionHelper.PropertyTraslation<NoAttribute>();

            Assert.Equal(3, dic.Count);
            Assert.Equal("Id", dic.First().Key);
            Assert.Equal("序号", dic.First().Value);
        }

        public class NoAttribute
        {
            [Display(Name = "序号")]
            public int Id { get; set; }

            [Display(Name = "名称")]
            public string Name { get; set; }

            [Display(Name = "年龄")]
            public int Age { get; set; }
        }

        [Fact]
        public void should_only_return_specified_columns()
        {
            var dic = ReflectionHelper.PropertyTraslation<NoAttributeIncludeAttribute>(new List<Type>() { typeof(SqlColumnAttribute) });

            Assert.Equal(2, dic.Count);
            Assert.Equal("Id", dic.First().Key);
            Assert.Equal("序号", dic.First().Value);
        }

        public class NoAttributeIncludeAttribute
        {
            [SqlColumn]
            [Display(Name = "序号")]
            public int Id { get; set; }

            [SqlColumn]
            [Display(Name = "名称")]
            public string Name { get; set; }

            [Display(Name = "年龄")]
            public int Age { get; set; }
        }

        [Fact]
        public void should_only_return_specified_columns_and_exclude_specified_columns()
        {
            var dic = ReflectionHelper.PropertyTraslation<NoAttributeIncludeAttributeExcludeAttribute>(new List<Type>() { typeof(SqlColumnAttribute) }, new List<Type>() { typeof(ObsoleteAttribute) });

            Assert.Equal(1, dic.Count);
            Assert.Equal("Id", dic.First().Key);
            Assert.Equal("序号", dic.First().Value);
        }

        public class NoAttributeIncludeAttributeExcludeAttribute
        {
            [SqlColumn]
            [Display(Name = "序号")]
            public int Id { get; set; }

            [SqlColumn]
            [Obsolete]
            [Display(Name = "名称")]
            public string Name { get; set; }

            [Display(Name = "年龄")]
            public int Age { get; set; }
        }

        #endregion

        [Fact]
        public void get_name_test()
        {
            var name = ReflectionHelper.GetName<NoAttributeIncludeAttribute>(x => x.Age);

            Assert.Equal("Age", name);
        }

        [Fact]
        public void get_display_name_test()
        {
            var name = ReflectionHelper.GetDisplayName<NoAttributeIncludeAttribute>(x => x.Age);

            Assert.Equal("年龄", name);
        }
    }
}
