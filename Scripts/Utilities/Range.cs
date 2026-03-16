#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;

    public static class Ranges
    {
        [Pure]
        public static From From(int start) => new From(start);

        [Pure]
        public static RangeEnumerable To(int stop) => new From(0).To(stop);

        [Pure]
        public static RangeEnumerable Take(int count) => new From(0).Take(count);
    }

    public readonly struct From
    {
        private readonly int start;

        public From(int start)
        {
            this.start = start;
        }

        [Pure]
        public RangeEnumerable To(int stop)
        {
            return new RangeEnumerable(this.start, stop);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RangeEnumerable Take(int count)
        {
            return this.To(this.start + count);
        }
    }

    public readonly struct RangeEnumerable : IEnumerable<int>
    {
        private readonly int start;
        private readonly int stop;

        public RangeEnumerable(int start, int stop)
        {
            this.start = start;
            this.stop  = stop;
        }

        public RangeEnumerator GetEnumerator()
        {
            return new RangeEnumerator(this.start, this.stop);
        }

        IEnumerator<int> IEnumerable<int>.GetEnumerator() => this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public struct RangeEnumerator : IEnumerator<int>
    {
        private readonly int stop;
        private          int current;

        public RangeEnumerator(int start, int stop)
        {
            this.stop    = stop;
            this.current = start - 1;
        }

        public readonly int Current => this.current;

        public bool MoveNext()
        {
            if (this.current >= this.stop - 1) return false;
            ++this.current;
            return true;
        }

        readonly object IEnumerator.Current => this.current;

        void IEnumerator.Reset() => throw new NotSupportedException();

        void IDisposable.Dispose() { }
    }
}