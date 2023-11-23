#if UNIT_EXTENSIONS_UNITASK
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;

    public static class EnumerableUniTaskExtensions
    {
        public static async UniTask ForEachAwaitAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            foreach (var item in enumerable)
            {
                await action(item);
            }
        }

        public static async UniTask ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            await enumerable.Select(action);
        }
    }
}
#endif