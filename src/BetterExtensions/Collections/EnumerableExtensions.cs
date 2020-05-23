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
        public static IEnumerable<TSource> AppendWith<TSource>(
            this IEnumerable<TSource> source, 
            TSource item, bool condition) =>
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
        public static IEnumerable<TSource> AppendWith<TSource>(
            this IEnumerable<TSource> source, 
            Func<TSource> funcItem, bool condition) =>
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
        ///<param name="source">The enumerable to search.</param>
        ///<param name="predicate">The expression to test the items against.</param>
        /// <typeparam name="TSource">Sequence items type</typeparam>
        ///<returns>The index of the first matching item, or -1 if no items match.</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> source, 
            Func<TSource, bool> predicate) {
            var idx = 0;
            foreach (var item in source) {
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
        public static IEnumerable<IEnumerable<TSource>> SplitWith<TSource>(
            this IEnumerable<TSource> source, 
            Func<TSource, bool> predicate) =>
            source.Aggregate(new List<List<TSource>> {new List<TSource>()},
                (list, value) =>
                {
                    if (predicate(value))
                        list.Add(new List<TSource>());
                    else
                        list.Last().Add(value);
                    return list;
                });

        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>An IEnumerable&lt;T&gt; whose elements are the result of invoking the transform function on each element of source.</returns>
        public static IEnumerable<TResult> Map<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector) =>
            source.Select(selector);
        
        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>An IEnumerable&lt;T&gt; whose elements are the result of invoking the transform function on each element of source.</returns>
        public static IEnumerable<TResult> Map<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector) =>
            source.Select(selector);

        /// <summary>
        /// Applies an accumulator function over a sequence.
        /// </summary>
        /// <param name="source">An IEnumerable&lt;T&gt; to aggregate over.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The transformed final accumulator value.</returns>
        public static TSource Reduce<TSource>(this IEnumerable<TSource> source, 
            Func<TSource, TSource, TSource> func) =>
            source.Aggregate(func);
        
        /// <summary>
        /// Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.
        /// </summary>
        /// <param name="source">An IEnumerable&lt;T&gt; to aggregate over.</param>
        /// <param name="seed">The initial accumulator value (unit).</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <returns>The transformed final accumulator value.</returns>
        public static TAccumulate Reduce<TSource, TAccumulate>(this IEnumerable<TSource> source, 
            TAccumulate seed, 
            Func<TAccumulate, TSource, TAccumulate> func) =>
            source.Aggregate(seed, func);
        
        /// <summary>
        /// Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.
        /// </summary>
        /// <param name="source">An IEnumerable&lt;T&gt; to aggregate over.</param>
        /// <param name="seed">The initial accumulator value (unit).</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <returns>The transformed final accumulator value.</returns>
        public static TResult Reduce<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, 
            TAccumulate seed, 
            Func<TAccumulate, TSource, TAccumulate> func, 
            Func<TAccumulate, TResult> resultSelector) =>
            source.Aggregate(seed, func, resultSelector);

        /// <summary>
        /// Projects each element of a sequence to an IEnumerable&lt;T&gt; and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
        /// <returns>An IEnumerable&lt;T&gt; whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        public static IEnumerable<TResult> Collect<TSource, TResult>(this IEnumerable<TSource> source, 
            Func<TSource, IEnumerable<TResult>> selector) =>
            source.SelectMany(selector);

        /// <summary>
        /// Projects each element of a sequence to an IEnumerable&lt;T&gt; and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each element of the input sequence.</param>
        /// <param name="resultSelector"></param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate elements collected by collectionSelector.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
        /// <returns>An IEnumerable&lt;T&gt; whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        public static IEnumerable<TResult> Collect<TSource, TCollection, TResult>(this IEnumerable<TSource> source, 
            Func<TSource, IEnumerable<TCollection>> collectionSelector, 
            Func<TSource, TCollection, TResult> resultSelector) =>
            source.SelectMany(collectionSelector, resultSelector);
        
        
        /// <summary>
        /// Deconstruct an array into head and tail as it is in a functional programming languages.
        /// </summary>
        /// <param name="source">A sequence of values to deconstruct.</param>
        /// <param name="head">First element of sequence.</param>
        /// <param name="tail">Sequence without first element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        public static void Deconstruct<TSource>(this IEnumerable<TSource> source, out TSource head, out IEnumerable<TSource> tail)
        {
            var list = source.ToArray();
            head = list.First();
            tail = new List<TSource>(list.Skip(1));
        }
    }
}