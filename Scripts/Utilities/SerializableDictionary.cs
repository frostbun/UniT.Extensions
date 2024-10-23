#if ODIN_INSPECTOR
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<KeyValuePair> values = new List<KeyValuePair>();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.values.Clear();
            foreach (var kv in this)
            {
                this.values.Add(new KeyValuePair(kv.Key, kv.Value));
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();
            foreach (var kv in this.values)
            {
                this.Add(kv.Key, kv.Value);
            }
        }

        [Serializable]
        private struct KeyValuePair
        {
            [field: SerializeField] public TKey   Key   { get; private set; }
            [field: SerializeField] public TValue Value { get; private set; }

            public KeyValuePair(TKey key, TValue value)
            {
                this.Key   = key;
                this.Value = value;
            }
        }
    }
}
#endif