#if ODIN_INSPECTOR
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private KeyValuePair[] values = Array.Empty<KeyValuePair>();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.values = this.Select(kv => new KeyValuePair(kv.Key, kv.Value)).ToArray();
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