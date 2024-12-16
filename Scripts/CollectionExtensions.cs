#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class CollectionExtensions
    {
        public static int GetRealIndex<T>(this IList<T> list, Index index)
        {
            return index.IsFromEnd ? list.Count - index.Value : index.Value;
        }

        public static void RemoveAt<T>(this IList<T> list, Index index)
        {
            list.RemoveAt(list.GetRealIndex(index));
        }

        public static void RemoveRange<T>(this IList<T> list, int start, int stop)
        {
            while (stop-- > start) list.RemoveAt(stop);
        }

        public static void RemoveRange<T>(this IList<T> list, Index start, Index stop)
        {
            list.RemoveRange(list.GetRealIndex(start), list.GetRealIndex(stop));
        }

        public static void RemoveRange<T>(this IList<T> list, Range range)
        {
            list.RemoveRange(range.Start, range.End);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            enumerable.ForEach(collection.Add);
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            enumerable.ForEach(item => collection.Remove(item));
        }

        public static void Clear<T>(this ICollection<T> collection, Action<T> action)
        {
            collection.ForEach(action);
            collection.Clear();
        }

        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            return new Stack<T>(enumerable);
        }

        public static T? PeekOrDefault<T>(this Stack<T> stack)
        {
            return stack.Count > 0 ? stack.Peek() : default;
        }

        public static T PeekOrDefault<T>(this Stack<T> stack, T defaultValue)
        {
            return stack.Count > 0 ? stack.Peek() : defaultValue;
        }

        public static T PeekOrDefault<T>(this Stack<T> stack, Func<T> valueFactory)
        {
            return stack.Count > 0 ? stack.Peek() : valueFactory();
        }

        public static T? PopOrDefault<T>(this Stack<T> stack)
        {
            return stack.Count > 0 ? stack.Pop() : default;
        }

        public static T PopOrDefault<T>(this Stack<T> stack, T defaultValue)
        {
            return stack.Count > 0 ? stack.Pop() : defaultValue;
        }

        public static T PopOrDefault<T>(this Stack<T> stack, Func<T> valueFactory)
        {
            return stack.Count > 0 ? stack.Pop() : valueFactory();
        }

        public static void Clear<T>(this Stack<T> stack, Action<T> action)
        {
            stack.ForEach(action);
            stack.Clear();
        }

        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return new Queue<T>(enumerable);
        }

        public static T? PeekOrDefault<T>(this Queue<T> queue)
        {
            return queue.Count > 0 ? queue.Peek() : default;
        }

        public static T PeekOrDefault<T>(this Queue<T> queue, T defaultValue)
        {
            return queue.Count > 0 ? queue.Peek() : defaultValue;
        }

        public static T PeekOrDefault<T>(this Queue<T> queue, Func<T> valueFactory)
        {
            return queue.Count > 0 ? queue.Peek() : valueFactory();
        }

        public static T? DequeueOrDefault<T>(this Queue<T> queue)
        {
            return queue.Count > 0 ? queue.Dequeue() : default;
        }

        public static T DequeueOrDefault<T>(this Queue<T> queue, T defaultValue)
        {
            return queue.Count > 0 ? queue.Dequeue() : defaultValue;
        }

        public static T DequeueOrDefault<T>(this Queue<T> queue, Func<T> valueFactory)
        {
            return queue.Count > 0 ? queue.Dequeue() : valueFactory();
        }

        public static void Clear<T>(this Queue<T> queue, Action<T> action)
        {
            queue.ForEach(action);
            queue.Clear();
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
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

        public static T[][] ToJaggedArray<T>(this T[,] source)
        {
            var dimension1 = source.GetLength(0);
            var dimension2 = source.GetLength(1);
            var result     = new T[dimension1][];
            for (var i = 0; i < dimension1; ++i)
            {
                result[i] = new T[dimension2];
                for (var j = 0; j < dimension2; ++j)
                {
                    result[i][j] = source[i, j];
                }
            }
            return result;
        }
    }
}