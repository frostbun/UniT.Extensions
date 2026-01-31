#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public static class TupleExtensions
    {
        [Pure]
        public static (TFirst, TSecond) First<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.First(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (TFirst, TSecond, TThird) First<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.First(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static (TFirst, TSecond) FirstOrDefault<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.FirstOrDefault(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (TFirst, TSecond, TThird) FirstOrDefault<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.FirstOrDefault(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static (TFirst, TSecond) Last<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.Last(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (TFirst, TSecond, TThird) Last<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.Last(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static (TFirst, TSecond) LastOrDefault<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.LastOrDefault(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (TFirst, TSecond, TThird) LastOrDefault<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.LastOrDefault(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static (TFirst, TSecond) Single<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.Single(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (TFirst, TSecond, TThird) Single<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.Single(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static (TFirst, TSecond) SingleOrDefault<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.SingleOrDefault(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (TFirst, TSecond, TThird) SingleOrDefault<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.SingleOrDefault(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond)> Where<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond, TThird)> Where<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond)> WhereFirst<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item1));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond, TThird)> WhereFirst<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item1));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond)> WhereSecond<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item2));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond, TThird)> WhereSecond<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item2));
        }

        [Pure]
        public static IEnumerable<(TFirst, TSecond, TThird)> WhereThird<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, bool> predicate)
        {
            return tuples.Where(tuple => predicate(tuple.Item3));
        }

        [Pure]
        public static IEnumerable<TResult> Select<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TResult> selector)
        {
            return tuples.Select(tuple => selector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static IEnumerable<TResult> Select<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TResult> selector)
        {
            return tuples.Select(tuple => selector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static IEnumerable<TFirst> SelectFirsts<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples)
        {
            return tuples.Select(tuple => tuple.Item1);
        }

        [Pure]
        public static IEnumerable<TFirst> SelectFirsts<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.Select(tuple => tuple.Item1);
        }

        [Pure]
        public static IEnumerable<TSecond> SelectSeconds<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples)
        {
            return tuples.Select(tuple => tuple.Item2);
        }

        [Pure]
        public static IEnumerable<TSecond> SelectSeconds<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.Select(tuple => tuple.Item2);
        }

        [Pure]
        public static IEnumerable<TThird> SelectThirds<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.Select(tuple => tuple.Item3);
        }

        [Pure]
        public static IEnumerable<TResult> SelectMany<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, IEnumerable<TResult>> selector)
        {
            return tuples.SelectMany(tuple => selector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static IEnumerable<TResult> SelectMany<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, IEnumerable<TResult>> selector)
        {
            return tuples.SelectMany(tuple => selector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static TAccumulate Aggregate<TFirst, TSecond, TAccumulate>(this IEnumerable<(TFirst, TSecond)> tuples, TAccumulate seed, Func<TAccumulate, TFirst, TSecond, TAccumulate> func)
        {
            return tuples.Aggregate(seed, (current, tuple) => func(current, tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static TAccumulate Aggregate<TFirst, TSecond, TThird, TAccumulate>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, TAccumulate seed, Func<TAccumulate, TFirst, TSecond, TThird, TAccumulate> func)
        {
            return tuples.Aggregate(seed, (current, tuple) => func(current, tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static TResult Min<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TResult> selector)
        {
            return tuples.Min(tuple => selector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static TResult Min<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TResult> selector)
        {
            return tuples.Min(tuple => selector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static TResult Max<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TResult> selector)
        {
            return tuples.Max(tuple => selector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static TResult Max<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TResult> selector)
        {
            return tuples.Max(tuple => selector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static (TFirst, TSecond) MinBy<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item1, tuple.Item2), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinBy<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MinByFirst<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.MinBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinByFirst<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.MinBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MinByFirst<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinByFirst<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MinBySecond<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.MinBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinBySecond<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.MinBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MinBySecond<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinBySecond<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinByThird<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TThird>? comparer = null)
        {
            return tuples.MinBy(tuple => tuple.Item3, comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MinByThird<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MinBy(tuple => keySelector(tuple.Item3), comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MaxBy<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item1, tuple.Item2), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxBy<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MaxByFirst<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.MaxBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxByFirst<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.MaxBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MaxByFirst<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxByFirst<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MaxBySecond<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.MaxBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxBySecond<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.MaxBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static (TFirst, TSecond) MaxBySecond<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxBySecond<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxByThird<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TThird>? comparer = null)
        {
            return tuples.MaxBy(tuple => tuple.Item3, comparer);
        }

        [Pure]
        public static (TFirst, TSecond, TThird) MaxByThird<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.MaxBy(tuple => keySelector(tuple.Item3), comparer);
        }

        [Pure]
        public static bool Any<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.Any(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static bool Any<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.Any(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static bool All<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.All(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static bool All<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.All(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        public static void ForEach<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Action<TFirst, TSecond> action)
        {
            tuples.ForEach(tuple => action(tuple.Item1, tuple.Item2));
        }

        public static void ForEach<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Action<TFirst, TSecond, TThird> action)
        {
            tuples.ForEach(tuple => action(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        public static void SafeForEach<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Action<TFirst, TSecond> action)
        {
            tuples.SafeForEach(tuple => action(tuple.Item1, tuple.Item2));
        }

        public static void SafeForEach<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Action<TFirst, TSecond, TThird> action)
        {
            tuples.SafeForEach(tuple => action(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond)>> GroupBy<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond, TThird)>> GroupBy<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static IEnumerable<IGrouping<TFirst, (TFirst, TSecond)>> GroupByFirst<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples)
        {
            return tuples.GroupBy(tuple => tuple.Item1);
        }

        [Pure]
        public static IEnumerable<IGrouping<TFirst, (TFirst, TSecond, TThird)>> GroupByFirst<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.GroupBy(tuple => tuple.Item1);
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond)>> GroupByFirst<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item1));
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond, TThird)>> GroupByFirst<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item1));
        }

        [Pure]
        public static IEnumerable<IGrouping<TSecond, (TFirst, TSecond)>> GroupBySecond<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples)
        {
            return tuples.GroupBy(tuple => tuple.Item2);
        }

        [Pure]
        public static IEnumerable<IGrouping<TSecond, (TFirst, TSecond, TThird)>> GroupBySecond<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.GroupBy(tuple => tuple.Item2);
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond)>> GroupBySecond<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item2));
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond, TThird)>> GroupBySecond<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item2));
        }

        [Pure]
        public static IEnumerable<IGrouping<TThird, (TFirst, TSecond, TThird)>> GroupByThird<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.GroupBy(tuple => tuple.Item3);
        }

        [Pure]
        public static IEnumerable<IGrouping<TKey, (TFirst, TSecond, TThird)>> GroupByThird<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector)
        {
            return tuples.GroupBy(tuple => keySelector(tuple.Item3));
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderBy<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item1, tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderBy<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByFirst<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.OrderBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByFirst<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.OrderBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByFirst<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByFirst<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderBySecond<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.OrderBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderBySecond<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.OrderBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderBySecond<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderBySecond<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByThird<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TThird>? comparer = null)
        {
            return tuples.OrderBy(tuple => tuple.Item3, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByThird<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderBy(tuple => keySelector(tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByDescending<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item1, tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescending<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByDescendingFirst<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescendingFirst<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByDescendingFirst<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescendingFirst<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByDescendingSecond<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescendingSecond<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> OrderByDescendingSecond<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescendingSecond<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescendingThird<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TThird>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => tuple.Item3, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> OrderByDescendingThird<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.OrderByDescending(tuple => keySelector(tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenBy<TFirst, TSecond, TKey>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item1, tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenBy<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByFirst<TFirst, TSecond>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.ThenBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByFirst<TFirst, TSecond, TThird>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.ThenBy(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByFirst<TFirst, TSecond, TKey>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByFirst<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenBySecond<TFirst, TSecond>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.ThenBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenBySecond<TFirst, TSecond, TThird>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.ThenBy(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenBySecond<TFirst, TSecond, TKey>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenBySecond<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByThird<TFirst, TSecond, TThird>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TThird>? comparer = null)
        {
            return tuples.ThenBy(tuple => tuple.Item3, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByThird<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenBy(tuple => keySelector(tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByDescending<TFirst, TSecond, TKey>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item1, tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescending<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByDescendingFirst<TFirst, TSecond>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescendingFirst<TFirst, TSecond, TThird>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TFirst>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => tuple.Item1, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByDescendingFirst<TFirst, TSecond, TKey>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescendingFirst<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item1), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByDescendingSecond<TFirst, TSecond>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescendingSecond<TFirst, TSecond, TThird>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TSecond>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => tuple.Item2, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond)> ThenByDescendingSecond<TFirst, TSecond, TKey>(this IOrderedEnumerable<(TFirst, TSecond)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescendingSecond<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TSecond, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item2), comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescendingThird<TFirst, TSecond, TThird>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, IComparer<TThird>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => tuple.Item3, comparer);
        }

        [Pure]
        public static IOrderedEnumerable<(TFirst, TSecond, TThird)> ThenByDescendingThird<TFirst, TSecond, TThird, TKey>(this IOrderedEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TThird, TKey> keySelector, IComparer<TKey>? comparer = null)
        {
            return tuples.ThenByDescending(tuple => keySelector(tuple.Item3), comparer);
        }

        [Pure]
        public static (List<TFirst>, List<TSecond>) Unzip<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples)
        {
            return tuples.Aggregate((new List<TFirst>(), new List<TSecond>()), (lists, tuple) =>
            {
                lists.Item1.Add(tuple.Item1);
                lists.Item2.Add(tuple.Item2);
                return lists;
            });
        }

        [Pure]
        public static (List<TFirst>, List<TSecond>, List<TThird>) Unzip<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples)
        {
            return tuples.Aggregate((new List<TFirst>(), new List<TSecond>(), new List<TThird>()), (lists, tuple) =>
            {
                lists.Item1.Add(tuple.Item1);
                lists.Item2.Add(tuple.Item2);
                lists.Item3.Add(tuple.Item3);
                return lists;
            });
        }

        [Pure]
        public static (List<(TFirst, TSecond)> Matches, List<(TFirst, TSecond)> Mismatches) Split<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, bool> predicate)
        {
            return tuples.Split(tuple => predicate(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static (List<(TFirst, TSecond, TThird)> Matches, List<(TFirst, TSecond, TThird)> Mismatches) Split<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, bool> predicate)
        {
            return tuples.Split(tuple => predicate(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static Dictionary<TKey, (TFirst, TSecond)> ToDictionary<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector) where TKey : notnull
        {
            return tuples.ToDictionary(tuple => keySelector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static Dictionary<TKey, TValue> ToDictionary<TFirst, TSecond, TKey, TValue>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, TKey> keySelector, Func<TFirst, TSecond, TValue> valueSelector) where TKey : notnull
        {
            return tuples.ToDictionary(tuple => keySelector(tuple.Item1, tuple.Item2), tuple => valueSelector(tuple.Item1, tuple.Item2));
        }

        [Pure]
        public static Dictionary<TKey, (TFirst, TSecond, TThird)> ToDictionary<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector) where TKey : notnull
        {
            return tuples.ToDictionary(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static Dictionary<TKey, TValue> ToDictionary<TFirst, TSecond, TThird, TKey, TValue>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, TKey> keySelector, Func<TFirst, TSecond, TThird, TValue> valueSelector) where TKey : notnull
        {
            return tuples.ToDictionary(tuple => keySelector(tuple.Item1, tuple.Item2, tuple.Item3), tuple => valueSelector(tuple.Item1, tuple.Item2, tuple.Item3));
        }

        [Pure]
        public static Dictionary<TFirst, TSecond> ToDictionary<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples) where TFirst : notnull
        {
            return tuples.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }
    }
}