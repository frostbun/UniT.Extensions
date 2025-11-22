#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class GroupExtensions
    {
        public static IGrouping<TKey, TItem> MinByKey<TKey, TItem>(this IEnumerable<IGrouping<TKey, TItem>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.MinBy(kv => kv.Key, comparer);
        }

        public static IGrouping<TKey, TItem> MinByKey<TKey, TItem, TCompareKey>(this IEnumerable<IGrouping<TKey, TItem>> dictionary, Func<TKey, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MinBy(kv => keySelector(kv.Key), comparer);
        }

        public static IGrouping<TKey, TItem> MaxByKey<TKey, TItem>(this IEnumerable<IGrouping<TKey, TItem>> dictionary, IComparer<TKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => kv.Key, comparer);
        }

        public static IGrouping<TKey, TItem> MaxByKey<TKey, TItem, TCompareKey>(this IEnumerable<IGrouping<TKey, TItem>> dictionary, Func<TKey, TCompareKey> keySelector, IComparer<TCompareKey>? comparer = null)
        {
            return dictionary.MaxBy(kv => keySelector(kv.Key), comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> OrderByKey<TItem, TKey>(this IEnumerable<IGrouping<TKey, TItem>> enumerable, IComparer<TKey>? comparer = null)
        {
            return enumerable.OrderBy(group => group.Key, comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> OrderByKey<TItem, TKey, TOrderKey>(this IEnumerable<IGrouping<TKey, TItem>> enumerable, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return enumerable.OrderBy(group => keySelector(group.Key), comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> OrderByDescendingKey<TItem, TKey>(this IEnumerable<IGrouping<TKey, TItem>> enumerable, IComparer<TKey>? comparer = null)
        {
            return enumerable.OrderByDescending(group => group.Key, comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> OrderByDescendingKey<TItem, TKey, TOrderKey>(this IEnumerable<IGrouping<TKey, TItem>> enumerable, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return enumerable.OrderByDescending(group => keySelector(group.Key), comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> ThenByKey<TItem, TKey>(this IOrderedEnumerable<IGrouping<TKey, TItem>> enumerable, IComparer<TKey>? comparer = null)
        {
            return enumerable.ThenBy(group => group.Key, comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> ThenByKey<TItem, TKey, TOrderKey>(this IOrderedEnumerable<IGrouping<TKey, TItem>> enumerable, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return enumerable.ThenByDescending(group => keySelector(group.Key), comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> ThenByDescendingKey<TItem, TKey>(this IOrderedEnumerable<IGrouping<TKey, TItem>> enumerable, IComparer<TKey>? comparer = null)
        {
            return enumerable.ThenByDescending(group => group.Key, comparer);
        }

        public static IOrderedEnumerable<IGrouping<TKey, TItem>> ThenByDescendingKey<TItem, TKey, TOrderKey>(this IOrderedEnumerable<IGrouping<TKey, TItem>> enumerable, Func<TKey, TOrderKey> keySelector, IComparer<TOrderKey>? comparer = null)
        {
            return enumerable.ThenByDescending(group => keySelector(group.Key), comparer);
        }
    }
}