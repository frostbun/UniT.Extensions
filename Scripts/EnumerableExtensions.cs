#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        {
            var dictionary = new Dictionary<TKey, T>();
            foreach (var item in enumerable)
            {
                dictionary.TryAdd(keySelector(item), item);
            }
            return dictionary[dictionary.Keys.Min()];
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector)
        {
            var dictionary = new Dictionary<TKey, T>();
            foreach (var item in enumerable)
            {
                dictionary.TryAdd(keySelector(item), item);
            }
            return dictionary[dictionary.Keys.Max()];
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }

        public static void SafeForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ToArray().ForEach(action);
        }

        public static int FirstIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate().First((_, item) => predicate(item)).Item1;
        }

        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            var hashSet = enumerable as HashSet<T> ?? enumerable.ToHashSet();
            return other.All(hashSet.Contains);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(_ => Guid.NewGuid());
        }

        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.Shuffle().Take(count);
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray().Random();
        }

        public static T? RandomOrDefault<T>(this IEnumerable<T> enumerable, T? defaultValue = default)
        {
            return enumerable.ToArray().RandomOrDefault(defaultValue);
        }

        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Func<T> valueFactory)
        {
            return enumerable.ToArray().RandomOrDefault(valueFactory);
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<int> weights, int totalWeight)
        {
            var randomWeight = UnityEngine.Random.Range(0, totalWeight);
            return IterTools.Zip(enumerable, weights)
                .First((_, weight) => (randomWeight -= weight) < 0)
                .Item1;
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<float> weights, float totalWeight)
        {
            var randomWeight = UnityEngine.Random.Range(0, totalWeight);
            return IterTools.Zip(enumerable, weights)
                .First((_, weight) => (randomWeight -= weight) < 0)
                .Item1;
        }

        public static T Random<T>(this IEnumerable<T> enumerable, ICollection<int> weights)
        {
            return enumerable.Random(weights, weights.Sum());
        }

        public static T Random<T>(this IEnumerable<T> enumerable, ICollection<float> weights)
        {
            return enumerable.Random(weights, weights.Sum());
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<int> weights)
        {
            return enumerable.Random(weights.ToArray());
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<float> weights)
        {
            return enumerable.Random(weights.ToArray());
        }

        public static IEnumerable<(int Index, T Value)> Enumerate<T>(this IEnumerable<T> enumerable, int start = 0)
        {
            return enumerable.Select(item => (start++, item));
        }

        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, int step)
        {
            return enumerable.Where((_, index) => index % step is 0);
        }

        public static IEnumerable<T> Repeat<T>(this T item, int count)
        {
            while (count-- > 0) yield return item;
        }

        public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> enumerable)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) yield break;
            var previous = enumerator.Current;
            while (enumerator.MoveNext())
            {
                yield return (previous, previous = enumerator.Current);
            }
        }

        public static IEnumerable<T> Accumulate<T>(this IEnumerable<T> enumerable, Func<T, T, T> accumulator)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) yield break;
            var current = enumerator.Current;
            yield return current;
            while (enumerator.MoveNext())
            {
                yield return current = accumulator(current, enumerator.Current);
            }
        }

        public static IEnumerable<TAccumulate> Accumulate<T, TAccumulate>(this IEnumerable<T> enumerable, TAccumulate seed, Func<TAccumulate, T, TAccumulate> accumulator)
        {
            return enumerable.Select(item => seed = accumulator(seed, item));
        }

        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            return enumerable.Enumerate()
                .GroupBy((index, _) => index / chunkSize)
                .Select(group => group.Select((_, value) => value));
        }

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

        public static (List<T> Matches, List<T> Mismatches) Split<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Aggregate((Matches: new List<T>(), Mismatches: new List<T>()), (lists, item) =>
            {
                if (predicate(item)) lists.Matches.Add(item);
                else lists.Mismatches.Add(item);
                return lists;
            });
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray().AsReadOnly();
        }
    }
}