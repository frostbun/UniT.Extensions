namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class CollectionExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Aggregate(new Stack<T>(), (stack, item) =>
            {
                stack.Push(item);
                return stack;
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekOrDefault<T>(this Stack<T> stack, Func<T> valueFactory = null)
        {
            return stack.Count > 0 ? stack.Peek() : (valueFactory ?? (() => default))();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopOrDefault<T>(this Stack<T> stack, Func<T> valueFactory = null)
        {
            return stack.Count > 0 ? stack.Pop() : (valueFactory ?? (() => default))();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Aggregate(new Queue<T>(), (queue, item) =>
            {
                queue.Enqueue(item);
                return queue;
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekOrDefault<T>(this Queue<T> queue, Func<T> valueFactory = null)
        {
            return queue.Count > 0 ? queue.Peek() : (valueFactory ?? (() => default))();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DequeueOrDefault<T>(this Queue<T> queue, Func<T> valueFactory = null)
        {
            return queue.Count > 0 ? queue.Dequeue() : (valueFactory ?? (() => default))();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList().AsReadOnly();
        }
    }
}