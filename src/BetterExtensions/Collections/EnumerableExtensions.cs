using System.Collections.Generic;
using System.Linq;

namespace BetterExtensions.Collections
{
    /// <summary>
    /// Enumerable Extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Append new item to sequence with condition
        /// </summary>
        /// <param name="source">Input sequence</param>
        /// <param name="item">Appendable item </param>
        /// <param name="condition">Condition that indicates should the item be added or not</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        /// <returns>Sequence with or not with appended item</returns>
        public static IEnumerable<TSource> AppendWith<TSource>(this IEnumerable<TSource> source, TSource item, bool condition) =>
            condition
                ? source.Append(item)
                : source;

        /// <summary>
        /// Filter sequence from null items to make null free
        /// </summary>
        /// <param name="source">Input sequence</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        /// <returns>Filtered sequence</returns>
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source) =>
            source.Where(item => item != null);
    }
}