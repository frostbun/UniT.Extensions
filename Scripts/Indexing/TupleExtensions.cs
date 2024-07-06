#nullable enable
namespace UniT.Extensions.Indexing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TupleExtensions
    {
        public static (TFirst, TSecond) First<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.First((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) First<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.First((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) FirstOrDefault<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.FirstOrDefault((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) FirstOrDefault<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.FirstOrDefault((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) Last<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.Last((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) Last<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.Last((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) LastOrDefault<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.LastOrDefault((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) LastOrDefault<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.LastOrDefault((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) Single<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.Single((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) Single<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.Single((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) SingleOrDefault<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.SingleOrDefault((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) SingleOrDefault<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.SingleOrDefault((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static IEnumerable<(TFirst, TSecond)> Where<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.Where((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static IEnumerable<(TFirst, TSecond, TThird)> Where<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.Where((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static IEnumerable<TResult> Select<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, TResult> selector)
        {
            return tuples.Select((tuple, index) => selector(tuple.Item1, tuple.Item2, index));
        }

        public static IEnumerable<TResult> Select<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, TResult> selector)
        {
            return tuples.Select((tuple, index) => selector(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static IEnumerable<TResult> SelectMany<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, IEnumerable<TResult>> selector)
        {
            return tuples.SelectMany((tuple, index) => selector(tuple.Item1, tuple.Item2, index));
        }

        public static IEnumerable<TResult> SelectMany<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, IEnumerable<TResult>> selector)
        {
            return tuples.SelectMany((tuple, index) => selector(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static TAccumulate Aggregate<TFirst, TSecond, TAccumulate>(this IEnumerable<(TFirst, TSecond)> tuples, TAccumulate seed, Func<TAccumulate, TFirst, TSecond, int, TAccumulate> func)
        {
            return tuples.Aggregate(seed, (current, tuple, index) => func(current, tuple.Item1, tuple.Item2, index));
        }

        public static TAccumulate Aggregate<TFirst, TSecond, TThird, TAccumulate>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, TAccumulate seed, Func<TAccumulate, TFirst, TSecond, TThird, int, TAccumulate> func)
        {
            return tuples.Aggregate(seed, (current, tuple, index) => func(current, tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static TResult Min<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, TResult> selector)
        {
            return tuples.Min((tuple, index) => selector(tuple.Item1, tuple.Item2, index));
        }

        public static TResult Min<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, TResult> selector)
        {
            return tuples.Min((tuple, index) => selector(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static TResult Max<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, TResult> selector)
        {
            return tuples.Max((tuple, index) => selector(tuple.Item1, tuple.Item2, index));
        }

        public static TResult Max<TFirst, TSecond, TThird, TResult>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, TResult> selector)
        {
            return tuples.Max((tuple, index) => selector(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) MinBy<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, TKey> keySelector)
        {
            return tuples.MinBy((tuple, index) => keySelector(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) MinBy<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, TKey> keySelector)
        {
            return tuples.MinBy((tuple, index) => keySelector(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static (TFirst, TSecond) MaxBy<TFirst, TSecond, TKey>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, TKey> keySelector)
        {
            return tuples.MaxBy((tuple, index) => keySelector(tuple.Item1, tuple.Item2, index));
        }

        public static (TFirst, TSecond, TThird) MaxBy<TFirst, TSecond, TThird, TKey>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, TKey> keySelector)
        {
            return tuples.MaxBy((tuple, index) => keySelector(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static bool Any<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.Any((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static bool Any<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.Any((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static bool All<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, int, bool> predicate)
        {
            return tuples.All((tuple, index) => predicate(tuple.Item1, tuple.Item2, index));
        }

        public static bool All<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Func<TFirst, TSecond, TThird, int, bool> predicate)
        {
            return tuples.All((tuple, index) => predicate(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static void ForEach<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Action<TFirst, TSecond, int> action)
        {
            tuples.ForEach((tuple, index) => action(tuple.Item1, tuple.Item2, index));
        }

        public static void ForEach<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Action<TFirst, TSecond, TThird, int> action)
        {
            tuples.ForEach((tuple, index) => action(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }

        public static void SafeForEach<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Action<TFirst, TSecond, int> action)
        {
            tuples.SafeForEach((tuple, index) => action(tuple.Item1, tuple.Item2, index));
        }

        public static void SafeForEach<TFirst, TSecond, TThird>(this IEnumerable<(TFirst, TSecond, TThird)> tuples, Action<TFirst, TSecond, TThird, int> action)
        {
            tuples.SafeForEach((tuple, index) => action(tuple.Item1, tuple.Item2, tuple.Item3, index));
        }
    }
}