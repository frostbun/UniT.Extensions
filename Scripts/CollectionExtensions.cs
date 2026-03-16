#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Random = UnityEngine.Random;

    public static class CollectionExtensions
    {
        public static void ShuffleInPlace<T>(this IList<T> list)
        {
            for (var i = 0; i < list.Count - 1; ++i)
            {
                var j = Random.Range(i, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetRealIndex<T>(this IList<T> list, Index index)
        {
            return index.IsFromEnd ? list.Count - index.Value : index.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetRange<T>(this IList<T> list, Index start, Index stop)
        {
            return list.GetRange(list.GetRealIndex(start), list.GetRealIndex(stop));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> GetRange<T>(this IList<T> list, Range range)
        {
            return list.GetRange(range.Start, range.End);
        }

        public static void RemoveRange<T>(this IList<T> list, int start, int stop)
        {
            while (stop-- > start) list.RemoveAt(stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this IList<T> list, Index start, Index stop)
        {
            list.RemoveRange(list.GetRealIndex(start), list.GetRealIndex(stop));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this IList<T> list, Range range)
        {
            list.RemoveRange(range.Start, range.End);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Remove(item);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this ICollection<T> collection, Action<T> action)
        {
            collection.ForEach(action);
            collection.Clear();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Stack<T> ToStack<T>(this IEnumerable<T> enumerable)
        {
            return new Stack<T>(enumerable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PeekOrDefault<T>(this Stack<T> stack)
        {
            return stack.Count > 0 ? stack.Peek() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekOrDefault<T>(this Stack<T> stack, T defaultValue)
        {
            return stack.Count > 0 ? stack.Peek() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekOrDefault<T>(this Stack<T> stack, Func<T> valueFactory)
        {
            return stack.Count > 0 ? stack.Peek() : valueFactory();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PopOrDefault<T>(this Stack<T> stack)
        {
            return stack.Count > 0 ? stack.Pop() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopOrDefault<T>(this Stack<T> stack, T defaultValue)
        {
            return stack.Count > 0 ? stack.Pop() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopOrDefault<T>(this Stack<T> stack, Func<T> valueFactory)
        {
            return stack.Count > 0 ? stack.Pop() : valueFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Stack<T> stack, Action<T> action)
        {
            stack.ForEach(action);
            stack.Clear();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
        {
            return new Queue<T>(enumerable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PeekOrDefault<T>(this Queue<T> queue)
        {
            return queue.Count > 0 ? queue.Peek() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekOrDefault<T>(this Queue<T> queue, T defaultValue)
        {
            return queue.Count > 0 ? queue.Peek() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekOrDefault<T>(this Queue<T> queue, Func<T> valueFactory)
        {
            return queue.Count > 0 ? queue.Peek() : valueFactory();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? DequeueOrDefault<T>(this Queue<T> queue)
        {
            return queue.Count > 0 ? queue.Dequeue() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DequeueOrDefault<T>(this Queue<T> queue, T defaultValue)
        {
            return queue.Count > 0 ? queue.Dequeue() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DequeueOrDefault<T>(this Queue<T> queue, Func<T> valueFactory)
        {
            return queue.Count > 0 ? queue.Dequeue() : valueFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Queue<T> queue, Action<T> action)
        {
            queue.ForEach(action);
            queue.Clear();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Deque<T> ToDeque<T>(this IEnumerable<T> enumerable)
        {
            return new Deque<T>(enumerable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PeekFrontOrDefault<T>(this Deque<T> deque)
        {
            return deque.Count > 0 ? deque.PeekFront() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekFrontOrDefault<T>(this Deque<T> deque, T defaultValue)
        {
            return deque.Count > 0 ? deque.PeekFront() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekFrontOrDefault<T>(this Deque<T> deque, Func<T> valueFactory)
        {
            return deque.Count > 0 ? deque.PeekFront() : valueFactory();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PeekBackOrDefault<T>(this Deque<T> deque)
        {
            return deque.Count > 0 ? deque.PeekBack() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekBackOrDefault<T>(this Deque<T> deque, T defaultValue)
        {
            return deque.Count > 0 ? deque.PeekBack() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PeekBackOrDefault<T>(this Deque<T> deque, Func<T> valueFactory)
        {
            return deque.Count > 0 ? deque.PeekBack() : valueFactory();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PopFrontOrDefault<T>(this Deque<T> deque)
        {
            return deque.Count > 0 ? deque.PopFront() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopFrontOrDefault<T>(this Deque<T> deque, T defaultValue)
        {
            return deque.Count > 0 ? deque.PopFront() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopFrontOrDefault<T>(this Deque<T> deque, Func<T> valueFactory)
        {
            return deque.Count > 0 ? deque.PopFront() : valueFactory();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? PopBackOrDefault<T>(this Deque<T> deque)
        {
            return deque.Count > 0 ? deque.PopBack() : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopBackOrDefault<T>(this Deque<T> deque, T defaultValue)
        {
            return deque.Count > 0 ? deque.PopBack() : defaultValue;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopBackOrDefault<T>(this Deque<T> deque, Func<T> valueFactory)
        {
            return deque.Count > 0 ? deque.PopBack() : valueFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear<T>(this Deque<T> deque, Action<T> action)
        {
            deque.ForEach(action);
            deque.Clear();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
            return Array.AsReadOnly(array);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray().AsReadOnly();
        }

        [Pure]
        public static T[,] To2DArray<T>(this T[][] source)
        {
            try
            {
                var dimension1 = source.Length;
                var dimension2 = source[0].Length;
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