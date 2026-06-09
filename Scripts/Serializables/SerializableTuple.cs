#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    [Serializable]
    public class SerializableTuple<T1, T2> : ITuple
    {
        [field: SerializeReference] public T1 Item1 { get; private set; }
        [field: SerializeReference] public T2 Item2 { get; private set; }

        public SerializableTuple() : this(default!, default!)
        {
        }

        public SerializableTuple(T1 item1, T2 item2)
        {
            this.Item1 = item1;
            this.Item2 = item2;
        }

        public void Deconstruct(out T1 x, out T2 y)
        {
            x = this.Item1;
            y = this.Item2;
        }

        int ITuple.Length => 2;

        object? ITuple.this[int index] => index switch
        {
            0 => this.Item1,
            1 => this.Item2,
            _ => throw new IndexOutOfRangeException(),
        };
    }

    [Serializable]
    public class SerializableTuple<T1, T2, T3> : ITuple
    {
        [field: SerializeReference] public T1 Item1 { get; private set; }
        [field: SerializeReference] public T2 Item2 { get; private set; }
        [field: SerializeReference] public T3 Item3 { get; private set; }

        public SerializableTuple() : this(default!, default!, default!)
        {
        }

        public SerializableTuple(T1 item1, T2 item2, T3 item3)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
        }

        public void Deconstruct(out T1 x, out T2 y, out T3 z)
        {
            x = this.Item1;
            y = this.Item2;
            z = this.Item3;
        }

        int ITuple.Length => 3;

        object? ITuple.this[int index] => index switch
        {
            0 => this.Item1,
            1 => this.Item2,
            2 => this.Item3,
            _ => throw new IndexOutOfRangeException(),
        };
    }

    [Serializable]
    public class SerializableTuple<T1, T2, T3, T4> : ITuple
    {
        [field: SerializeReference] public T1 Item1 { get; private set; }
        [field: SerializeReference] public T2 Item2 { get; private set; }
        [field: SerializeReference] public T3 Item3 { get; private set; }
        [field: SerializeReference] public T4 Item4 { get; private set; }

        public SerializableTuple() : this(default!, default!, default!, default!)
        {
        }

        public SerializableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
        {
            this.Item1 = item1;
            this.Item2 = item2;
            this.Item3 = item3;
            this.Item4 = item4;
        }

        public void Deconstruct(out T1 x, out T2 y, out T3 z, out T4 w)
        {
            x = this.Item1;
            y = this.Item2;
            z = this.Item3;
            w = this.Item4;
        }

        int ITuple.Length => 4;

        object? ITuple.this[int index] => index switch
        {
            0 => this.Item1,
            1 => this.Item2,
            2 => this.Item3,
            3 => this.Item4,
            _ => throw new IndexOutOfRangeException(),
        };
    }
}