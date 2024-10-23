#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;

    public class PriorityQueue<TItem, TPriority>
    {
        private readonly SortedList<TPriority, TItem> items;

        public PriorityQueue() : this(Comparer<TPriority>.Default)
        {
        }

        public PriorityQueue(Comparison<TPriority> comparison) : this(Comparer<TPriority>.Create(comparison))
        {
        }

        public PriorityQueue(IComparer<TPriority> comparer)
        {
            this.items = new SortedList<TPriority, TItem>(Comparer<TPriority>.Create((i1, i2) =>
            {
                var result = comparer.Compare(i1, i2);
                return result is 0 ? 1 : result;
            }));
        }

        public int Count => this.items.Count;

        public void Enqueue(TItem element, TPriority priority)
        {
            this.items.Add(priority, element);
        }

        public TItem Dequeue()
        {
            var result = this.Peek();
            this.items.RemoveAt(this.items.Count - 1);
            return result;
        }

        public TItem Peek()
        {
            return this.items.Values[this.items.Count - 1];
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}