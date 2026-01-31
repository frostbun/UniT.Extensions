#nullable enable
namespace UniT.Extensions.Indexing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public static class EnumerableExtensions
    {
        [Pure]
        public static T First<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.First(item => predicate(item, index++));
        }

        [Pure]
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.FirstOrDefault(item => predicate(item, index++));
        }

        [Pure]
        public static T Last<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.Last(item => predicate(item, index++));
        }

        [Pure]
        public static T LastOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.LastOrDefault(item => predicate(item, index++));
        }

        [Pure]
        public static T Single<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.Single(item => predicate(item, index++));
        }

        [Pure]
        public static T SingleOrDefault<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.SingleOrDefault(item => predicate(item, index++));
        }

        [Pure]
        public static TResult Aggregate<TSource, TResult>(this IEnumerable<TSource> enumerable, TResult seed, Func<TResult, TSource, int, TResult> func)
        {
            var index = 0;
            return enumerable.Aggregate(seed, (current, item) => func(current, item, index++));
        }

        [Pure]
        public static TResult Min<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, int, TResult> selector)
        {
            var index = 0;
            return enumerable.Min(item => selector(item, index++));
        }

        [Pure]
        public static TResult Max<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, int, TResult> selector)
        {
            var index = 0;
            return enumerable.Max(item => selector(item, index++));
        }

        [Pure]
        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, int, TKey> keySelector)
        {
            var index = 0;
            return enumerable.MinBy(item => keySelector(item, index++));
        }

        [Pure]
        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, int, TKey> keySelector)
        {
            var index = 0;
            return enumerable.MaxBy(item => keySelector(item, index++));
        }

        [Pure]
        public static bool Any<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.Any(item => predicate(item, index++));
        }

        [Pure]
        public static bool All<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate)
        {
            var index = 0;
            return enumerable.All(item => predicate(item, index++));
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var index = 0;
            enumerable.ForEach(item => action(item, index++));
        }

        public static void SafeForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var index = 0;
            enumerable.SafeForEach(item => action(item, index++));
        }
    }
}