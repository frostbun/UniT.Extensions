#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Serializable2DArray<TItem> : IEnumerable<TItem>, ISerializationCallbackReceiver
    {
        [SerializeField] private Column[] columns;

        public Serializable2DArray() : this(0, 0)
        {
        }

        public Serializable2DArray(int width, int height)
        {
            this.columns = new Column[width];
            for (var i = 0; i < width; ++i) this.columns[i] = new(height);
        }

        public int Width => this.columns.Length;

        public int Height => this.columns.Length > 0 ? this.columns[0].Height : 0;

        public TItem this[int x, int y] { get => this.columns[x].Cells[y]; set => this.columns[x].Cells[y] = value; }

        public IEnumerable<TItem> GetColumn(int x) => this.columns[x].Cells;

        public IEnumerable<TItem> GetRow(int y) => this.columns.Select((column, y) => column.Cells[y], y);

        public IEnumerator<TItem> GetEnumerator()
        {
            for (var y = 0; y < this.Height; ++y)
            {
                for (var x = 0; x < this.Width; ++x)
                {
                    yield return this.columns[x].Cells[y];
                }
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            for (var x = 0; x < this.Width; ++x)
            {
                if (this.columns[x].Height == this.Height) continue;
                var oldColumn = this.columns[x];
                this.columns[x] = new(this.Height);
                var height = Mathf.Min(oldColumn.Height, this.Height);
                for (var y = 0; y < height; ++y)
                {
                    this.columns[x].Cells[y] = oldColumn.Cells[y];
                }
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        [Serializable]
        public struct Column
        {
            [field: SerializeReference] public TItem[] Cells { get; private set; }

            public readonly int Height => this.Cells.Length;

            public Column(int height)
            {
                this.Cells = new TItem[height];
            }
        }
    }
}