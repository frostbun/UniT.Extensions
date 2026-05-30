#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEditor;
    using UnityEngine;

    public abstract class NestedPropertyDrawer : PropertyDrawer
    {
        protected abstract string PropertyName { get; }

        public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative(this.PropertyName), label, includeChildren: true);
        }

        public sealed override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative(this.PropertyName), label, includeChildren: true);
        }
    }
}