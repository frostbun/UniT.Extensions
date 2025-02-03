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

        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.Except(new[] { item });
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
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .First();
        }

        public static int FirstIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .DefaultIfEmpty(-1)
                .First();
        }

        public static int LastIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .Last();
        }

        public static int LastIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .DefaultIfEmpty(-1)
                .Last();
        }

        private static int SingleIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .Single();
        }

        public static int SingleIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .DefaultIfEmpty(-1)
                .Single();
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

        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable switch
        {
            ICollection<T> collection         => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : throw new InvalidOperationException("Sequence contains no elements"),
            IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : throw new InvalidOperationException("Sequence contains no elements"),
            _                                 => enumerable.ToArray().Random(),
        };

        public static T? RandomOrDefault<T>(this IEnumerable<T> enumerable) => enumerable switch
        {
            ICollection<T> collection         => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : default,
            IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : default,
            _                                 => enumerable.ToArray().RandomOrDefault(),
        };

        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue) => enumerable switch
        {
            ICollection<T> collection         => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : defaultValue,
            IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : defaultValue,
            _                                 => enumerable.ToArray().RandomOrDefault(defaultValue),
        };

        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Func<T> valueFactory) => enumerable switch
        {
            ICollection<T> collection         => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : valueFactory(),
            IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : valueFactory(),
            _                                 => enumerable.ToArray().RandomOrDefault(valueFactory),
        };

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<int> weights, int totalWeight)
        {
            var randomWeight = UnityEngine.Random.Range(0, totalWeight);
            return IterTools.Zip(enumerable, weights)
                .WhereSecond(weight => (randomWeight -= weight) < 0)
                .SelectFirsts()
                .First();
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<float> weights, float totalWeight)
        {
            var randomWeight = UnityEngine.Random.Range(0, totalWeight);
            return IterTools.Zip(enumerable, weights)
                .WhereSecond(weight => (randomWeight -= weight) < 0)
                .SelectFirsts()
                .First();
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<int> weights)
        {
            weights = weights switch
            {
                ICollection<int> or IReadOnlyCollection<int> => weights,
                _                                            => weights.ToArray(),
            };
            // ReSharper disable PossibleMultipleEnumeration
            return enumerable.Random(weights, weights.Sum());
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<float> weights)
        {
            weights = weights switch
            {
                ICollection<float> or IReadOnlyCollection<float> => weights,
                _                                                => weights.ToArray(),
            };
            // ReSharper disable PossibleMultipleEnumeration
            return enumerable.Random(weights, weights.Sum());
            // ReSharper restore PossibleMultipleEnumeration
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
                .GroupByFirst(index => index / chunkSize)
                .Select(group => group.SelectSeconds());
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
                if (predicate(item))
                    lists.Matches.Add(item);
                else
                    lists.Mismatches.Add(item);
                return lists;
            });
        }

        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray().AsReadOnly();
        }
    }
}