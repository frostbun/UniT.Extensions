#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
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
}