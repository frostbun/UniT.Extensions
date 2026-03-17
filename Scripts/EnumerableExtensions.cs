#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.CompilerServices;
    #if UNIT_ZLINQ
    using ZLinq;
    #else
    using System.Buffers;
    #endif

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateFromFirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, T, T> func, T defaultValue) => enumerable.AggregateFromFirstOrDefault(func, () => defaultValue);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? AggregateFromFirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, T, T> func) => enumerable.AggregateFromFirstOrDefault(func, static () => default!);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AggregateFromFirst<T>(this IEnumerable<T> enumerable, Func<T, T, T> func) => enumerable.AggregateFromFirstOrDefault(func, static () => throw new InvalidOperationException("Sequence contains no elements"));

        #endregion

        #region Min

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory, IComparer<T>? comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            return enumerable.AggregateFromFirstOrDefault((x, y) => comparer.Compare(x, y) < 0 ? x : y, defaultValueFactory);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, IComparer<T>? comparer = null) => enumerable.MinOrDefault(() => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinOrDefault<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MinOrDefault(static () => default!, comparer);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Min<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MinOrDefault(static () => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        #region Max

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory, IComparer<T>? comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            return enumerable.AggregateFromFirstOrDefault((x, y) => comparer.Compare(x, y) > 0 ? x : y, defaultValueFactory);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue, IComparer<T>? comparer = null) => enumerable.MaxOrDefault(() => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxOrDefault<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MaxOrDefault(static () => default!, comparer);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Max<T>(this IEnumerable<T> enumerable, IComparer<T>? comparer = null) => enumerable.MaxOrDefault(static () => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        #region MinBy

        [Pure]
        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T> defaultValueFactory, IComparer<TKey>? comparer = null)
        {
            comparer ??= Comparer<TKey>.Default;
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValueFactory();
            var bestItem = enumerator.Current;
            var bestKey  = keySelector(bestItem);
            while (enumerator.MoveNext())
            {
                var key = keySelector(enumerator.Current);
                if (comparer.Compare(key, bestKey) < 0)
                {
                    bestItem = enumerator.Current;
                    bestKey  = key;
                }
            }
            return bestItem;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, T defaultValue, IComparer<TKey>? comparer = null) => enumerable.MinByOrDefault(keySelector, () => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MinByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MinByOrDefault(keySelector, static () => default!, comparer);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MinBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MinByOrDefault(keySelector, static () => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        #region MaxBy

        [Pure]
        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, Func<T> defaultValueFactory, IComparer<TKey>? comparer = null)
        {
            comparer ??= Comparer<TKey>.Default;
            using var enumerator = enumerable.GetEnumerator();
            if (!enumerator.MoveNext()) return defaultValueFactory();
            var bestItem = enumerator.Current;
            var bestKey  = keySelector(bestItem);
            while (enumerator.MoveNext())
            {
                var key = keySelector(enumerator.Current);
                if (comparer.Compare(key, bestKey) > 0)
                {
                    bestItem = enumerator.Current;
                    bestKey  = key;
                }
            }
            return bestItem;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, T defaultValue, IComparer<TKey>? comparer = null) => enumerable.MaxByOrDefault(keySelector, () => defaultValue, comparer);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? MaxByOrDefault<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MaxByOrDefault(keySelector, static () => default!, comparer);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T MaxBy<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, IComparer<TKey>? comparer = null) => enumerable.MaxByOrDefault(keySelector, static () => throw new InvalidOperationException("Sequence contains no elements"), comparer);

        #endregion

        [Pure]
        public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            foreach (var element in enumerable)
            {
                if (comparer.Equals(element, item)) continue;
                yield return element;
            }
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            return other.Concat(enumerable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            return enumerable.Concat(other);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Flat<T>(this IEnumerable<IEnumerable<T>> enumerable)
        {
            return enumerable.SelectMany(Item.S);
        }

        [Pure]
        public static IEnumerable<T> Where<T, TState>(this IEnumerable<T> enumerable, Func<T, TState, bool> predicate, TState state)
        {
            foreach (var item in enumerable)
            {
                if (!predicate(item, state)) continue;
                yield return item;
            }
        }

        [Pure]
        public static IEnumerable<TResult> Select<T, TResult, TState>(this IEnumerable<T> enumerable, Func<T, TState, TResult> selector, TState state)
        {
            foreach (var item in enumerable)
            {
                yield return selector(item, state);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T, TState>(this IEnumerable<T> enumerable, Action<T, TState> action, TState state)
        {
            foreach (var item in enumerable) action(item, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            #if UNIT_ZLINQ
            using var array = enumerable.AsValueEnumerable().ToArrayPool();
            foreach (var item in array.Span) action(item);
            #else
            if (enumerable is ICollection<T> collection)
            {
                var array = ArrayPool<T>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    foreach (var item in array.AsSpan(0, collection.Count)) action(item);
                }
                finally
                {
                    ArrayPool<T>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
                }
            }
            else
            {
                foreach (var item in enumerable.ToArray().AsSpan()) action(item);
            }
            #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<T, TState>(this IEnumerable<T> enumerable, Action<T, TState> action, TState state)
        {
            #if UNIT_ZLINQ
            using var array = enumerable.AsValueEnumerable().ToArrayPool();
            foreach (var item in array.Span) action(item, state);
            #else
            if (enumerable is ICollection<T> collection)
            {
                var array = ArrayPool<T>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    foreach (var item in array.AsSpan(0, collection.Count)) action(item, state);
                }
                finally
                {
                    ArrayPool<T>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
                }
            }
            else
            {
                foreach (var item in enumerable.ToArray().AsSpan()) action(item, state);
            }
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FirstIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item)) return index;
                ++index;
            }
            throw new InvalidOperationException("Sequence contains no matching element");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FirstIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item)) return index;
                ++index;
            }
            return -1;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var lastIndex = -1;
            var index     = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item)) lastIndex = index;
                ++index;
            }
            return lastIndex >= 0 ? lastIndex : throw new InvalidOperationException("Sequence contains no matching element");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var lastIndex = -1;
            var index     = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item)) lastIndex = index;
                ++index;
            }
            return lastIndex;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SingleIndex<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var foundIndex = -1;
            var index      = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    if (foundIndex >= 0) throw new InvalidOperationException("Sequence contains more than one matching element");
                    foundIndex = index;
                }
                ++index;
            }
            return foundIndex >= 0 ? foundIndex : throw new InvalidOperationException("Sequence contains no matching element");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SingleIndexOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var foundIndex = -1;
            var index      = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                {
                    if (foundIndex >= 0) throw new InvalidOperationException("Sequence contains more than one matching element");
                    foundIndex = index;
                }
                ++index;
            }
            return foundIndex;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FirstIndexOf<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var index = 0;
            foreach (var i in enumerable)
            {
                if (comparer.Equals(i, item)) return index;
                ++index;
            }
            throw new InvalidOperationException("Sequence contains no matching element");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FirstIndexOrDefaultOf<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var index = 0;
            foreach (var i in enumerable)
            {
                if (comparer.Equals(i, item)) return index;
                ++index;
            }
            return -1;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndexOf<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var lastIndex = -1;
            var index     = 0;
            foreach (var i in enumerable)
            {
                if (comparer.Equals(i, item)) lastIndex = index;
                ++index;
            }
            return lastIndex >= 0 ? lastIndex : throw new InvalidOperationException("Sequence contains no matching element");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int LastIndexOrDefaultOf<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var lastIndex = -1;
            var index     = 0;
            foreach (var i in enumerable)
            {
                if (comparer.Equals(i, item)) lastIndex = index;
                ++index;
            }
            return lastIndex;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SingleIndexOf<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var foundIndex = -1;
            var index      = 0;
            foreach (var i in enumerable)
            {
                if (comparer.Equals(i, item))
                {
                    if (foundIndex >= 0) throw new InvalidOperationException("Sequence contains more than one matching element");
                    foundIndex = index;
                }
                ++index;
            }
            return foundIndex >= 0 ? foundIndex : throw new InvalidOperationException("Sequence contains no matching element");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SingleIndexOrDefaultOf<T>(this IEnumerable<T> enumerable, T item, IEqualityComparer<T>? comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;
            var foundIndex = -1;
            var index      = 0;
            foreach (var i in enumerable)
            {
                if (comparer.Equals(i, item))
                {
                    if (foundIndex >= 0) throw new InvalidOperationException("Sequence contains more than one matching element");
                    foundIndex = index;
                }
                ++index;
            }
            return foundIndex;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContainsAll<T>(this IEnumerable<T> enumerable, IEnumerable<T> other)
        {
            var hashSet = enumerable as HashSet<T> ?? enumerable.ToHashSet();
            return other.All(hashSet.Contains);
        }

        [Pure]
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            #if UNIT_ZLINQ
            using var array = enumerable.AsValueEnumerable().ToArrayPool();
            for (var i = 0; i < array.Size - 1; ++i)
            {
                var j = UnityEngine.Random.Range(i, array.Size);
                yield return array.Array[j];
                array.Array[j] = array.Array[i];
            }
            #else
            if (enumerable is ICollection<T> collection)
            {
                var array = ArrayPool<T>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    for (var i = 0; i < array.Length - 1; ++i)
                    {
                        var j = UnityEngine.Random.Range(i, array.Length);
                        yield return array[j];
                        array[j] = array[i];
                    }
                }
                finally
                {
                    ArrayPool<T>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<T>());
                }
            }
            else
            {
                var array = enumerable.ToArray();
                for (var i = 0; i < array.Length - 1; ++i)
                {
                    var j = UnityEngine.Random.Range(i, array.Length);
                    yield return array[j];
                    array[j] = array[i];
                }
            }
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.Shuffle().Take(count);
        }

        [Pure]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count, IEnumerable<int> weights)
        {
            var collection        = enumerable as ICollection<T> ?? enumerable.ToArray();
            var weightsCollection = weights as ICollection<int> ?? weights.ToArray();
            var sumWeight         = weightsCollection.Sum();
            var chosenIndices     = new HashSet<int>();
            while (count-- > 0)
            {
                if (sumWeight <= 0) break;
                var randomWeight = UnityEngine.Random.Range(0, sumWeight);
                foreach (var (index, (item, weight)) in IterTools.Zip(collection, weightsCollection).Enumerate())
                {
                    if (chosenIndices.Contains(index)) continue;
                    if ((randomWeight -= weight) >= 0) continue;
                    yield return item;
                    sumWeight -= weight;
                    chosenIndices.Add(index);
                    break;
                }
            }
        }

        [Pure]
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> enumerable, int count, IEnumerable<float> weights)
        {
            var collection        = enumerable as ICollection<T> ?? enumerable.ToArray();
            var weightsCollection = weights as ICollection<float> ?? weights.ToArray();
            var sumWeight         = weightsCollection.Sum();
            var chosenIndices     = new HashSet<int>();
            while (count-- > 0)
            {
                if (sumWeight <= 0) break;
                var randomWeight = UnityEngine.Random.Range(0, sumWeight);
                foreach (var (index, (item, weight)) in IterTools.Zip(collection, weightsCollection).Enumerate())
                {
                    if (chosenIndices.Contains(index)) continue;
                    if ((randomWeight -= weight) >= 0) continue;
                    yield return item;
                    sumWeight -= weight;
                    chosenIndices.Add(index);
                    break;
                }
            }
        }

        #region Random

        [Pure]
        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, Func<T> defaultValueFactory)
        {
            if (enumerable is ICollection<T> collection)
            {
                return collection.Count > 0 ? collection.ElementAt(UnityEngine.Random.Range(0, collection.Count)) : defaultValueFactory();
            }
            #if UNIT_ZLINQ
            using var array = enumerable.AsValueEnumerable().ToArrayPool();
            return array.Size > 0 ? array.Array[UnityEngine.Random.Range(0, array.Size)] : defaultValueFactory();
            #else
            var array = enumerable.ToArray();
            return array.Length > 0 ? array[UnityEngine.Random.Range(0, array.Length)] : defaultValueFactory();
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T RandomOrDefault<T>(this IEnumerable<T> enumerable, T defaultValue) => enumerable.RandomOrDefault(() => defaultValue);

        // ReSharper disable once ReturnTypeCanBeNotNullable
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? RandomOrDefault<T>(this IEnumerable<T> enumerable) => enumerable.RandomOrDefault(static () => default!);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Random<T>(this IEnumerable<T> enumerable) => enumerable.RandomOrDefault(static () => throw new InvalidOperationException("Sequence contains no elements"));

        #endregion

        [Pure]
        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<int> weights)
        {
            var weightsCollection = weights as ICollection<int> ?? weights.ToArray();
            var sumWeight         = weightsCollection.Sum();
            var randomWeight      = UnityEngine.Random.Range(0, sumWeight);
            foreach (var (item, weight) in IterTools.Zip(enumerable, weightsCollection))
            {
                if ((randomWeight -= weight) >= 0) continue;
                return item;
            }
            throw new InvalidOperationException("Sequence contains no elements");
        }

        [Pure]
        public static T Random<T>(this IEnumerable<T> enumerable, IEnumerable<float> weights)
        {
            var weightsCollection = weights as ICollection<float> ?? weights.ToArray();
            var sumWeight         = weightsCollection.Sum();
            var randomWeight      = UnityEngine.Random.Range(0, sumWeight);
            foreach (var (item, weight) in IterTools.Zip(enumerable, weightsCollection))
            {
                if ((randomWeight -= weight) >= 0) continue;
                return item;
            }
            throw new InvalidOperationException("Sequence contains no elements");
        }

        [Pure]
        public static IEnumerable<(int Index, T Value)> Enumerate<T>(this IEnumerable<T> enumerable, int start = 0)
        {
            foreach (var item in enumerable) yield return (start++, item);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TAccumulate> Accumulate<T, TAccumulate>(this IEnumerable<T> enumerable, TAccumulate seed, Func<TAccumulate, T, TAccumulate> accumulator)
        {
            return enumerable.Select(item => seed = accumulator(seed, item));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> enumerable, int chunkSize)
        {
            var chunk = new List<T>(chunkSize);
            foreach (var item in enumerable)
            {
                chunk.Add(item);
                if (chunk.Count == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                }
            }
            if (chunk.Count > 0) yield return chunk;
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
    }
}