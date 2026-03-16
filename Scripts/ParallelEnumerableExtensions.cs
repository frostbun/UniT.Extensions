#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public static class ParallelEnumerableExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ParallelForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Parallel.ForEach(enumerable, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeParallelForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ToArray().ParallelForEach(action);
        }
    }
}