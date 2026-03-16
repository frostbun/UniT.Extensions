#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.CompilerServices;
    #if UNIT_ZLINQ
    using ZLinq;
    #else
    using System.Buffers;
    #endif

    public static class DictionaryExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            foreach (var key in keys)
            {
                dictionary.Remove(key);
            }
        }

        public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
        {
            var count = 0;
            #if UNIT_ZLINQ
            using var array = dictionary.AsValueEnumerable().ToArrayPool();
            foreach (var (key, value) in array.Span)
            {
                if (!predicate(key, value)) continue;
                dictionary.Remove(key);
                ++count;
            }
            #else
            var array = ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Rent(dictionary.Count);
            try
            {

                dictionary.CopyTo(array, 0);
                foreach (var (key, value) in array.AsSpan(0, dictionary.Count))
                {
                    if (!predicate(key, value)) continue;
                    dictionary.Remove(key);
                    ++count;
                }
            }
            finally
            {
                ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<KeyValuePair<TKey, TValue>>());
            }
            #endif
            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            dictionary.ForEach(action);
            dictionary.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TValue> action)
        {
            dictionary.ForEach(action);
            dictionary.Clear();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueFactory();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueFactory(key);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, TValue> valueFactory, TState state)
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueFactory(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue? RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.Remove(key, out var value) ? value : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.Remove(key, out var value) ? value : defaultValue;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.Remove(key, out var value) ? value : valueFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
        {
            return dictionary.Remove(key, out var value) ? value : valueFactory(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue RemoveOrDefault<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, TValue> valueFactory, TState state)
        {
            return dictionary.Remove(key, out var value) ? value : valueFactory(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            dictionary.TryAdd(key, valueFactory);
            return dictionary[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
        {
            dictionary.TryAdd(key, valueFactory);
            return dictionary[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrAdd<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, TValue> valueFactory, TState state)
        {
            dictionary.TryAdd(key, valueFactory, state);
            return dictionary[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            return dictionary.GetOrAdd(key, () => new TValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, valueFactory());
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, valueFactory(key));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, TValue> valueFactory, TState state)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, valueFactory(state));
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            return dictionary.TryAdd(key, () => new TValue());
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> First<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.First(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue>? FirstOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.FirstOrDefault(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> Last<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Last(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue>? LastOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.LastOrDefault(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> Single<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Single(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue>? SingleOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.SingleOrDefault(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Where<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            foreach (var kv in dictionary)
            {
                if (!predicate(kv.Key, kv.Value)) continue;
                yield return kv;
            }
        }

        [Pure]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Where<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TState, bool> predicate, TState state)
        {
            foreach (var kv in dictionary)
            {
                if (!predicate(kv.Key, kv.Value, state)) continue;
                yield return kv;
            }
        }

        [Pure]
        public static IEnumerable<KeyValuePair<TKey, TValue>> WhereKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, bool> predicate)
        {
            foreach (var kv in dictionary)
            {
                if (!predicate(kv.Key)) continue;
                yield return kv;
            }
        }

        [Pure]
        public static IEnumerable<KeyValuePair<TKey, TValue>> WhereKey<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TState, bool> predicate, TState state)
        {
            foreach (var kv in dictionary)
            {
                if (!predicate(kv.Key, state)) continue;
                yield return kv;
            }
        }

        [Pure]
        public static IEnumerable<KeyValuePair<TKey, TValue>> WhereValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, bool> predicate)
        {
            foreach (var kv in dictionary)
            {
                if (!predicate(kv.Value)) continue;
                yield return kv;
            }
        }

        [Pure]
        public static IEnumerable<KeyValuePair<TKey, TValue>> WhereValue<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TState, bool> predicate, TState state)
        {
            foreach (var kv in dictionary)
            {
                if (!predicate(kv.Value, state)) continue;
                yield return kv;
            }
        }

        [Pure]
        public static IEnumerable<TResult> Select<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            foreach (var kv in dictionary)
            {
                yield return selector(kv.Key, kv.Value);
            }
        }

        [Pure]
        public static IEnumerable<TResult> Select<TKey, TValue, TResult, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TState, TResult> selector, TState state)
        {
            foreach (var kv in dictionary)
            {
                yield return selector(kv.Key, kv.Value, state);
            }
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TKey> SelectKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.Select(kv => kv.Key);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TValue> SelectValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.Select(kv => kv.Value);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> SelectMany<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, IEnumerable<TResult>> selector)
        {
            return dictionary.SelectMany(kv => selector(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TAccumulate Aggregate<TKey, TValue, TAccumulate>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TAccumulate seed, Func<TAccumulate, TKey, TValue, TAccumulate> func)
        {
            return dictionary.Aggregate(seed, (current, kv) => func(current, kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Min<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Min(kv => selector(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Max<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Max(kv => selector(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MinBy<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MinByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.MinBy(kv => kv.Key, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MinByKey<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Key), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MinByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.MinBy(kv => kv.Value, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MinByValue<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MaxBy<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MaxByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => kv.Key, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MaxByKey<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Key), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MaxByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.MaxBy(kv => kv.Value, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KeyValuePair<TKey, TValue> MaxByValue<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Any<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Any(kv => predicate(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool All<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.All(kv => predicate(kv.Key, kv.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue> action)
        {
            foreach (var (key, value) in dictionary) action(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue, TState> action, TState state)
        {
            foreach (var (key, value) in dictionary) action(key, value, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TValue> action)
        {
            foreach (var (_, value) in dictionary) action(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TValue, TState> action, TState state)
        {
            foreach (var (_, value) in dictionary) action(value, state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue> action)
        {
            #if UNIT_ZLINQ
            using var array = dictionary.AsValueEnumerable().ToArrayPool();
            foreach (var (key, value) in array.Span) action(key, value);
            #else
            if (dictionary is ICollection<KeyValuePair<TKey, TValue>> collection)
            {
                var array = ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    foreach (var (key, value) in array.AsSpan(0, collection.Count)) action(key, value);
                }
                finally
                {
                    ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<KeyValuePair<TKey, TValue>>());
                }
            }
            else
            {
                foreach (var (key, value) in dictionary.ToArray().AsSpan()) action(key, value);
            }
            #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue, TState> action, TState state)
        {
            #if UNIT_ZLINQ
            using var array = dictionary.AsValueEnumerable().ToArrayPool();
            foreach (var (key, value) in array.Span) action(key, value, state);
            #else
            if (dictionary is ICollection<KeyValuePair<TKey, TValue>> collection)
            {
                var array = ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    foreach (var (key, value) in array.AsSpan(0, collection.Count)) action(key, value, state);
                }
                finally
                {
                    ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<KeyValuePair<TKey, TValue>>());
                }
            }
            else
            {
                foreach (var (key, value) in dictionary.ToArray().AsSpan()) action(key, value, state);
            }
            #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TValue> action)
        {
            #if UNIT_ZLINQ
            using var array = dictionary.AsValueEnumerable().ToArrayPool();
            foreach (var (_, value) in array.Span) action(value);
            #else
            if (dictionary is ICollection<KeyValuePair<TKey, TValue>> collection)
            {
                var array = ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    foreach (var (_, value) in array.AsSpan(0, collection.Count)) action(value);
                }
                finally
                {
                    ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<KeyValuePair<TKey, TValue>>());
                }
            }
            else
            {
                foreach (var (_, value) in dictionary.ToArray().AsSpan()) action(value);
            }
            #endif
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<TKey, TValue, TState>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TValue, TState> action, TState state)
        {
            #if UNIT_ZLINQ
            using var array = dictionary.AsValueEnumerable().ToArrayPool();
            foreach (var (_, value) in array.Span) action(value, state);
            #else
            if (dictionary is ICollection<KeyValuePair<TKey, TValue>> collection)
            {
                var array = ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Rent(collection.Count);
                try
                {
                    collection.CopyTo(array, 0);
                    foreach (var (_, value) in array.AsSpan(0, collection.Count)) action(value, state);
                }
                finally
                {
                    ArrayPool<KeyValuePair<TKey, TValue>>.Shared.Return(array, RuntimeHelpers.IsReferenceOrContainsReferences<KeyValuePair<TKey, TValue>>());
                }
            }
            else
            {
                foreach (var (_, value) in dictionary.ToArray().AsSpan()) action(value, state);
            }
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupBy<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Key, kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, KeyValuePair<TKey, TValue>>> GroupByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.GroupBy(kv => kv.Key);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupByKey<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Key));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TValue, KeyValuePair<TKey, TValue>>> GroupByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.GroupBy(kv => kv.Value);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupByValue<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Value));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderBy<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => kv.Key, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKey<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Key), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.OrderBy(kv => kv.Value, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValue<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescending<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => kv.Key, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingKey<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Key), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => kv.Value, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingValue<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenBy<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByKey<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => kv.Key, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByKey<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Key), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByValue<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.ThenBy(kv => kv.Value, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByValue<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescending<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingKey<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => kv.Key, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingKey<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Key), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingValue<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => kv.Value, comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingValue<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Value), comparer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary) where TKey : notnull
        {
            return dictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}