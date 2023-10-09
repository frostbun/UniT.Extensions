#if UNIT_EXTENSIONS_UNITASK
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;

    public static class EnumerableUniTaskExtensions
    {
        public static async UniTask ForEach<T>(this IEnumerable<T> enumerable, Func<T, UniTask> action)
        {
            foreach (var item in enumerable)
            {
                await action(item);
            }
        }
    }
}
#endif