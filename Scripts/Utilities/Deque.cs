#nullable enable
namespace UniT.Extensions
{
    using System.Collections.Generic;

    public class Deque<T>
    {
        private readonly LinkedList<T> items = new LinkedList<T>();

        public int Count => this.items.Count;

        public void PushFront(T item)
        {
            this.items.AddFirst(item);
        }

        public void PushBack(T item)
        {
            this.items.AddLast(item);
        }

        public T PopFront()
        {
            var item = this.PeekFront();
            this.items.RemoveFirst();
            return item;
        }

        public T PopBack()
        {
            var item = this.PeekBack();
            this.items.RemoveLast();
            return item;
        }

        public T PeekFront()
        {
            return this.items.First.Value;
        }

        public T PeekBack()
        {
            return this.items.Last.Value;
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}