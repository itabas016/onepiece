using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class CombinatoricsUtilitiesTest
    {
        [Fact]
        public void test_int_list()
        {
            var list = new List<int>() { 1, 2, 3, 4 };

            //Combinations sort by descending
            list.GetCombinations<int>(x =>
            {
                var str = x.Select(m => m.ToString()).Aggregate((a, b) => a + "," + b);
                Console.WriteLine(str);
            }, 3);
            Console.WriteLine();
            //Permutations
            list.GetPermutations<int>(x =>
            {
                var str = x.Select(m => m.ToString()).Aggregate((a, b) => a + "," + b);
                Console.WriteLine(str);
            }, 4, false);
        }

        [Fact]
        public void test_string_list()
        {
            var list = new List<string>() { "A", "B", "C", "D", "E" };

            //output by A|B,A|C,B,A step by every letter
            list.GetAllCombinations<string>(x => Console.WriteLine(x.Aggregate((a, b) => a + "," + b)));
        }

        [Fact]
        public void test_permute_and_shuffle()
        {
            var list = new List<int>() { 1, 2, 3, 4 };
            IList<int> sortdecending = new List<int>() { 2, 1, 3, 0 };
            var output = list.Permute<int>(sortdecending).ToList();
            //Permute assign indics order
            output.ForEach(x => Console.Write(x));

            Console.WriteLine();
            //ouptut radom list
            list.Shuffle<int>();
            list.ForEach(x => Console.Write(x));
        }
    }
}
