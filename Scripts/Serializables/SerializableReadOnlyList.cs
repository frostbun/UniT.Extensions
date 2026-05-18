#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class SerializableReadOnlyList<T> : IReadOnlyList<T>
    {
        [SerializeReference] private List<T> values;

        public SerializableReadOnlyList() : this(new())
        {
        }

        public SerializableReadOnlyList(List<T> values)
        {
            this.values = values;
        }

        public int Count => this.values.Count;

        public T this[int index] => this.values[index];

        public List<T>.Enumerator GetEnumerator() => this.values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.values.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => this.values.GetEnumerator();
    }
}