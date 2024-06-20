#nullable enable
namespace UniT.Extensions
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    #if UNIT_JSON
    using System.Globalization;
    using Newtonsoft.Json;
    #endif

    public static class StringExtensions
    {
        public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhitespace([NotNullWhen(false)] this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string Join<T>(this IEnumerable<T> enumerable, char separator)
        {
            return string.Join(separator, enumerable);
        }

        public static string Join<T>(this IEnumerable<T> enumerable, string separator)
        {
            return string.Join(separator, enumerable);
        }

        public static string Wrap(this string str, string wrapper)
        {
            return string.Concat(wrapper, str, wrapper);
        }

        public static string Wrap(this string str, string prefix, string suffix)
        {
            return string.Concat(prefix, str, suffix);
        }

        public static string? NullIfEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }

        public static string? NullIfWhitespace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        public static string EmptyIfNull(this string? str)
        {
            return str ?? string.Empty;
        }

        #if UNIT_JSON
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            Culture                = CultureInfo.InvariantCulture,
            TypeNameHandling       = TypeNameHandling.Auto,
            ReferenceLoopHandling  = ReferenceLoopHandling.Ignore,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
        };

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }
        #endif
    }
}