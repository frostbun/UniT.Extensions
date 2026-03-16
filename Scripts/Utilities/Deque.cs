#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Deque<T> : ICollection<T>, IReadOnlyCollection<T>
    {
        private T[] items;
        private int head;
        private int tail;
        private int count;

        public Deque() : this(4) { }

        public Deque(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            this.items = new T[capacity > 0 ? capacity : 4];
        }

        public Deque(IEnumerable<T> items)
        {
            if (items is ICollection<T> collection)
            {
                this.items = new T[collection.Count > 0 ? collection.Count : 4];
                collection.CopyTo(this.items, 0);
                this.count = collection.Count;
                this.tail  = collection.Count;
            }
            else
            {
                this.items = items.ToArray();
                this.count = this.items.Length;
                this.tail  = this.count;
            }
        }

        public int Count => this.count;

        private int Capacity => this.items.Length;

        private int Next(int index) => index + 1 >= this.Capacity ? 0 : index + 1;

        private int Previous(int index) => index - 1 < 0 ? this.Capacity - 1 : index - 1;

        private void EnsureCapacity()
        {
            if (this.count < this.Capacity) return;

            var newCapacity = this.Capacity * 2;
            var newItems    = new T[newCapacity];

            if (this.head < this.tail)
            {
                Array.Copy(this.items, this.head, newItems, 0, this.count);
            }
            else
            {
                var headLength = this.Capacity - this.head;
                Array.Copy(this.items, this.head, newItems, 0, headLength);
                Array.Copy(this.items, 0, newItems, headLength, this.tail);
            }

            this.items = newItems;
            this.head  = 0;
            this.tail  = this.count;
        }

        public void PushFront(T item)
        {
            this.EnsureCapacity();
            this.head             = this.Previous(this.head);
            this.items[this.head] = item;
            ++this.count;
        }

        public void PushBack(T item)
        {
            this.EnsureCapacity();
            this.items[this.tail] = item;
            this.tail             = this.Next(this.tail);
            ++this.count;
        }

        public T PopFront()
        {
            var item = this.PeekFront();
            this.items[this.head] = default!;
            this.head             = this.Next(this.head);
            --this.count;
            return item;
        }

        public T PopBack()
        {
            var item = this.PeekBack();
            this.tail             = this.Previous(this.tail);
            this.items[this.tail] = default!;
            --this.count;
            return item;
        }

        public T PeekFront()
        {
            if (this.count is 0) throw new InvalidOperationException("Deque is empty");
            return this.items[this.head];
        }

        public T PeekBack()
        {
            if (this.count is 0) throw new InvalidOperationException("Deque is empty");
            return this.items[this.Previous(this.tail)];
        }

        public void Clear()
        {
            if (this.head < this.tail)
            {
                Array.Clear(this.items, this.head, this.count);
            }
            else
            {
                Array.Clear(this.items, this.head, this.Capacity - this.head);
                Array.Clear(this.items, 0, this.tail);
            }

            this.head  = 0;
            this.tail  = 0;
            this.count = 0;
        }

        public bool Contains(T item)
        {
            if (this.head < this.tail)
            {
                for (var i = this.head; i < this.tail; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(this.items[i], item)) return true;
                }
            }
            else
            {
                for (var i = this.head; i < this.Capacity; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(this.items[i], item)) return true;
                }
                for (var i = 0; i < this.tail; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(this.items[i], item)) return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.head < this.tail)
            {
                for (var i = this.head; i < this.tail; i++) yield return this.items[i];
            }
            else
            {
                for (var i = this.head; i < this.Capacity; i++) yield return this.items[i];
                for (var i = 0; i < this.tail; i++) yield return this.items[i];
            }
        }

        bool ICollection<T>.IsReadOnly => false;

        void ICollection<T>.Add(T item) => this.PushBack(item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException(nameof(arrayIndex));
            if (this.count > array.Length - arrayIndex) throw new ArgumentException("Destination array is not long enough");

            if (this.head < this.tail)
            {
                Array.Copy(this.items, this.head, array, arrayIndex, this.count);
            }
            else
            {
                var headLength = this.Capacity - this.head;
                Array.Copy(this.items, this.head, array, arrayIndex, headLength);
                Array.Copy(this.items, 0, array, arrayIndex + headLength, this.tail);
            }
        }

        bool ICollection<T>.Remove(T item) => throw new NotSupportedException("Deque does not support arbitrary removal. Use PopFront or PopBack.");

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}