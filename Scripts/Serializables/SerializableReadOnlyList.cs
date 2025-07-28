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
    public class SerializableReadOnlyList<T> : IReadOnlyList<T>
    {
        [SerializeReference] private List<T> values = new List<T>();

        public int Count => this.values.Count;

        public T this[int index] => this.values[index];

        IEnumerator IEnumerable.GetEnumerator() => this.values.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.values.GetEnumerator();
    }

    #if UNITY_EDITOR
    #if ODIN_INSPECTOR
    internal sealed class SerializableReadOnlyListDrawer<T> : OdinValueDrawer<SerializableReadOnlyList<T>>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children["values"].Draw(label);
        }
    }
    #else
    [CustomPropertyDrawer(typeof(SerializableReadOnlyList<>), useForChildren: true)]
    internal sealed class SerializableReadOnlyListDrawer : PropertyDrawer
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