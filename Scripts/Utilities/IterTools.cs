#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IterTools
    {
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
            if (e1HasValue || e2HasValue) throw new InvalidOperationException("The number of items is different. If this is intentional, use ZipShortest or ZipLongest instead.");
        }

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
            if (e1HasValue || e2HasValue || e3HasValue) throw new InvalidOperationException("The number of items is different. If this is intentional, use ZipShortest or ZipLongest instead.");
        }

        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return Zip(first, second, (i1, i2) => (i1, i2));
        }

        public static IEnumerable<(TFirst, TSecond, TThird)> Zip<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return Zip(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

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

        public static IEnumerable<(TFirst, TSecond)> ZipShortest<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return ZipShortest(first, second, (i1, i2) => (i1, i2));
        }

        public static IEnumerable<(TFirst, TSecond, TThird)> ZipShortest<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return ZipShortest(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

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

        public static IEnumerable<(TFirst?, TSecond?)> ZipLongest<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return ZipLongest(first, second, (i1, i2) => (i1, i2));
        }

        public static IEnumerable<(TFirst?, TSecond?, TThird?)> ZipLongest<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return ZipLongest(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

        public static IEnumerable<TResult> Product<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            return first.SelectMany(i1 => second.Select(i2 => resultSelector(i1, i2)));
        }

        public static IEnumerable<TResult> Product<TFirst, TSecond, TThird, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            return first.SelectMany(i1 => second.SelectMany(i2 => third.Select(i3 => resultSelector(i1, i2, i3))));
        }

        public static IEnumerable<(TFirst, TSecond)> Product<TFirst, TSecond>(IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            return Product(first, second, (i1, i2) => (i1, i2));
        }

        public static IEnumerable<(TFirst, TSecond, TThird)> Product<TFirst, TSecond, TThird>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, IEnumerable<TThird> third)
        {
            return Product(first, second, third, (i1, i2, i3) => (i1, i2, i3));
        }

        public static IEnumerable<int> Range(int start, int stop)
        {
            while (start < stop) yield return start++;
        }

        public static IEnumerable<int> Range(int stop)
        {
            return Range(0, stop);
        }

        public static IEnumerable<T> Repeat<T>(Func<T> itemFactory, int count)
        {
            while (count-- > 0) yield return itemFactory();
        }

        public static void Repeat(Action action, int count)
        {
            while (count-- > 0) action();
        }

        private static T? GetCurrentOrDefault<T>(IEnumerator<T> enumerator, bool hasValue) => hasValue ? enumerator.Current : default;
    }
}