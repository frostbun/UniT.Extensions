namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class DictionaryExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory = null)
        {
            return dictionary.TryGetValue(key, out var value) ? value : (valueFactory ?? (() => default))();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory = null)
        {
            dictionary.TryAdd(key, valueFactory ?? (() => default));
            return dictionary[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, valueFactory());
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<TKey, TValue>> Where<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Where(kv => predicate(kv.Key, kv.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> Select<TKey, TValue, TResult>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Select(kv => selector(kv.Key, kv.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Aggregate<TKey, TValue, TResult>(this IDictionary<TKey, TValue> dictionary, TResult seed, Func<TResult, TKey, TValue, TResult> func)
        {
            return dictionary.Aggregate(seed, (current, kv) => func(current, kv.Key, kv.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            dictionary.ForEach(kv => action(kv.Key, kv.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SafeForEach<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            dictionary.SafeForEach(kv => action(kv.Key, kv.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
        {
            var count = 0;
            dictionary.SafeForEach((key, value) =>
            {
                if (!predicate(key, value)) return;
                dictionary.Remove(key);
                count++;
            });
            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new(dictionary);
        }
    }
}