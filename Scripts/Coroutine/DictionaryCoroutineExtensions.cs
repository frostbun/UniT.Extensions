#if !UNIT_UNITASK
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class DictionaryCoroutineExtensions
    {
        private static readonly HashSet<object> Locks = new HashSet<object>();

        public static IEnumerator GetOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<Action<TValue>, IEnumerator> valueFactory, Action<TValue> callback)
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                callback(value);
                yield break;
            }
            yield return valueFactory(callback);
        }

        public static IEnumerator RemoveOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<Action<TValue>, IEnumerator> valueFactory, Action<TValue> callback)
        {
            if (dictionary.TryRemove(key, out var value))
            {
                callback(value);
                yield break;
            }
            yield return valueFactory(callback);
        }

        public static IEnumerator GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<Action<TValue>, IEnumerator> valueFactory, Action<TValue> callback)
        {
            yield return dictionary.TryAddAsync(key, valueFactory, _ => callback(dictionary[key]));
        }

        public static IEnumerator TryAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<Action<TValue>, IEnumerator> valueFactory, Action<bool> callback)
        {
            if (dictionary.ContainsKey(key))
            {
                callback(false);
                yield break;
            }
            var @lock = (dictionary, key);
            if (!Locks.Add(@lock))
            {
                yield return new WaitUntil(() => !Locks.Contains(@lock));
                yield return dictionary.TryAddAsync(key, valueFactory, callback);
                yield break;
            }
            try
            {
                yield return valueFactory(value =>
                {
                    if (!dictionary.TryAdd(key, value)) throw new InvalidOperationException("Dictionary was modified while trying to add a new value asynchronously");
                    callback(true);
                });
            }
            finally
            {
                Locks.Remove(@lock);
            }
        }
    }
}
#endif