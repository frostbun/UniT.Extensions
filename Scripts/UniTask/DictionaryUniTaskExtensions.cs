#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using Cysharp.Threading.Tasks;

    public static class DictionaryUniTaskExtensions
    {
        private static readonly HashSet<object> Locks = new HashSet<object>();

        public static UniTask<TValue> GetOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? UniTask.FromResult(value) : valueFactory();
        }

        public static UniTask<TValue> RemoveOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            return dictionary.Remove(key, out var value) ? UniTask.FromResult(value) : valueFactory();
        }

        public static UniTask<TValue> GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            return dictionary.TryAddAsync(key, valueFactory).ContinueWith(_ => dictionary[key]);
        }

        public static async UniTask<bool> TryAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            var @lock = (object)(dictionary, key);
            if (Locks.Contains(@lock)) await UniTask.WaitUntil((Locks, @lock), state => !state.Locks.Contains(state.@lock));
            if (dictionary.ContainsKey(key)) return false;
            Locks.Add(@lock);
            try
            {
                return dictionary.TryAdd(key, await valueFactory());
            }
            finally
            {
                Locks.Remove(@lock);
            }
        }
    }
}
#endif