using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterExtensions.Collections
{
    /// <summary>
    /// Queryable Extensions
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Execute Query with ToList and than select sequence of elements with selector 
        /// </summary>
        /// <param name="source">Query</param>
        /// <param name="selector">selector</param>
        /// <typeparam name="TSource">DBO type</typeparam>
        /// <typeparam name="TResult">Expected result type</typeparam>
        /// <returns>Sequence of selector output</returns>
        public static IEnumerable<TResult> ToListSelect<TSource, TResult>(
            this IQueryable<TSource> source,
            Func<TSource, TResult> selector) =>
            source
                .ToList()
                .Select(selector);
    }
}