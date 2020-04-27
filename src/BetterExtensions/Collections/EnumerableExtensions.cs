using System;
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
        /// Append new item to sequence with condition. This will execute item func only if condition is true
        /// </summary>
        /// <param name="source">Input sequence</param>
        /// <param name="funcItem">Appendable item func which will be called in case of condition</param>
        /// <param name="condition">Condition that indicates should the item be added or not</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        /// <returns>Sequence with or not with appended item</returns>
        public static IEnumerable<TSource> AppendWith<TSource>(this IEnumerable<TSource> source, Func<TSource> funcItem, bool condition) =>
            condition
                ? source.Append(funcItem.Invoke())
                : source;
        
        /// <summary>
        /// Filter sequence from null items to make null free
        /// </summary>
        /// <param name="source">Input sequence</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        /// <returns>Filtered sequence</returns>
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source) =>
            source.Where(item => item != null);
            
        ///<summary>Finds the index of the first item matching an expression in an enumerable.</summary>
        ///<param name="items">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> items, Func<TSource, bool> predicate) {
            var idx = 0;
            foreach (var item in items) {
                if (predicate(item)) return idx;
                idx++;
            }
            return -1;
        }

        /// <summary>
        /// Split sequence with predicate
        /// </summary>
        /// <param name="source">Input sequence</param>
        /// <param name="predicate">Split sequences by predicate</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        /// <returns>Split sequence with predicate</returns>
        public static IEnumerable<IEnumerable<TSource>> SplitWith<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) =>
            source.Aggregate(new List<List<TSource>> {new List<TSource>()},
                (list, value) =>
                {
                    if (predicate(value))
                        list.Add(new List<TSource>());
                    else
                        list.Last().Add(value);
                    return list;
                });
    }
}