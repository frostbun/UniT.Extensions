#nullable enable
namespace UniT.Extensions.Editor
{
    using System.Linq;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

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
}