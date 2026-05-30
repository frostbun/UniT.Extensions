#nullable enable
namespace UniT.Extensions.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(LayerAttribute))]
    internal sealed class LayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (LayerMask.LayerToName(property.intValue).IsNullOrWhiteSpace())
            {
                property.intValue = 0;
            }
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}