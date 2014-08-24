using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public class RandomService : IRandomService
    {
        public Random R { get; private set; }

        public RandomService()
        {
            R = new Random(Guid.NewGuid().GetHashCode());
        }

        /// <summary>
        /// => [min, max]
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public int Random(int min, int max)
        {
            return this.R.Next(min, max + 1);
        }

        public object Random(Dictionary<object, int> candidates)
        {
            return RandomInternal(candidates, null);
        }

        internal object RandomInternal(Dictionary<object, int> candidates, int? randomValue)
        {
            if (candidates == null) return null;

            var sum = candidates.Values.Sum();
            var r = randomValue.HasValue ? randomValue.Value : this.Random(0, sum);
            var ret = default(object);

            var total = 0;
            foreach (var item in candidates)
            {
                total += item.Value;
                if (r <= total)
                {
                    ret = item.Key;
                    break;
                }
            }

            return ret;
        }

        public T Random<T>(Dictionary<T, int> candidates)
        {
            if (candidates == null) return default(T);

            var ret = RandomInternal(candidates, null);

            return ret;
        }

        internal T RandomInternal<T>(Dictionary<T, int> candidates, int? randomValue)
        {
            if (candidates == null) return default(T);

            var sum = candidates.Values.Sum();
            var r = randomValue.HasValue ? randomValue.Value : this.Random(0, sum);
            var ret = default(T);

            var total = 0;
            foreach (var item in candidates)
            {
                total += item.Value;
                if (r <= total)
                {
                    ret = item.Key;
                    break;
                }
            }

            return ret;
        }
    }
}
