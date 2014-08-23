using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Returns the first element of a sequence, if it is null return one empty instance.
        ///
        /// Parameters:
        ///   source:
        ///     The System.Collections.Generic.IEnumerable<T> to return the first element
        ///     of.
        ///
        /// Type parameters:
        ///   TSource:
        ///     The type of the elements of source.
        ///
        /// Returns:
        ///     The first element in the specified sequence.
        ///
        /// Exceptions:
        ///   System.ArgumentNullException:
        ///     source is null.
        ///
        ///   System.InvalidOperationException:
        ///     The source sequence is empty.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource FirstOrEmpty<TSource>(this IEnumerable<TSource> source)
            where TSource : new()
        {
            var first = source.FirstOrDefault();
            if (first == null)
                first = new TSource();

            return first;
        }

        public static string Concatenate<T>(this IEnumerable<T> collection, Func<T, string> selector = null, string separator = ",")
        {
            if (collection == null || !collection.Any()) return string.Empty;

            if (selector == null) selector = x => x.ToString();

            var sb = new StringBuilder();
            foreach (T t in collection) sb.Append(selector(t)).Append(separator);

            return sb.Remove(sb.Length - 1, 1).ToString();
        }
    }
}
