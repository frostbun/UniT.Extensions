#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
        {
            keys.ForEach(key => dictionary.Remove(key));
        }

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

        public static TValue? GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out var value) ? value : default;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueFactory();
        }

        public static TValue? RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.Remove(key, out var value) ? value : default;
        }

        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
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

        public static IEnumerable<KeyValuePair<TKey, TValue>> WhereKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, bool> predicate)
        {
            return dictionary.Where(kv => predicate(kv.Key));
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> WhereValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, bool> predicate)
        {
            return dictionary.Where(kv => predicate(kv.Value));
        }

        public static IEnumerable<TResult> Select<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TResult> selector)
        {
            return dictionary.Select(kv => selector(kv.Key, kv.Value));
        }

        public static IEnumerable<TKey> SelectKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.Select(kv => kv.Key);
        }

        public static IEnumerable<TValue> SelectValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.Select(kv => kv.Value);
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

        public static KeyValuePair<TKey, TValue> MinBy<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        public static KeyValuePair<TKey, TValue> MinByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.MinBy(kv => kv.Key, comparer);
        }

        public static KeyValuePair<TKey, TValue> MinByKey<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Key), comparer);
        }

        public static KeyValuePair<TKey, TValue> MinByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.MinBy(kv => kv.Value, comparer);
        }

        public static KeyValuePair<TKey, TValue> MinByValue<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Value), comparer);
        }

        public static KeyValuePair<TKey, TValue> MaxBy<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        public static KeyValuePair<TKey, TValue> MaxByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => kv.Key, comparer);
        }

        public static KeyValuePair<TKey, TValue> MaxByKey<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Key), comparer);
        }

        public static KeyValuePair<TKey, TValue> MaxByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.MaxBy(kv => kv.Value, comparer);
        }

        public static KeyValuePair<TKey, TValue> MaxByValue<TKey, TValue, TCompareKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Value), comparer);
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

        public static IEnumerable<IGrouping<TKey, KeyValuePair<TKey, TValue>>> GroupByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.GroupBy(kv => kv.Key);
        }

        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupByKey<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Key));
        }

        public static IEnumerable<IGrouping<TValue, KeyValuePair<TKey, TValue>>> GroupByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary)
        {
            return dictionary.GroupBy(kv => kv.Value);
        }

        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupByValue<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Value));
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderBy<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => kv.Key, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKey<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Key), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.OrderBy(kv => kv.Value, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValue<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderBy(kv => keySelector(kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescending<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => kv.Key, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingKey<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Key), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => kv.Value, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByDescendingValue<TKey, TValue, TOrderKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.OrderByDescending(kv => keySelector(kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenBy<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByKey<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => kv.Key, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByKey<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Key), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByValue<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.ThenBy(kv => kv.Value, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByValue<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenBy(kv => keySelector(kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescending<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Key, kv.Value), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingKey<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => kv.Key, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingKey<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Key), comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingValue<TKey, TValue>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, IComparer<TValue>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => kv.Value, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> ThenByDescendingValue<TKey, TValue, TOrderKey>(this IOrderedEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TValue, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return dictionary.ThenByDescending(kv => keySelector(kv.Value), comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary) where TKey : notnull
        {
            return dictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}