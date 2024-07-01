#if !UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumerableCoroutineExtensions
    {
        public static IEnumerator ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, IEnumerator> action, Action? callback = null)
        {
            foreach (var item in enumerable) yield return action(item);
            callback?.Invoke();
        }

        public static IEnumerator ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, IEnumerator> action, Action? callback = null)
        {
            yield return enumerable.Select(action).Gather();
            callback?.Invoke();
        }

        public static IEnumerator SafeForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, IEnumerator> action, Action? callback = null)
        {
            return enumerable.ToArray().ForEachAwaitAsync(action, callback);
        }

        public static IEnumerator SafeForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, IEnumerator> action, Action? callback = null)
        {
            return enumerable.ToArray().ForEachAsync(action, callback);
        }

        public static IEnumerator SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, Action<TResult>, IEnumerator> selector, Action<IEnumerable<TResult>> callback)
        {
            var collection = enumerable as ICollection<TSource> ?? enumerable.ToArray();
            var dictionary = new Dictionary<TSource, TResult>();
            yield return collection.Select(source => selector(source, result => dictionary.Add(source, result))).Gather();
            callback(collection.Select(source => dictionary[source]));
        }

        public static IEnumerator ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>?, IEnumerator> action, Action? callback = null, IProgress<float>? progress = null)
        {
            var collection = enumerable as ICollection<T> ?? enumerable.ToArray();
            return IterTools.Zip(
                collection,
                progress.CreateSubProgresses(collection.Count),
                action
            ).ForEachAwaitAsync(Item.S, callback);
        }

        public static IEnumerator ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>?, IEnumerator> action, Action? callback = null, IProgress<float>? progress = null)
        {
            var collection = enumerable as ICollection<T> ?? enumerable.ToArray();
            return IterTools.Zip(
                collection,
                progress.CreateSubProgresses(collection.Count),
                action
            ).ForEachAsync(Item.S, callback);
        }

        public static IEnumerator SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, Action<TResult>, IProgress<float>?, IEnumerator> selector, Action<IEnumerable<TResult>> callback, IProgress<float>? progress = null)
        {
            var collection = enumerable as ICollection<TSource> ?? enumerable.ToArray();
            var dictionary = new Dictionary<TSource, TResult>();
            yield return IterTools.Zip(
                collection,
                progress.CreateSubProgresses(collection.Count),
                (source, progress) => selector(source, result => dictionary.Add(source, result), progress)
            ).Gather();
            callback(collection.Select(source => dictionary[source]));
        }
    }
}
#endif