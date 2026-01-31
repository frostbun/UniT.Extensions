#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using UnityEngine;
    #if UNIT_JSON
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    #endif

    public static class StringExtensions
    {
        [Pure]
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        [Pure]
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
        public static string TrimStart(this string str, string trimStr)
        {
            var isTrimmed = false;
            var span      = str.AsSpan();
            if (span.StartsWith(trimStr))
            {
                span      = span[trimStr.Length..];
                isTrimmed = true;
            }
            return isTrimmed ? span.ToString() : str;
        }

        [Pure]
        public static string TrimEnd(this string str, string trimStr)
        {
            var isTrimmed = false;
            var span      = str.AsSpan();
            if (span.EndsWith(trimStr))
            {
                span      = span[..^trimStr.Length];
                isTrimmed = true;
            }
            return isTrimmed ? span.ToString() : str;
        }

        [Pure]
        public static string Join<T>(this IEnumerable<T> enumerable, char separator)
        {
            return string.Join(separator, enumerable);
        }

        [Pure]
        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        [Pure]
        public static string Wrap(this string str, string wrapper)
        {
            return string.Concat(wrapper, str, wrapper);
        }

        [Pure]
        public static string Wrap(this string str, string prefix, string suffix)
        {
            return string.Concat(prefix, str, suffix);
        }

        [Pure]
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        [Pure]
        public static string? NullIfEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }

        [Pure]
        public static string? NullIfWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        [Pure]
        public static string EmptyIfNull(this string? str)
        {
            return str ?? string.Empty;
        }

        [Pure]
        public static string WithColor(this string str, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{str}</color>";
        }

        [Pure]
        public static string WithSize(this string str, int pixel)
        {
            return $"<size={pixel}>{str}</size>";
        }

        [Pure]
        public static string MultiplySize(this string str, float multiplier)
        {
            return $"<size={multiplier * 100}%>{str}</size>";
        }

        [Pure]
        public static string AddSize(this string str, int pixel)
        {
            return $"<size={(pixel > 0 ? "+" : "-")}{Mathf.Abs(pixel)}>{str}</size>";
        }

        #if UNIT_JSON
        private static readonly JsonSerializerSettings JSON_SETTINGS = new JsonSerializerSettings
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
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JSON_SETTINGS);
        }
        #endif
    }
}