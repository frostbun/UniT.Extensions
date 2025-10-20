#nullable enable
namespace UniT.Extensions
{
    using System;
    using UnityEngine;
    #if UNITY_EDITOR
    using UnityEditor;
    #endif

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class LayerAttribute : PropertyAttribute
    {
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    internal sealed class LayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
    #endif
}