#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class IterTools
    {
        [Pure]
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            using var e1         = first.GetEnumerator();
            using var e2         = second.GetEnumerator();
            var       e1HasValue = e1.MoveNext();
            var       e2HasValue = e2.MoveNext();
            while (e1HasValue && e2HasValue)
            {
                yield return resultSelector(e1.Current, e2.Current);
                e1HasValue = e1.MoveNext();
                e2HasValue = e2.MoveNext();
            }
            if (e1HasValue || e2HasValue) throw new InvalidOperationException("The number of items are different. If this is intentional, use ZipShortest or ZipLongest instead.");
        }

        [Pure]
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            using var e1         = first.GetEnumerator();
            using var e2         = second.GetEnumerator();
            using var e3         = third.GetEnumerator();
            var       e1HasValue = e1.MoveNext();
            var       e2HasValue = e2.MoveNext();
            var       e3HasValue = e3.MoveNext();
            while (e1HasValue && e2HasValue && e3HasValue)
            {
                yield return resultSelector(e1.Current, e2.Current, e3.Current);
                e1HasValue = e1.MoveNext();
                e2HasValue = e2.MoveNext();
                e3HasValue = e3.MoveNext();
            }
            if (e1HasValue || e2HasValue || e3HasValue) throw new InvalidOperationException("The number of items are different. If this is intentional, use ZipShortest or ZipLongest instead.");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return Zip(first, second, (i1, i2) => (i1, i2));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst, TSecond, TThird)> Zip<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return Zip(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

        [Pure]
        public static IEnumerable<TResult> ZipShortest<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            using var e1         = first.GetEnumerator();
            using var e2         = second.GetEnumerator();
            var       e1HasValue = e1.MoveNext();
            var       e2HasValue = e2.MoveNext();
            while (e1HasValue && e2HasValue)
            {
                yield return resultSelector(e1.Current, e2.Current);
                e1HasValue = e1.MoveNext();
                e2HasValue = e2.MoveNext();
            }
        }

        [Pure]
        public static IEnumerable<TResult> ZipShortest<TFirst, TSecond, TThird, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            using var e1         = first.GetEnumerator();
            using var e2         = second.GetEnumerator();
            using var e3         = third.GetEnumerator();
            var       e1HasValue = e1.MoveNext();
            var       e2HasValue = e2.MoveNext();
            var       e3HasValue = e3.MoveNext();
            while (e1HasValue && e2HasValue && e3HasValue)
            {
                yield return resultSelector(e1.Current, e2.Current, e3.Current);
                e1HasValue = e1.MoveNext();
                e2HasValue = e2.MoveNext();
                e3HasValue = e3.MoveNext();
            }
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst, TSecond)> ZipShortest<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return ZipShortest(first, second, (i1, i2) => (i1, i2));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst, TSecond, TThird)> ZipShortest<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return ZipShortest(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

        [Pure]
        public static IEnumerable<TResult> ZipLongest<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst?, TSecond?, TResult> resultSelector)
        {
            using var e1         = first.GetEnumerator();
            using var e2         = second.GetEnumerator();
            var       e1HasValue = e1.MoveNext();
            var       e2HasValue = e2.MoveNext();
            while (e1HasValue || e2HasValue)
            {
                yield return resultSelector(
                    GetCurrentOrDefault(e1, e1HasValue),
                    GetCurrentOrDefault(e2, e2HasValue)
                );
                e1HasValue = e1.MoveNext();
                e2HasValue = e2.MoveNext();
            }
        }

        [Pure]
        public static IEnumerable<TResult> ZipLongest<TFirst, TSecond, TThird, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst?, TSecond?, TThird?, TResult> resultSelector)
        {
            using var e1         = first.GetEnumerator();
            using var e2         = second.GetEnumerator();
            using var e3         = third.GetEnumerator();
            var       e1HasValue = e1.MoveNext();
            var       e2HasValue = e2.MoveNext();
            var       e3HasValue = e3.MoveNext();
            while (e1HasValue || e2HasValue || e3HasValue)
            {
                yield return resultSelector(
                    GetCurrentOrDefault(e1, e1HasValue),
                    GetCurrentOrDefault(e2, e2HasValue),
                    GetCurrentOrDefault(e3, e3HasValue)
                );
                e1HasValue = e1.MoveNext();
                e2HasValue = e2.MoveNext();
                e3HasValue = e3.MoveNext();
            }
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst?, TSecond?)> ZipLongest<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return ZipLongest(first, second, (i1, i2) => (i1, i2));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst?, TSecond?, TThird?)> ZipLongest<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return ZipLongest(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

        [Pure]
        public static IEnumerable<TResult> Product<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            var firstCollection  = first as ICollection<TFirst> ?? first.ToArray();
            var secondCollection = second as ICollection<TSecond> ?? second.ToArray();
            foreach (var i1 in firstCollection)
            foreach (var i2 in secondCollection)
                yield return resultSelector(i1, i2);
        }

        [Pure]
        public static IEnumerable<TResult> Product<TFirst, TSecond, TThird, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            var firstCollection  = first as ICollection<TFirst> ?? first.ToArray();
            var secondCollection = second as ICollection<TSecond> ?? second.ToArray();
            var thirdCollection  = third as ICollection<TThird> ?? third.ToArray();
            foreach (var i1 in firstCollection)
            foreach (var i2 in secondCollection)
            foreach (var i3 in thirdCollection)
                yield return resultSelector(i1, i2, i3);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst, TSecond)> Product<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return Product(first, second, (i1, i2) => (i1, i2));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(TFirst, TSecond, TThird)> Product<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return Product(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<(int, int)> Product(int first, int second)
        {
            return Product(Ranges.Take(first), Ranges.Take(second));
        }

        [Pure]
        public static IEnumerable<IReadOnlyList<T>> Permutations<T>(IEnumerable<T> enumerables, int count, bool useSharedBuffer = false)
        {
            var items  = enumerables as IList<T> ?? enumerables.ToArray();
            var buffer = new List<T>(count);
            var used   = new bool[items.Count];
            return PermutationsRecursive(items, count, buffer, used, useSharedBuffer);

            static IEnumerable<IReadOnlyList<T>> PermutationsRecursive(IList<T> items, int count, List<T> buffer, bool[] used, bool useSharedBuffer)
            {
                if (count is 0)
                {
                    yield return useSharedBuffer ? buffer : buffer.ToArray();
                    yield break;
                }

                for (var i = 0; i < items.Count; ++i)
                {
                    if (used[i]) continue;

                    used[i] = true;
                    buffer.Add(items[i]);

                    foreach (var result in PermutationsRecursive(items, count - 1, buffer, used, useSharedBuffer))
                    {
                        yield return result;
                    }

                    buffer.RemoveAt(buffer.Count - 1);
                    used[i] = false;
                }
            }
        }

        [Pure]
        public static IEnumerable<IReadOnlyList<T>> Permutations<T>(IEnumerable<T> enumerables, bool useSharedBuffer = false)
        {
            var items = enumerables as IList<T> ?? enumerables.ToArray();
            return Permutations(items, items.Count, useSharedBuffer);
        }

        [Pure]
        public static IEnumerable<IReadOnlyList<T>> Combinations<T>(IEnumerable<T> enumerables, int count, bool useSharedBuffer = false)
        {
            var items  = enumerables as IList<T> ?? enumerables.ToArray();
            var buffer = new List<T>(count);
            return CombinationsRecursive(items, count, 0, buffer, useSharedBuffer);

            static IEnumerable<IReadOnlyList<T>> CombinationsRecursive(IList<T> items, int count, int startIndex, List<T> buffer, bool useSharedBuffer)
            {
                if (count is 0)
                {
                    yield return useSharedBuffer ? buffer : buffer.ToArray();
                    yield break;
                }

                for (var i = startIndex; i <= items.Count - count; ++i)
                {
                    buffer.Add(items[i]);

                    foreach (var result in CombinationsRecursive(items, count - 1, i + 1, buffer, useSharedBuffer))
                    {
                        yield return result;
                    }

                    buffer.RemoveAt(buffer.Count - 1);
                }
            }
        }

        [Pure]
        public static IEnumerable<IReadOnlyList<T>> CombinationsWithReplacement<T>(IEnumerable<T> enumerables, int count, bool useSharedBuffer = false)
        {
            var items  = enumerables as IList<T> ?? enumerables.ToArray();
            var buffer = new List<T>(count);
            return CombinationsWithReplacementRecursive(items, count, 0, buffer, useSharedBuffer);

            static IEnumerable<IReadOnlyList<T>> CombinationsWithReplacementRecursive(IList<T> items, int count, int startIndex, List<T> buffer, bool useSharedBuffer)
            {
                if (count is 0)
                {
                    yield return useSharedBuffer ? buffer : buffer.ToArray();
                    yield break;
                }

                for (var i = startIndex; i < items.Count; ++i)
                {
                    buffer.Add(items[i]);

                    foreach (var result in CombinationsWithReplacementRecursive(items, count - 1, i, buffer, useSharedBuffer))
                    {
                        yield return result;
                    }

                    buffer.RemoveAt(buffer.Count - 1);
                }
            }
        }

        [Pure]
        public static IEnumerable<T> Repeat<T>(Func<T> itemFactory, int count)
        {
            while (count-- > 0) yield return itemFactory();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Repeat(Action action, int count)
        {
            while (count-- > 0) action();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T? GetCurrentOrDefault<T>(IEnumerator<T> enumerator, bool hasValue) => hasValue ? enumerator.Current : default;
    }
}