using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Fact]
        public void collection_generic_default_instance_test()
        {
            var first = new Person() { Name = "first", Age = 1 };
            var second = new Person() { Name = "second", Age = 2 };
            var person_list = new List<Person>() { first, second };

            var person_empty = new List<Person>() { };
            var default_instance = new Person();

            Assert.Equal(first, person_list.FirstOrEmpty());
            Assert.Equal(null, person_empty.FirstOrEmpty().Name);
            Assert.Equal(0, person_empty.FirstOrEmpty().Age);
            Assert.Equal(false, person_empty.FirstOrEmpty() == default_instance);
        }

        [Fact]
        public void string_list_concatenate()
        {
            var list = new List<string>();
            Assert.Equal(string.Empty, list.Concatenate(x => x, ASCII.COMMA));

            list.Add("a");
            Assert.Equal("a", list.Concatenate(x => x, ASCII.COMMA));

            list.Add("b");
            Assert.Equal("a,b", list.Concatenate());
        }

        [Fact]
        public void tuple_list_concatenate()
        {
            var list = new List<Tuple<int, int, string>>();

            Assert.Equal(string.Empty, list.Concatenate());

            list.Add(new Tuple<int, int, string>(1, 1, "2"));
            Assert.Equal("1", list.Concatenate(x => x.Item1.ToString()));

            list.Add(new Tuple<int, int, string>(10, 1, "2"));
            Assert.Equal("1,10", list.Concatenate(x => x.Item1.ToString()));
            Assert.Equal("2,2", list.Concatenate(x => x.Item3));
        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
