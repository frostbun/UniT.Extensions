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
    internal sealed class Serializable2DArrayDrawer<T> : OdinValueDrawer<Serializable2DArray<T>>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children[0].Draw(label);
        }
    }
    #else
    [CustomPropertyDrawer(typeof(Serializable2DArray<>), useForChildren: true)]
    internal sealed class Serializable2DArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("columns"), label, includeChildren: true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("columns"), label, includeChildren: true);
        }
    }
    #endif
}