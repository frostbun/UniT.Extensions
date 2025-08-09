#if ODIN_INSPECTOR
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    #if UNITY_EDITOR
    using Sirenix.OdinInspector.Editor;
    #endif

    [Serializable]
    public class SerializableReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        [SerializeField] private SerializableDictionary<TKey, TValue> values;

        public SerializableReadOnlyDictionary() : this(new SerializableDictionary<TKey, TValue>())
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

        IEnumerator IEnumerable.GetEnumerator() => this.values.GetEnumerator();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => this.values.GetEnumerator();
    }

    #if UNITY_EDITOR
    internal sealed class SerializableReadOnlyDictionaryDrawer<TKey, TValue> : OdinValueDrawer<SerializableReadOnlyDictionary<TKey, TValue>>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children[0].Draw(label);
        }
    }
    #endif
}
#endif