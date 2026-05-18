#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEngine;
    #if ODIN_INSPECTOR
    using Sirenix.OdinInspector.Editor;
    #else
    using UnityEditor;
    #endif

    #if ODIN_INSPECTOR
    internal sealed class SerializableReadOnlyDictionaryDrawer<TKey, TValue> : OdinValueDrawer<SerializableReadOnlyDictionary<TKey, TValue>>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children[0].Draw(label);
        }
    }
    #else
    [CustomPropertyDrawer(typeof(SerializableReadOnlyDictionary<,>), useForChildren: true)]
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
}