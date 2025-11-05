#nullable enable
namespace UniT.Extensions
{
    using System;
    using UnityEngine;
    #if UNITY_EDITOR
    using System.Linq;
    using UnityEditor;
    #endif

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SortingLayerAttribute : PropertyAttribute
    {
    }

    #if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SortingLayerAttribute))]
    internal sealed class SortingLayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var currentIndex = Mathf.Max(0, SortingLayer.layers.SingleIndexOrDefault(layer => layer.id == property.intValue));
            var newIndex     = EditorGUI.Popup(position, label.text, currentIndex, SortingLayer.layers.Select(layer => layer.name).ToArray());
            property.intValue = SortingLayer.layers[newIndex].id;
        }
    }
    #endif
}