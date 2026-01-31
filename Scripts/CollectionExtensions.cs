#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;

    public static class CollectionExtensions
    {
        [Pure]
        public static int GetRealIndex<T>(this IList<T> list, Index index)
        {
            return index.IsFromEnd ? list.Count - index.Value : index.Value;
        }

        public static void RemoveAt<T>(this IList<T> list, Index index)
        {
            list.RemoveAt(list.GetRealIndex(index));
        }

        public static void RemoveAtSwapBack<T>(this IList<T> list, Index index)
        {
            var realIndex = list.GetRealIndex(index);
            var lastIndex = list.Count - 1;
            list[realIndex] = list[lastIndex];
            list.RemoveAt(lastIndex);
        }

        [Pure]
        public static IEnumerable<T> GetRange<T>(this IList<T> list, int start, int stop)
        {
            for (var i = start; i < stop; ++i)
            {
                yield return list[i];
            }
        }

        [Pure]
        public static IEnumerable<T> GetRange<T>(this IList<T> list, Index start, Index stop)
        {
            return list.GetRange(list.GetRealIndex(start), list.GetRealIndex(stop));
        }

        [Pure]
        public static IEnumerable<T> GetRange<T>(this IList<T> list, Range range)
        {
            return list.GetRange(range.Start, range.End);
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

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            items.ForEach(collection.Add);
        }

        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            items.ForEach(item => collection.Remove(item));
        }

        public static void Clear<T>(this ICollection<T> collection, Action<T> action)
        {
            collection.ForEach(action);
            collection.Clear();
        }

        [Pure]
        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            return new Stack<T>(enumerable);
        }

        [Pure]
        public static T? PeekOrDefault<T>(this Stack<T> stack)
        {
            return stack.Count > 0 ? stack.Peek() : default;
        }

        [Pure]
        public static T PeekOrDefault<T>(this Stack<T> stack, T defaultValue)
        {
            return stack.Count > 0 ? stack.Peek() : defaultValue;
        }

        [Pure]
        public static T PeekOrDefault<T>(this Stack<T> stack, Func<T> valueFactory)
        {
            return stack.Count > 0 ? stack.Peek() : valueFactory();
        }

        [Pure]
        public static T? PopOrDefault<T>(this Stack<T> stack)
        {
            return stack.Count > 0 ? stack.Pop() : default;
        }

        [Pure]
        public static T PopOrDefault<T>(this Stack<T> stack, T defaultValue)
        {
            return stack.Count > 0 ? stack.Pop() : defaultValue;
        }

        [Pure]
        public static T PopOrDefault<T>(this Stack<T> stack, Func<T> valueFactory)
        {
            return stack.Count > 0 ? stack.Pop() : valueFactory();
        }

        public static void Clear<T>(this Stack<T> stack, Action<T> action)
        {
            stack.ForEach(action);
            stack.Clear();
        }

        [Pure]
        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return new Queue<T>(enumerable);
        }

        [Pure]
        public static T? PeekOrDefault<T>(this Queue<T> queue)
        {
            return queue.Count > 0 ? queue.Peek() : default;
        }

        [Pure]
        public static T PeekOrDefault<T>(this Queue<T> queue, T defaultValue)
        {
            return queue.Count > 0 ? queue.Peek() : defaultValue;
        }

        [Pure]
        public static T PeekOrDefault<T>(this Queue<T> queue, Func<T> valueFactory)
        {
            return queue.Count > 0 ? queue.Peek() : valueFactory();
        }

        [Pure]
        public static T? DequeueOrDefault<T>(this Queue<T> queue)
        {
            return queue.Count > 0 ? queue.Dequeue() : default;
        }

        [Pure]
        public static T DequeueOrDefault<T>(this Queue<T> queue, T defaultValue)
        {
            return queue.Count > 0 ? queue.Dequeue() : defaultValue;
        }

        [Pure]
        public static T DequeueOrDefault<T>(this Queue<T> queue, Func<T> valueFactory)
        {
            return queue.Count > 0 ? queue.Dequeue() : valueFactory();
        }

        public static void Clear<T>(this Queue<T> queue, Action<T> action)
        {
            queue.ForEach(action);
            queue.Clear();
        }

        [Pure]
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }

        [Pure]
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

        [Pure]
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