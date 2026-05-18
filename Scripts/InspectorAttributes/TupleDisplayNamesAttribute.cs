#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TupleDisplayNamesAttribute : PropertyAttribute
    {
        public IReadOnlyList<string> Names { get; }

        public TupleDisplayNamesAttribute(params string[] names)
        {
            this.Names = names;
        }
    }
}