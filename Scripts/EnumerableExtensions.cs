#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public static class EnumerableExtensions
    {
        #region AggregateFromFirst

        [Pure]
        public static T AggregateFromFirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, T, T> func, Func<T> defaultValueFactory)
        {
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValueFactory();
            var current = enumerator.Current;
            while (enumerator.MoveNext())
            {
                current = func(current, enumerator.Current);
            }
            return current;
        }

        [Pure]
        public static T AggregateFromFirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, T, T> func, T defaultValue) => enumerable.AggregateFromFirstOrDefault(func, () => defaultValue);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        public static T? AggregateFromFirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, T, T> func) => enumerable.AggregateFromFirstOrDefault(func, () => default!);

        [Pure]
        public static T AggregateFromFirst<T>(this IEnumerable<T> enumerable, Func<T, T, T> func) => enumerable.AggregateFromFirstOrDefault(func, () => throw new InvalidOperationException("Sequence contains no elements"));

        #endregion

        #region Min

        [Pure]
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory, IComparer<T>? comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            return enumerable.AggregateFromFirstOrDefault((x, y) => comparer.Compare(x, y) < 0 ? x : y, defaultValueFactory);
        }

        [Pure]
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, IComparer<T>? comparer = null) => enumerable.MinOrDefault(() => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        public static T? MinOrDefault<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MinOrDefault(() => default!, comparer);

        [Pure]
        public static T Min<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MinOrDefault(() => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        #region Max

        [Pure]
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory, IComparer<T>? comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            return enumerable.AggregateFromFirstOrDefault((x, y) => comparer.Compare(x, y) > 0 ? x : y, defaultValueFactory);
        }

        [Pure]
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, IComparer<T>? comparer = null) => enumerable.MaxOrDefault(() => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        public static T? MaxOrDefault<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MaxOrDefault(() => default!, comparer);

        [Pure]
        public static T Max<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MaxOrDefault(() => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        #region MinBy

        [Pure]
        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T> defaultValueFactory, IComparer<TKey>? comparer = null)
        {
            comparer ??= Comparer<TKey>.Default;
            return enumerable
                .Select(item => (Item: item, Key: keySelector(item)))
                .AggregateFromFirstOrDefault(
                    (x, y) => comparer.Compare(x.Key, y.Key) < 0 ? x : y,
                    () => (Item: defaultValueFactory(), Key: default!)
                )
                .Item;
        }

        [Pure]
        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, T defaultValue, IComparer<TKey>? comparer = null) => enumerable.MinByOrDefault(keySelector, () => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        public static T? MinByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MinByOrDefault(keySelector, () => default!, comparer);

        [Pure]
        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MinByOrDefault(keySelector, () => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        #region MaxBy

        [Pure]
        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T> defaultValueFactory, IComparer<TKey>? comparer = null)
        {
            comparer ??= Comparer<TKey>.Default;
            return enumerable
                .Select(item => (Item: item, Key: keySelector(item)))
                .AggregateFromFirstOrDefault(
                    (x, y) => comparer.Compare(x.Key, y.Key) > 0 ? x : y,
                    () => (Item: defaultValueFactory(), Key: default!)
                )
                .Item;
        }

        [Pure]
        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, T defaultValue, IComparer<TKey>? comparer = null) => enumerable.MaxByOrDefault(keySelector, () => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        public static T? MaxByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MaxByOrDefault(keySelector, () => default!, comparer);

        [Pure]
        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MaxByOrDefault(keySelector, () => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        [Pure]
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.Except(new[] { item });
        }

        [Pure]
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            return other.Concat(enumerable);
        }

        [Pure]
        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            return enumerable.Concat(other);
        }

        [Pure]
        public static IEnumerable<T> Flat<T>(this IEnumerable<IEnumerable<T>> enumerable)
        {
            return enumerable.SelectMany(Item.S);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }

        public static void SafeForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ToArray().ForEach(action);
        }

        [Pure]
        public static int FirstIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .First();
        }

        [Pure]
        public static int FirstIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .DefaultIfEmpty(-1)
                .First();
        }

        [Pure]
        public static int LastIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .Last();
        }

        [Pure]
        public static int LastIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .DefaultIfEmpty(-1)
                .Last();
        }

        [Pure]
        public static int SingleIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .Single();
        }

        [Pure]
        public static int SingleIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Enumerate()
                .WhereSecond(predicate)
                .SelectFirsts()
                .DefaultIfEmpty(-1)
                .Single();
        }

        [Pure]
        public static int FirstIndexOf<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.FirstIndex(i => Equals(i, item));
        }

        [Pure]
        public static int FirstIndexOrDefaultOf<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.FirstIndexOrDefault(i => Equals(i, item));
        }

        [Pure]
        public static int LastIndexOf<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.LastIndex(i => Equals(i, item));
        }

        [Pure]
        public static int LastIndexOrDefaultOf<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.LastIndexOrDefault(i => Equals(i, item));
        }

        [Pure]
        public static int SingleIndexOf<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.SingleIndex(i => Equals(i, item));
        }

        [Pure]
        public static int SingleIndexOrDefaultOf<T>(this IEnumerable<T> enumerable, T item)
        {
            return enumerable.SingleIndexOrDefault(i => Equals(i, item));
        }

        [Pure]
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            var hashSet = enumerable as HashSet<T> ?? enumerable.ToHashSet();
            return other.All(hashSet.Contains);
        }

        [Pure]
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(_ => Guid.NewGuid());
        }

        [Pure]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.Shuffle().Take(count);
        }

        [Pure]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count, IEnumerable<int> weights)
        {
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            weights    = weights.ToCollectionIfNeeded();
            var sumWeight     = weights.Sum();
            var chosenIndices = new HashSet<int>();
            while (count-- > 0)
            {
                if (sumWeight <= 0) break;
                var randomWeight = UnityEngine.Random.Range(0, sumWeight);
                foreach (var (index, (item, weight)) in IterTools.Zip(enumerable, weights).Enumerate())
                {
                    if (chosenIndices.Contains(index)) continue;
                    if ((randomWeight -= weight) >= 0) continue;
                    yield return item;
                    sumWeight -= weight;
                    chosenIndices.Add(index);
                    break;
                }
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Pure]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count, IEnumerable<float> weights)
        {
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            weights    = weights.ToCollectionIfNeeded();
            var sumWeight     = weights.Sum();
            var chosenIndices = new HashSet<int>();
            while (count-- > 0)
            {
                if (sumWeight <= 0) break;
                var randomWeight = UnityEngine.Random.Range(0, sumWeight);
                foreach (var (index, (item, weight)) in IterTools.Zip(enumerable, weights).Enumerate())
                {
                    if (chosenIndices.Contains(index)) continue;
                    if ((randomWeight -= weight) >= 0) continue;
                    yield return item;
                    sumWeight -= weight;
                    chosenIndices.Add(index);
                    break;
                }
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        #region Random

        [Pure]
        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory)
        {
            // ReSharper disable PossibleMultipleEnumeration
            enumerable = enumerable.ToCollectionIfNeeded();
            return enumerable.Any() ? enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count())) : defaultValueFactory();
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Pure]
        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue) => enumerable.RandomOrDefault(() => defaultValue);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        public static T? RandomOrDefault<T>(this IEnumerable<T> enumerable) => enumerable.RandomOrDefault(() => default!);

        [Pure]
        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.RandomOrDefault(() => throw new InvalidOperationException("Sequence contains no elements"));

        #endregion

        [Pure]
        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<int> weights)
        {
            // ReSharper disable PossibleMultipleEnumeration
            weights = weights.ToCollectionIfNeeded();
            var sumWeight    = weights.Sum();
            var randomWeight = UnityEngine.Random.Range(0, sumWeight);
            foreach (var (item, weight) in IterTools.Zip(enumerable, weights))
            {
                if ((randomWeight -= weight) >= 0) continue;
                return item;
            }
            throw new InvalidOperationException("Sequence contains no elements");
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Pure]
        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<float> weights)
        {
            // ReSharper disable PossibleMultipleEnumeration
            weights = weights.ToCollectionIfNeeded();
            var sumWeight    = weights.Sum();
            var randomWeight = UnityEngine.Random.Range(0, sumWeight);
            foreach (var (item, weight) in IterTools.Zip(enumerable, weights))
            {
                if ((randomWeight -= weight) >= 0) continue;
                return item;
            }
            throw new InvalidOperationException("Sequence contains no elements");
            // ReSharper restore PossibleMultipleEnumeration
        }

        [Pure]
        public static IEnumerable<(int Index, T Value)> Enumerate<T>(this IEnumerable<T> enumerable, int start = 0)
        {
            return enumerable.Select(item => (start++, item));
        }

        [Pure]
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, int step)
        {
            return enumerable.Where((_, index) => index % step is 0);
        }

        [Pure]
        public static IEnumerable<T> Repeat<T>(this T item, int count)
        {
            while (count-- > 0) yield return item;
        }

        [Pure]
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

        [Pure]
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

        [Pure]
        public static IEnumerable<TAccumulate> Accumulate<T, TAccumulate>(this IEnumerable<T> enumerable, TAccumulate seed, Func<TAccumulate, T, TAccumulate> accumulator)
        {
            return enumerable.Select(item => seed = accumulator(seed, item));
        }

        [Pure]
        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            return enumerable.Enumerate()
                .GroupByFirst(index => index / chunkSize)
                .Select(group => group.SelectSeconds());
        }

        [Pure]
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

        [Pure]
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

        [Pure]
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray().AsReadOnly();
        }

        [Pure]
        public static IEnumerable<T> ToCollectionIfNeeded<T>(this IEnumerable<T> enumerable) => enumerable switch
        {
            ICollection<T> or IReadOnlyCollection<T> => enumerable,
            _                                        => enumerable.ToArray(),
        };
    }
}