#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    #if UNITY_EDITOR
    #if ODIN_INSPECTOR
    using Sirenix.OdinInspector.Editor;
    #else
    using UnityEditor;
    #endif
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
    #if ODIN_INSPECTOR
    internal sealed class SerializableReadOnlyDictionaryDrawer<TKey, TValue> : OdinValueDrawer<SerializableReadOnlyDictionary<TKey, TValue>>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children[0].Draw(label);
        }
    }
    #else
    [CustomPropertyDrawer(typeof(SerializableReadOnlyDictionary<>), useForChildren: true)]
    internal sealed class SerializableReadOnlyDictionaryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("values"), label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("values"), label, includeChildren: true);
        }
    }
    #endif
    #endif
}