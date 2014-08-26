using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class ObjectExtensionsTest
    {
        [Fact]
        public void object_clone_test()
        {
            var person = new Person();
            person.Name = "Bob";
            person.Age = 12;
            person.Marker = new Marker(1111);

            var instance = person.ShallowCopy();
            Show(person);
            Show(instance);

            Assert.Equal(person.Marker.Number, instance.Marker.Number);

            person.Name = "Jack";
            person.Age = 22;
            person.Marker.Number = 2222;

            var instance2 = person.Clone<Person>();
            Show(person);
            Show(instance2);

            Assert.Equal(false, instance2.Equals(person));
        }

        public void Show(Person person)
        {
            Console.WriteLine(string.Format("Name: {0}, Age: {1}", person.Name, person.Age));
            Console.WriteLine(string.Format("Marker Number: {0}", person.Marker.Number));
        }

        [Serializable]
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public Marker Marker { get; set; }

            public Person ShallowCopy()
            {
                return (Person)this.MemberwiseClone();
            }
        }

        [Serializable]
        public class Marker
        {
            public int Number { get; set; }

            public Marker(int number)
            {
                this.Number = number;
            }
        }
    }
}
