namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class EnumerableExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ToArray().ForEach(action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(_ => Guid.NewGuid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.Shuffle().Take(count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Choice<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Shuffle().First();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(int, T)> Enumerate<T>(this IEnumerable<T> enumerable, int start = 0)
        {
            return enumerable.Select(item => (start++, item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> enumerable)
        {
            var cache = new List<T>();
            foreach (var item in enumerable)
            {
                yield return item;
                cache.Add(item);
            }
            while (cache.Count > 0)
            {
                foreach (var item in cache)
                {
                    yield return item;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static (List<T> Matches, List<T> Unmatches) Split<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Aggregate((Matches: new List<T>(), Unmatches: new List<T>()), (lists, item) =>
            {
                if (predicate(item)) lists.Matches.Add(item);
                else lists.Unmatches.Add(item);
                return lists;
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[,] To2DArray<T>(this T[][] source)
        {
            try
            {
                var dimension1 = source.Length;
                var dimension2 = source.GroupBy(row => row.Length).Single().Key;
                var result     = new T[dimension1, dimension2];
                for (var i = 0; i < dimension1; ++i)
                {
                    for (var j = 0; j < dimension2; ++j)
                    {
                        result[i, j] = source[i][j];
                    }
                }

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular");
            }
        }
    }
}