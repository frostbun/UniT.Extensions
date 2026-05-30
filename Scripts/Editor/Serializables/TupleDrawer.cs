#nullable enable
namespace UniT.Extensions.Editor
{
    using System.Runtime.CompilerServices;
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(ITuple), useForChildren: true)]
    [CustomPropertyDrawer(typeof(TupleDisplayNamesAttribute), useForChildren: true)]
    internal sealed class SerializableTupleCustomDisplayNameDrawer : PropertyDrawer
    {
        private const float SPACING = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent? label)
        {
            var tuple        = (ITuple)property.boxedValue;
            var displayNames = this.attribute as TupleDisplayNamesAttribute;

            var originalLabelWidth = EditorGUIUtility.labelWidth;
            var fullWidth          = position.width;
            GUILayout.BeginHorizontal();

            position.width = label is not null && !label.text.IsNullOrWhiteSpace() ? EditorGUIUtility.labelWidth : 0;
            EditorGUI.LabelField(position, label);
            position.x += position.width;

            position.width = (fullWidth - position.width - (tuple.Length - 1) * SPACING) / tuple.Length;
            for (var index = 0; index < tuple.Length; ++index)
            {
                var childLabel = new GUIContent(
                    displayNames is not null && displayNames.Names.Count > index
                        ? displayNames.Names[index]
                        : $"Item {index + 1}"
                );
                EditorStyles.label.CalcMinMaxWidth(childLabel, out var width, out _);
                EditorGUIUtility.labelWidth = width;
                EditorGUI.PropertyField(
                    position,
                    property.FindPropertyRelative($"Item{index + 1}".ToBackingFieldName()),
                    childLabel,
                    includeChildren: true
                );
                position.x += position.width + SPACING;
            }

            EditorGUIUtility.labelWidth = originalLabelWidth;
            GUILayout.EndHorizontal();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent? label)
        {
            return EditorGUIUtility.singleLineHeight;
        }
    }
}