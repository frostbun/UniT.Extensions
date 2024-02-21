namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static bool TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            #if UNITY_2021_2_OR_NEWER
            return dictionary.Remove(key, out value);
            #else
            if (!dictionary.TryGet(key, out value)) return false;
            dictionary.Remove(key);
            return true;
            #endif
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGet(key, out var value) ? value : default;
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.TryGet(key, out var value) ? value : valueFactory();
        }

        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryRemove(key, out var value) ? value : default;
        }

        public static TValue RemoveOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory)
        {
            return dictionary.TryRemove(key, out var value) ? value : valueFactory();
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

        public static IEnumerable<IGrouping<TGroupKey, KeyValuePair<TKey, TValue>>> GroupBy<TKey, TValue, TGroupKey>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, TGroupKey> keySelector)
        {
            return dictionary.GroupBy(kv => keySelector(kv.Key, kv.Value));
        }

        public static TResult Aggregate<TKey, TValue, TResult>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, TResult seed, Func<TResult, TKey, TValue, TResult> func)
        {
            return dictionary.Aggregate(seed, (current, kv) => func(current, kv.Key, kv.Value));
        }

        #region Sum

        public static int Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, int> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static long Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, long> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static float Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, float> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static double Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, double> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static decimal Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, decimal> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static int? Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, int?> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static long? Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, long?> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static float? Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, float?> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static double? Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, double?> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static decimal? Sum<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Func<TKey, TValue, decimal?> selector)
        {
            return dictionary.Sum(kv => selector(kv.Key, kv.Value));
        }

        public static int Sum<TKey>(this IEnumerable<KeyValuePair<TKey, int>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static long Sum<TKey>(this IEnumerable<KeyValuePair<TKey, long>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static float Sum<TKey>(this IEnumerable<KeyValuePair<TKey, float>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static double Sum<TKey>(this IEnumerable<KeyValuePair<TKey, double>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static decimal Sum<TKey>(this IEnumerable<KeyValuePair<TKey, decimal>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static int? Sum<TKey>(this IEnumerable<KeyValuePair<TKey, int?>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static long? Sum<TKey>(this IEnumerable<KeyValuePair<TKey, long?>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static float? Sum<TKey>(this IEnumerable<KeyValuePair<TKey, float?>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static double? Sum<TKey>(this IEnumerable<KeyValuePair<TKey, double?>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        public static decimal? Sum<TKey>(this IEnumerable<KeyValuePair<TKey, decimal?>> dictionary)
        {
            return dictionary.Sum(kv => kv.Value);
        }

        #endregion

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

        public static void SafeForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictionary, Action<TKey, TValue> action)
        {
            dictionary.SafeForEach(kv => action(kv.Key, kv.Value));
        }

        public static int RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<TKey, TValue, bool> predicate)
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

        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }
    }
}