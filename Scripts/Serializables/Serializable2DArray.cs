#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    #if UNITY_EDITOR
    #if ODIN_INSPECTOR
    using Sirenix.OdinInspector.Editor;
    #else
    using UnityEditor;
    #endif
    #endif

    [Serializable]
    public class Serializable2DArray<TItem> : IEnumerable<TItem>, ISerializationCallbackReceiver
    {
        [SerializeField] private Column[] columns;

        public Serializable2DArray() : this(0, 0)
        {
        }

        public Serializable2DArray(int width, int height)
        {
            this.columns = IterTools.Repeat(() => new Column(height), width).ToArray();
        }

        public int Width => this.columns.Length;

        public int Height => this.columns.Length > 0 ? this.columns[0].Height : 0;

        public TItem this[int x, int y] { get => this.columns[x].Cells[y]; set => this.columns[x].Cells[y] = value; }

        public IEnumerable<TItem> GetColumn(int x) => this.columns[x].Cells;

        public IEnumerable<TItem> GetRow(int y) => this.columns.Select(column => column.Cells[y]);

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            foreach (var x in Ranges.Take(this.Width))
            {
                if (this.columns[x].Height == this.Height) continue;
                var oldColumn = this.columns[x];
                this.columns[x] = new Column(this.Height);
                foreach (var y in Ranges.Take(Mathf.Min(oldColumn.Height, this.Height)))
                {
                    this.columns[x].Cells[y] = oldColumn.Cells[y];
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerator<TItem> GetEnumerator()
        {
            foreach (var y in Ranges.Take(this.Height))
            {
                foreach (var x in Ranges.Take(this.Width))
                {
                    yield return this.columns[x].Cells[y];
                }
            }
        }

        [Serializable]
        private struct Column
        {
            [field: SerializeReference] public TItem[] Cells { get; private set; }

            public int Height => this.Cells.Length;

            public Column(int height)
            {
                this.Cells = new TItem[height];
            }
        }
    }

    #if UNITY_EDITOR
    #if ODIN_INSPECTOR
    internal sealed class Serializable2DArrayDrawer<T> : OdinValueDrawer<Serializable2DArray<T>>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            this.Property.Children["columns"].Draw(label);
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
    #endif
}