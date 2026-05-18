#nullable enable
namespace UniT.Extensions
{
    using System;
    using UnityEngine;

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SortingLayerAttribute : PropertyAttribute
    {
    }
}