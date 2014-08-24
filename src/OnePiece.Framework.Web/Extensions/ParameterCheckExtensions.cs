using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class ParameterCheckExtensions
    {
        public static void ThrowIfNullOrEmpty(this string parameter, string errorMessage = "")
        {
            if (parameter.IsNullOrEmpty())
            {
                ThrowParameterErrorException(errorMessage);
            }
        }

        public static void ThrowIfNull(this object parameter, string errorMessage = "")
        {
            if (parameter == null)
            {
                ThrowParameterErrorException(errorMessage);
            }
        }

        public static void ThrowIfNegtive(this int parameter, string errorMessage = "")
        {
            if (parameter < 0)
            {
                ThrowParameterErrorException(errorMessage);
            }
        }

        public static void ThrowIfNegtiveOrZero(this int parameter, string errorMessage = "")
        {
            if (parameter <= 0)
            {
                ThrowParameterErrorException(errorMessage);
            }
        }

        public static void ThrowIfMatch(this Expression<Func<bool>> predicate, string errorMessage = "")
        {
            if (predicate != null)
            {
                ThrowIfMatch(predicate.Compile(), errorMessage);
            }
        }

        public static void ThrowIfMatch(this Func<bool> predicate, string errorMessage = "")
        {
            if (predicate != null && predicate())
            {
                ThrowParameterErrorException(errorMessage);
            }
        }

        internal static void ThrowParameterErrorException(string errorMessage = "")
        {
            throw new ParameterErrorException(errorMessage);
        }
    }
}
