#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static int RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
        {
            var count = 0;
            dictionary.SafeForEach((key, value) =>
            {
                if (!predicate(key, value)) return;
                dictionary.Remove(key);
                ++count;
            });
            return count;
        }

        public static void Clear<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TKey, TValue> action)
        {
            dictionary.ForEach(action);
            dictionary.Clear();
        }

        public static void Clear<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<TValue> action)
        {
            dictionary.ForEach(action);
            dictionary.Clear();
        }

        public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueFactory();
        }

        public static TValue? RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue? defaultValue = default)
        {
            return dictionary.Remove(key, out var value) ? value : defaultValue;
        }

        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.Remove(key, out var value) ? value : valueFactory();
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            dictionary.TryAdd(key, valueFactory);
            return dictionary[key];
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            return dictionary.GetOrAdd(key, () => new TValue());
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            if (dictionary.ContainsKey(key)) return false;
            dictionary.Add(key, valueFactory());
            return true;
        }

        public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            return dictionary.TryAdd(key, () => new TValue());
        }

        public static KeyValuePair<TKey, TValue> First<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.First(kv => predicate(kv.Key, kv.Value));
        }

        public static KeyValuePair<TKey, TValue>? FirstOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.FirstOrDefault(kv => predicate(kv.Key, kv.Value));
        }

        public static KeyValuePair<TKey, TValue> Last<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Last(kv => predicate(kv.Key, kv.Value));
        }

        public static KeyValuePair<TKey, TValue>? LastOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.LastOrDefault(kv => predicate(kv.Key, kv.Value));
        }

        public static KeyValuePair<TKey, TValue> Single<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Single(kv => predicate(kv.Key, kv.Value));
        }

        public static KeyValuePair<TKey, TValue>? SingleOrDefault<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.SingleOrDefault(kv => predicate(kv.Key, kv.Value));
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> Where<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Where(kv => predicate(kv.Key, kv.Value));
        }

        public static IEnumerable<TResult> Select<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Select(kv => selector(kv.Key, kv.Value));
        }

        public static IEnumerable<TResult> SelectMany<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, IEnumerable<TResult>> selector)
        {
            return dictionary.SelectMany(kv => selector(kv.Key, kv.Value));
        }

        public static TAccumulate Aggregate<TKey, TValue, TAccumulate>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TAccumulate seed, Func<TAccumulate, TKey, TValue, TAccumulate> func)
        {
            return dictionary.Aggregate(seed, (current, kv) => func(current, kv.Key, kv.Value));
        }

        public static TResult Min<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Min(kv => selector(kv.Key, kv.Value));
        }

        public static TResult Max<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Max(kv => selector(kv.Key, kv.Value));
        }

        public static bool Any<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.Any(kv => predicate(kv.Key, kv.Value));
        }

        public static bool All<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, bool> predicate)
        {
            return dictionary.All(kv => predicate(kv.Key, kv.Value));
        }

        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue> action)
        {
            dictionary.ForEach(kv => action(kv.Key, kv.Value));
        }

        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TValue> action)
        {
            dictionary.ForEach(kv => action(kv.Value));
        }

        public static void SafeForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue> action)
        {
            dictionary.SafeForEach(kv => action(kv.Key, kv.Value));
        }

        public static void SafeForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TValue> action)
        {
            dictionary.SafeForEach(kv => action(kv.Value));
        }

        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupBy<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Key, kv.Value));
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderBy<TKey, TValue, TKeySelector>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TKeySelector> keySelector)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Key, kv.Value));
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescending<TKey, TValue, TKeySelector>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TKeySelector> keySelector)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Key, kv.Value));
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenBy<TKey, TValue, TKeySelector>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TKeySelector> keySelector)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Key, kv.Value));
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescending<TKey, TValue, TKeySelector>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TKeySelector> keySelector)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Key, kv.Value));
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}