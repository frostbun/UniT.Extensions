#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ParallelEnumerableExtensions
    {
        public static void ParallelForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            Parallel.ForEach(enumerable, action);
        }

        public static void SafeParallelForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ToArray().ParallelForEach(action);
        }

        public static ParallelQuery<T> Shuffle<T>(this ParallelQuery<T> enumerable)
        {
            return enumerable.OrderBy(_ => Guid.NewGuid());
        }

        public static ParallelQuery<T> Sample<T>(this ParallelQuery<T> enumerable, int count)
        {
            return enumerable.Shuffle().Take(count);
        }
    }
}