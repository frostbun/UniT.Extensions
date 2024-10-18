#if !UNIT_UNITASK
#nullable enable
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
            if (dictionary.Remove(key, out var value))
            {
                callback(value);
                yield break;
            }
            yield return valueFactory(callback);
        }

        public static IEnumerator GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<Action<TValue>, IEnumerator> valueFactory, Action<TValue> callback)
        {
            return dictionary.TryAddAsync(key, valueFactory, _ => callback(dictionary[key]));
        }

        public static IEnumerator TryAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<Action<TValue>, IEnumerator> valueFactory, Action<bool>? callback = null)
        {
            var @lock = (dictionary, key);
            yield return new WaitUntil(() => !Locks.Contains(@lock));
            if (dictionary.ContainsKey(key))
            {
                callback?.Invoke(false);
                yield break;
            }
            Locks.Add(@lock);
            try
            {
                yield return valueFactory(value => callback?.Invoke(dictionary.TryAdd(key, value)));
            }
            finally
            {
                Locks.Remove(@lock);
            }
        }
    }
}
#endif