namespace UniT.Extensions
{
    using System.Collections.Generic;
    #if UNIT_EXTENSIONS_NEWTONSOFT_JSON
    using Newtonsoft.Json;
    #endif

    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
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

        #if UNIT_EXTENSIONS_NEWTONSOFT_JSON
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling      = TypeNameHandling.Auto,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }
        #endif
    }
}