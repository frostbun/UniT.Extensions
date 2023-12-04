#if UNIT_EXTENSIONS_UNITASK
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

        public static UniTask<bool> TryAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            if (dictionary.ContainsKey(key)) return UniTask.FromResult(false);
            var @lock = (dictionary, key);
            if (Locks.Contains(@lock))
            {
                return UniTask.WaitUntil(() => !Locks.Contains(@lock))
                    .ContinueWith(() => dictionary.TryAddAsync(key, valueFactory));
            }
            Locks.Add(@lock);
            return valueFactory().ContinueWith(value =>
            {
                if (dictionary.ContainsKey(key))
                    throw new InvalidOperationException("Dictionary was modified while trying to add a new value asynchronously");
                dictionary.Add(key, value);
                return true;
            }).Finally(() => Locks.Remove(@lock));
        }
    }
}
#endif