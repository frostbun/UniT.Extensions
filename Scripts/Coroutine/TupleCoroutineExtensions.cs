#if !UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class TupleCoroutineExtensions
    {
        public static IEnumerator ForEachAwaitAsync<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, IEnumerator> action, Action? callback = null)
        {
            return tuples.ForEachAwaitAsync(tuple => action(tuple.Item1, tuple.Item2), callback);
        }

        public static IEnumerator ForEachAsync<TFirst, TSecond>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, IEnumerator> action, Action? callback = null)
        {
            return tuples.ForEachAsync(tuple => action(tuple.Item1, tuple.Item2), callback);
        }

        public static IEnumerator SelectAsync<TFirst, TSecond, TResult>(this IEnumerable<(TFirst, TSecond)> tuples, Func<TFirst, TSecond, Action<TResult>, IEnumerator> selector, Action<IEnumerable<TResult>> callback)
        {
            return tuples.SelectAsync((tuple, callback) => selector(tuple.Item1, tuple.Item2, callback), callback);
        }
    }
}
#endif