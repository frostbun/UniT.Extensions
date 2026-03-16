#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public static class EnumerableUniTaskExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            foreach (var item in enumerable) await action(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask ForEachAwaitAsync<T, TState>(this IEnumerable<T> enumerable, Func<T, TState, UniTask> action, TState state)
        {
            foreach (var item in enumerable) await action(item, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            await enumerable.Select(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask ForEachAsync<T, TState>(this IEnumerable<T> enumerable, Func<T, TState, UniTask> action, TState state)
        {
            await enumerable.Select(action, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask SafeForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            return enumerable.ToArray().ForEachAwaitAsync(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask SafeForEachAwaitAsync<T, TState>(this IEnumerable<T> enumerable, Func<T, TState, UniTask> action, TState state)
        {
            return enumerable.ToArray().ForEachAwaitAsync(action, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask SafeForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            return enumerable.ToArray().ForEachAsync(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask SafeForEachAsync<T, TState>(this IEnumerable<T> enumerable, Func<T, TState, UniTask> action, TState state)
        {
            return enumerable.ToArray().ForEachAsync(action, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, UniTask<TResult>> selector)
        {
            return await enumerable.Select(selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>?, CancellationToken, UniTask> action, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            var collection = enumerable as ICollection<T> ?? enumerable.ToArray();
            return IterTools.Zip(
                collection,
                progress.CreateSubProgresses(collection.Count),
                cancellationToken.Repeat(collection.Count),
                action
            ).ForEachAwaitAsync(Item.S);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>?, CancellationToken, UniTask> action, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            var collection = enumerable as ICollection<T> ?? enumerable.ToArray();
            return IterTools.Zip(
                collection,
                progress.CreateSubProgresses(collection.Count),
                cancellationToken.Repeat(collection.Count),
                action
            ).ForEachAsync(Item.S);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, IProgress<float>?, CancellationToken, UniTask<TResult>> selector, IProgress<float>? progress, CancellationToken cancellationToken)
        {
            var collection = enumerable as ICollection<TSource> ?? enumerable.ToArray();
            return IterTools.Zip(
                collection,
                progress.CreateSubProgresses(collection.Count),
                cancellationToken.Repeat(collection.Count),
                selector
            ).SelectAsync(Item.S);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<T[]> ToArrayAsync<T>(this UniTask<IEnumerable<T>> enumerable)
        {
            return (await enumerable).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<List<T>> ToListAsync<T>(this UniTask<IEnumerable<T>> enumerable)
        {
            return (await enumerable).ToList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<T[]> ToArrayAsync<T>(this IEnumerable<UniTask<T>> enumerable)
        {
            return await enumerable;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<List<T>> ToListAsync<T>(this IEnumerable<UniTask<T>> enumerable)
        {
            return (await enumerable).ToList();
        }
    }
}
#endif