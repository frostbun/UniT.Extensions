#nullable enable
namespace UniT.Extensions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    public static class EnumeratorExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetNext(this IEnumerator enumerator, [MaybeNullWhen(false)] out object value)
        {
            if (!enumerator.MoveNext())
            {
                value = null;
                return false;
            }
            value = enumerator.Current;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetNext<T>(this IEnumerator<T> enumerator, [MaybeNullWhen(false)] out T value)
        {
            if (!enumerator.MoveNext())
            {
                value = default;
                return false;
            }
            value = enumerator.Current;
            return true;
        }
    }
}