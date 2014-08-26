using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class CommonExtensionsTest
    {
        [Fact]
        public void filename_formatting_test()
        {
            var fileName = "a*1\\/:?<|>.txt";

            Assert.Equal("a1.txt", fileName.NormalzieFileName());
        }

        [Fact]
        public void test_object_null_able_test()
        {
            var personlist = new List<Person>()
            { 
                new Person() { Age = 21 },
                new Person() { Name = "test2"},
            };

            personlist.ForEach(x =>
                Console.WriteLine(string.Format("Name:{0} Age:{1}", x.Name.MakeSureNotNull(), x.Age.MakeSureNotNull())));

        }

        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
