﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return x => true; }
        public static Expression<Func<T, bool>> False<T>() { return x => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);


            //// need to detect whether they use the same
            //// parameter instance; if not, they need fixing
            //ParameterExpression param = expr1.Parameters[0];
            //if (ReferenceEquals(param, expr2.Parameters[0]))
            //{
            //    // simple version
            //    return Expression.Lambda<Func<T, bool>>(
            //        Expression.AndAlso(expr1.Body, expr2.Body), param);
            //}
            //// otherwise, keep expr1 "as is" and invoke expr2
            //return Expression.Lambda<Func<T, bool>>(
            //    Expression.AndAlso(
            //        expr1.Body,
            //        Expression.Invoke(expr2, param)), param);
        }

        public static Expression<Func<T, bool>> And<T>(this IEnumerable<Expression<Func<T, bool>>> exprList)
        {
            if (exprList != null)
            {
                var retExpr = default(Expression<Func<T, bool>>);

                foreach (var expr in exprList)
                {
                    if (expr == null) continue;

                    if (retExpr == null) retExpr = expr;
                    else
                    {
                        retExpr = retExpr.And(expr);
                    }
                }

                return retExpr;
            }

            return null;
        }
    }
}
