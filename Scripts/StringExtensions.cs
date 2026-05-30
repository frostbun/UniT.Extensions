#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class StringExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        [Pure]
        public static string Trim(this string str, string trimStr)
        {
            var isTrimmed = false;
            var span      = str.AsSpan();
            if (span.StartsWith(trimStr))
            {
                span      = span[trimStr.Length..];
                isTrimmed = true;
            }
            if (span.EndsWith(trimStr))
            {
                span      = span[..^trimStr.Length];
                isTrimmed = true;
            }
            return isTrimmed ? span.ToString() : str;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string TrimStart(this string str, string trimStr)
        {
            var span = str.AsSpan();
            return span.StartsWith(trimStr) ? span[trimStr.Length..].ToString() : str;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string TrimEnd(this string str, string trimStr)
        {
            var span = str.AsSpan();
            return span.EndsWith(trimStr) ? span[..^trimStr.Length].ToString() : str;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<T>(this IEnumerable<T> enumerable, char separator)
        {
            return string.Join(separator, enumerable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wrap(this string str, string wrapper)
        {
            return string.Concat(wrapper, str, wrapper);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wrap(this string str, string prefix, string suffix)
        {
            return string.Concat(prefix, str, suffix);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string? NullIfEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string? NullIfWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string EmptyIfNull(this string? str)
        {
            return str ?? string.Empty;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string WithColor(this string str, Color color)
        {
            return string.Concat("<color=#", ColorUtility.ToHtmlStringRGB(color), ">", str, "</color>");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string WithSize(this string str, int pixel)
        {
            return string.Concat("<size=", pixel, ">", str, "</size>");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MultiplySize(this string str, float multiplier)
        {
            return string.Concat("<size=", multiplier * 100, "%>", str, "</size>");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AddSize(this string str, int pixel)
        {
            return string.Concat("<size=", pixel > 0 ? "+" : "-", Mathf.Abs(pixel), ">", str, "</size>");
        }
    }
}