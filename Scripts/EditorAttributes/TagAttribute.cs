#nullable enable
namespace UniT.Extensions
{
    using System;
    using UnityEngine;
    #if UNITY_EDITOR
    using System.Linq;
    using UnityEditor;
    using UnityEditorInternal;
    #endif

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TagAttribute : PropertyAttribute
    {
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TagAttribute))]
    internal sealed class TagAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var tags = InternalEditorUtility.tags;
            if (!tags.Contains(property.stringValue))
            {
                property.stringValue = tags[0];
            }
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
        }
    }
    #endif
}