using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Web
{
    public class RandomServiceTest
    {
        [Fact]
        public void random_list_weight_test()
        {
            var dictionary = new Dictionary<object, int>();

            dictionary[PropertyType.A] = 10;
            dictionary[PropertyType.B] = 20;
            dictionary[PropertyType.C] = 30;

            var randomService = new RandomService();
            var retA = randomService.RandomInternal(dictionary, 3);
            var retB = randomService.RandomInternal(dictionary, 15);
            var retC = randomService.RandomInternal(dictionary, 55);

            Assert.Equal(PropertyType.A, (PropertyType)retA);
            Assert.Equal(PropertyType.B, (PropertyType)retB);
            Assert.Equal(PropertyType.C, (PropertyType)retC);
        }

        [Fact]
        public void random_list_generic()
        {
            var dictionary = new Dictionary<PropertyType, int>();

            dictionary[PropertyType.A] = 10;
            dictionary[PropertyType.B] = 20;
            dictionary[PropertyType.C] = 30;

            var randomService = new RandomService();
            var retA = randomService.Random<PropertyType>(dictionary);

            Console.WriteLine(retA);

            var dic = new Dictionary<string, int>();

            dic["a"] = 10;
            dic["b"] = 20;
            dic["c"] = 30;

            var retb = randomService.Random<string>(dic);

            Console.WriteLine(retb);
        }

        public enum PropertyType
        {
            A = 0,
            B = 1,
            C = 2
        }
    }
}
