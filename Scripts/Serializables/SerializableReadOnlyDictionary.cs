#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class SerializableReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue> where TKey : notnull
    {
        [SerializeField] private SerializableDictionary<TKey, TValue> values;

        public SerializableReadOnlyDictionary() : this(new())
        {
        }

        public SerializableReadOnlyDictionary(SerializableDictionary<TKey, TValue> values)
        {
            this.values = values;
        }

        public int Count => this.values.Count;

        public TValue this[TKey key] => this.values[key];

        public IEnumerable<TKey> Keys => this.values.Keys;

        public IEnumerable<TValue> Values => this.values.Values;

        public bool ContainsKey(TKey key) => this.values.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => this.values.TryGetValue(key, out value);

        public Dictionary<TKey, TValue>.Enumerator GetEnumerator() => this.values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.values.GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => this.values.GetEnumerator();
    }
}