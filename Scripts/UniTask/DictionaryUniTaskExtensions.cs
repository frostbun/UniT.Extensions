#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Cysharp.Threading.Tasks;

    public static class DictionaryUniTaskExtensions
    {
        private static readonly HashSet<object> Locks = new HashSet<object>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<TValue> GetOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? UniTask.FromResult(value) : valueFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<TValue> GetOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, UniTask<TValue>> valueFactory)
        {
            return dictionary.TryGetValue(key, out var value) ? UniTask.FromResult(value) : valueFactory(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<TValue> GetOrDefaultAsync<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, UniTask<TValue>> valueFactory, TState state)
        {
            return dictionary.TryGetValue(key, out var value) ? UniTask.FromResult(value) : valueFactory(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<TValue> RemoveOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            return dictionary.Remove(key, out var value) ? UniTask.FromResult(value) : valueFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<TValue> RemoveOrDefaultAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, UniTask<TValue>> valueFactory)
        {
            return dictionary.Remove(key, out var value) ? UniTask.FromResult(value) : valueFactory(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask<TValue> RemoveOrDefaultAsync<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, UniTask<TValue>> valueFactory, TState state)
        {
            return dictionary.Remove(key, out var value) ? UniTask.FromResult(value) : valueFactory(state);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<TValue> GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            await dictionary.TryAddAsync(key, valueFactory);
            return dictionary[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<TValue> GetOrAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, UniTask<TValue>> valueFactory)
        {
            await dictionary.TryAddAsync(key, valueFactory);
            return dictionary[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<TValue> GetOrAddAsync<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, UniTask<TValue>> valueFactory, TState state)
        {
            await dictionary.TryAddAsync(key, valueFactory, state);
            return dictionary[key];
        }

        public static async UniTask<bool> TryAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<UniTask<TValue>> valueFactory)
        {
            var @lock = (object)(dictionary, key);
            if (Locks.Contains(@lock)) await UniTask.WaitUntil(@lock, static @lock => !Locks.Contains(@lock));
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

        public static async UniTask<bool> TryAddAsync<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, UniTask<TValue>> valueFactory)
        {
            var @lock = (object)(dictionary, key);
            if (Locks.Contains(@lock)) await UniTask.WaitUntil(@lock, static @lock => !Locks.Contains(@lock));
            if (dictionary.ContainsKey(key)) return false;
            Locks.Add(@lock);
            try
            {
                return dictionary.TryAdd(key, await valueFactory(key));
            }
            finally
            {
                Locks.Remove(@lock);
            }
        }

        public static async UniTask<bool> TryAddAsync<TKey, TValue, TState>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TState, UniTask<TValue>> valueFactory, TState state)
        {
            var @lock = (object)(dictionary, key);
            if (Locks.Contains(@lock)) await UniTask.WaitUntil(@lock, static @lock => !Locks.Contains(@lock));
            if (dictionary.ContainsKey(key)) return false;
            Locks.Add(@lock);
            try
            {
                return dictionary.TryAdd(key, await valueFactory(state));
            }
            finally
            {
                Locks.Remove(@lock);
            }
        }
    }
}
#endif