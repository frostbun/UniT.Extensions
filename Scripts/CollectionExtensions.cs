namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static void RemoveAt<T>(this IList<T> list, Index index)
        {
            list.RemoveAt(index.IsFromEnd ? list.Count - index.Value : index.Value);
        }

        public static void Clear<T>(this ICollection<T> collection, Action<T> action)
        {
            collection.ForEach(action);
            collection.Clear();
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList().AsReadOnly();
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Aggregate(new Stack<T>(), (stack, item) =>
            {
                stack.Push(item);
                return stack;
            });
        }

        public static T PeekOrDefault<T>(this Stack<T> stack, Func<T> valueFactory = null)
        {
            return stack.Count > 0
                ? stack.Peek()
                : (valueFactory ?? (() => default))();
        }

        public static T PopOrDefault<T>(this Stack<T> stack, Func<T> valueFactory = null)
        {
            return stack.Count > 0
                ? stack.Pop()
                : (valueFactory ?? (() => default))();
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Aggregate(new Queue<T>(), (queue, item) =>
            {
                queue.Enqueue(item);
                return queue;
            });
        }

        public static T PeekOrDefault<T>(this Queue<T> queue, Func<T> valueFactory = null)
        {
            return queue.Count > 0
                ? queue.Peek()
                : (valueFactory ?? (() => default))();
        }

        public static T DequeueOrDefault<T>(this Queue<T> queue, Func<T> valueFactory = null)
        {
            return queue.Count > 0
                ? queue.Dequeue()
                : (valueFactory ?? (() => default))();
        }

        public static T[,] To2DArray<T>(this T[][] source)
        {
            try
            {
                var dimension1 = source.Length;
                var dimension2 = source.GroupBy(row => row.Length).Single().Key;
                var result     = new T[dimension1, dimension2];
                for (var i = 0; i < dimension1; ++i)
                {
                    for (var j = 0; j < dimension2; ++j)
                    {
                        result[i, j] = source[i][j];
                    }
                }
                return result;
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException("The given jagged array is not rectangular");
            }
        }
    }
}