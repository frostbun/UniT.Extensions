#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    #if UNIT_JSON
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    #endif
    #if UNIT_ZSTRING
    using Cysharp.Text;
    #endif

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
            #if UNIT_ZSTRING
            return ZString.Join(separator, enumerable);
            #else
            return string.Join(separator, enumerable);
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            #if UNIT_ZSTRING
            return ZString.Join(separator, enumerable);
            #else
            return string.Join(separator, enumerable);
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wrap(this string str, string wrapper)
        {
            #if UNIT_ZSTRING
            return ZString.Concat(wrapper, str, wrapper);
            #else
            return string.Concat(wrapper, str, wrapper);
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wrap(this string str, string prefix, string suffix)
        {
            #if UNIT_ZSTRING
            return ZString.Concat(prefix, str, suffix);
            #else
            return string.Concat(prefix, str, suffix);
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Format(this string str, params object[] args)
        {
            #if UNIT_ZSTRING
            return ZString.Format(str, args);
            #else
            return string.Format(str, args);
            #endif
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
            #if UNIT_ZSTRING
            return ZString.Concat("<color=#", ColorUtility.ToHtmlStringRGB(color), ">", str, "</color>");
            #else
            return string.Concat("<color=#", ColorUtility.ToHtmlStringRGB(color), ">", str, "</color>");
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string WithSize(this string str, int pixel)
        {
            #if UNIT_ZSTRING
            return ZString.Concat("<size=", pixel, ">", str, "</size>");
            #else
            return string.Concat("<size=", pixel, ">", str, "</size>");
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string MultiplySize(this string str, float multiplier)
        {
            #if UNIT_ZSTRING
            return ZString.Concat("<size=", multiplier * 100, "%>", str, "</size>");
            #else
            return string.Concat("<size=", multiplier * 100, "%>", str, "</size>");
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AddSize(this string str, int pixel)
        {
            #if UNIT_ZSTRING
            return ZString.Concat("<size=", pixel > 0 ? "+" : "-", Mathf.Abs(pixel), ">", str, "</size>");
            #else
            return string.Concat("<size=", pixel > 0 ? "+" : "-", Mathf.Abs(pixel), ">", str, "</size>");
            #endif
        }

        #if UNIT_JSON
        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            Culture               = CultureInfo.InvariantCulture,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting            = Formatting.Indented,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter(),
            },
        };

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }
        #endif
    }
}