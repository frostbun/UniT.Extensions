#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using Random = UnityEngine.Random;

    public static class ReadOnlyCollectionExtensions
    {
        public static int GetIndexR<T>(this IReadOnlyList<T> list, Index index)
        {
            return index.IsFromEnd ? list.Count - index.Value : index.Value;
        }

        public static IEnumerable<T> SliceR<T>(this IReadOnlyList<T> list, int start, int end, int step = 1)
        {
            for (var i = start; i < end; i += step)
            {
                yield return list[i];
            }
        }

        public static IEnumerable<T> SliceR<T>(this IReadOnlyList<T> list, Index start, Index stop, int step = 1)
        {
            return list.SliceR(list.GetIndexR(start), list.GetIndexR(stop), step);
        }

        public static IEnumerable<T> SliceR<T>(this IReadOnlyList<T> list, Range range, int step = 1)
        {
            return list.SliceR(range.Start, range.End, step);
        }

        public static IEnumerable<T> EachR<T>(this IReadOnlyList<T> list, int step)
        {
            return list.SliceR(.., step);
        }

        public static T RandomR<T>(this IReadOnlyList<T> list)
        {
            return list.Count > 0 ? list[Random.Range(0, list.Count)] : throw new InvalidOperationException("List empty");
        }

        public static T? RandomOrDefaultR<T>(this IReadOnlyList<T> list, T? defaultValue = default)
        {
            return list.Count > 0 ? list[Random.Range(0, list.Count)] : defaultValue;
        }

        public static T RandomOrDefaultR<T>(this IReadOnlyList<T> list, Func<T> valueFactory)
        {
            return list.Count > 0 ? list[Random.Range(0, list.Count)] : valueFactory();
        }
    }
}