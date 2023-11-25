#if UNIT_EXTENSIONS_UNITASK
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public static class EnumerableUniTaskExtensions
    {
        public static async UniTask ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            foreach (var item in enumerable) await action(item);
        }

        public static async UniTask ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            await enumerable.Select(action);
        }

        public static async UniTask<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, UniTask<TResult>> selector)
        {
            return await enumerable.Select(selector);
        }

        public static UniTask ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>, CancellationToken, UniTask> action, IProgress<float> progress, CancellationToken cancellationToken)
        {
            enumerable = enumerable as ICollection<T> ?? enumerable.ToArray();
            return IterTools.StrictZip(
                enumerable,
                progress.CreateSubProgresses(enumerable.Count()),
                cancellationToken.Repeat(enumerable.Count()),
                action
            ).ForEachAwaitAsync(Item.S);
        }

        public static UniTask ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, IProgress<float>, CancellationToken, UniTask> action, IProgress<float> progress, CancellationToken cancellationToken)
        {
            enumerable = enumerable as ICollection<T> ?? enumerable.ToArray();
            return IterTools.StrictZip(
                enumerable,
                progress.CreateSubProgresses(enumerable.Count()),
                cancellationToken.Repeat(enumerable.Count()),
                action
            ).ForEachAsync(Item.S);
        }

        public static UniTask<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> enumerable, Func<TSource, IProgress<float>, CancellationToken, UniTask<TResult>> action, IProgress<float> progress, CancellationToken cancellationToken)
        {
            enumerable = enumerable as ICollection<TSource> ?? enumerable.ToArray();
            return IterTools.StrictZip(
                enumerable,
                progress.CreateSubProgresses(enumerable.Count()),
                cancellationToken.Repeat(enumerable.Count()),
                action
            ).SelectAsync(Item.S);
        }

        public static async UniTask<T[]> ToArrayAsync<T>(this UniTask<IEnumerable<T>> enumerable)
        {
            return (await enumerable).ToArray();
        }

        public static async UniTask<List<T>> ToListAsync<T>(this UniTask<IEnumerable<T>> enumerable)
        {
            return (await enumerable).ToList();
        }

        public static async UniTask<T[]> ToArrayAsync<T>(this IEnumerable<UniTask<T>> enumerable)
        {
            return await enumerable;
        }

        public static async UniTask<List<T>> ToListAsync<T>(this IEnumerable<UniTask<T>> enumerable)
        {
            return (await enumerable).ToList();
        }
    }
}
#endif