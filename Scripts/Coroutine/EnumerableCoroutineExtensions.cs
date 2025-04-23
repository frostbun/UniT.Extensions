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
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            var dictionary = new Dictionary<TSource, TResult>();
            yield return enumerable.Select(source => selector(source, result => dictionary.Add(source, result))).Gather();
            callback(enumerable.Select(source => dictionary[source]));
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static IEnumerator ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>?, IEnumerator> action, Action? callback = null, IProgress<float>? progress = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            return IterTools.Zip(
                enumerable,
                progress.CreateSubProgresses(enumerable.Count()),
                action
            ).ForEachAwaitAsync(Item.S, callback);
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static IEnumerator ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>?, IEnumerator> action, Action? callback = null, IProgress<float>? progress = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            return IterTools.Zip(
                enumerable,
                progress.CreateSubProgresses(enumerable.Count()),
                action
            ).ForEachAsync(Item.S, callback);
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static IEnumerator SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, Action<TResult>, IProgress<float>?, IEnumerator> selector, Action<IEnumerable<TResult>> callback, IProgress<float>? progress = null)
        {
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            var dictionary = new Dictionary<TSource, TResult>();
            yield return IterTools.Zip(
                enumerable,
                progress.CreateSubProgresses(enumerable.Count()),
                (source, progress) => selector(source, result => dictionary.Add(source, result), progress)
            ).Gather();
            callback(enumerable.Select(source => dictionary[source]));
            // ReSharper restore PossibleMultipleEnumeration
        }
    }
}
#endif