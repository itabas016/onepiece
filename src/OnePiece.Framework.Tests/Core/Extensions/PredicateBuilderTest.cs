using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class PredicateBuilderTest
    {
        List<int> Repo = new List<int>();
        public PredicateBuilderTest()
        {
            Enumerable.Range(0, 10).ToList().ForEach(x => Repo.Add(x));
        }

        [Fact]
        public void test_and()
        {
            var predicate = PredicateBuilder.True<int>();

            predicate = predicate.And<int>(i => i < 10 && i > 2);
            predicate = predicate.And<int>(i => i > 5 && i < 9);

            var ret = Repo.Where(predicate.Compile()).ToList();

            Assert.Equal(3, ret.Count);
        }


        [Fact]
        public void test_or()
        {
            var predicate = PredicateBuilder.False<int>();

            predicate = predicate.Or<int>(i => i > 8 && i < 10);
            predicate = predicate.Or<int>(i => i > 3 && i < 5);

            var ret = Repo.Where(predicate.Compile()).ToList();

            Assert.Equal(2, ret.Count);
        }
    }
}
