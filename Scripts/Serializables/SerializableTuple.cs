#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    #if UNITY_EDITOR
    using UnityEditor;
    #if ODIN_INSPECTOR
    using System.Linq;
    using Sirenix.Utilities.Editor;
    using Sirenix.OdinInspector.Editor;
    #endif
    #endif

    [Serializable]
    public class SerializableTuple<T1, T2> : ITuple
    {
        [field: SerializeReference] public T1 Item1 { get; private set; }
        [field: SerializeReference] public T2 Item2 { get; private set; }

        public SerializableTuple() : this(default!, default!)
        {
        }

        public SerializableTuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public void Deconstruct(out T1 x, out T2 y)
        {
            x = this.Item1;
            y = this.Item2;
        }

        int ITuple.Length => 2;

        object? ITuple.this[int index] => index switch
        {
            0 => this.Item1,
            1 => this.Item2,
            _ => throw new IndexOutOfRangeException(),
        };
    }

    [Serializable]
    public class SerializableTuple<T1, T2, T3> : ITuple
    {
        [field: SerializeReference] public T1 Item1 { get; private set; }
        [field: SerializeReference] public T2 Item2 { get; private set; }
        [field: SerializeReference] public T3 Item3 { get; private set; }

        public SerializableTuple(T1 item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public void Deconstruct(out T1 x, out T2 y, out T3 z)
        {
            x = this.Item1;
            y = this.Item2;
            z = this.Item3;
        }

        int ITuple.Length => 3;

        object? ITuple.this[int index] => index switch
        {
            0 => this.Item1,
            1 => this.Item2,
            2 => this.Item3,
            _ => throw new IndexOutOfRangeException(),
        };
    }

    [Serializable]
    public class SerializableTuple<T1, T2, T3, T4> : ITuple
    {
        [field: SerializeReference] public T1 Item1 { get; private set; }
        [field: SerializeReference] public T2 Item2 { get; private set; }
        [field: SerializeReference] public T3 Item3 { get; private set; }
        [field: SerializeReference] public T4 Item4 { get; private set; }

        public SerializableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }

        public void Deconstruct(out T1 x, out T2 y, out T3 z, out T4 w)
        {
            x = this.Item1;
            y = this.Item2;
            z = this.Item3;
            w = this.Item4;
        }

        int ITuple.Length => 4;

        object? ITuple.this[int index] => index switch
        {
            0 => this.Item1,
            1 => this.Item2,
            2 => this.Item3,
            3 => this.Item4,
            _ => throw new IndexOutOfRangeException(),
        };
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TupleDisplayNamesAttribute : PropertyAttribute
    {
        public IReadOnlyList<string> Names { get; }

        public TupleDisplayNamesAttribute(params string[] names) : base(applyToCollection: true)
        {
            this.Names = names;
        }
    }

    #if UNITY_EDITOR
    #if ODIN_INSPECTOR
    internal sealed class TupleDrawer : OdinValueDrawer<ITuple>
    {
        protected override void DrawPropertyLayout(GUIContent? label)
        {
            var displayNames = this.Property.GetAttribute<TupleDisplayNamesAttribute>();
            SirenixEditorGUI.BeginHorizontalPropertyLayout(label);
            foreach (var (property, name) in IterTools.ZipLongest(this.Property.Children, displayNames?.Names ?? Enumerable.Empty<string>()))
            {
                if (property is null) break;
                var childLabel = new GUIContent(name ?? property.NiceName);
                GUIHelper.PushLabelWidth(EditorStyles.label.CalcWidth(childLabel));
                property.Draw(childLabel);
                GUIHelper.PopLabelWidth();
            }
            SirenixEditorGUI.EndHorizontalPropertyLayout();
        }
    }
    #else
    [CustomPropertyDrawer(typeof(SerializableTuple<,>), useForChildren: true)]
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

            position.width = label is { } && !label.text.IsNullOrWhiteSpace() ? EditorGUIUtility.labelWidth : 0;
            EditorGUI.LabelField(position, label);
            position.x += position.width;

            position.width = (fullWidth - position.width - (tuple.Length - 1) * SPACING) / tuple.Length;
            foreach (var index in Ranges.Take(tuple.Length))
            {
                var name = $"Item{index + 1}";
                var childLabel = new GUIContent(
                    displayNames is { } && displayNames.Names.Count > index
                        ? displayNames.Names[index]
                        : name
                );
                EditorStyles.label.CalcMinMaxWidth(childLabel, out var width, out _);
                EditorGUIUtility.labelWidth = width;
                EditorGUI.PropertyField(
                    position,
                    property.FindPropertyRelative(name.ToBackingFieldName()),
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
    #endif
    #endif
}