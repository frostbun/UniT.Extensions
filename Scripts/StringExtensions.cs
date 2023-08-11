namespace UniT.Extensions
{
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;
    using UnityEngine;

    public static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrWhitespace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wrap(this string str, string wrapper)
        {
            return str.Wrap(wrapper, wrapper);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wrap(this string str, string prefix, string suffix)
        {
            return $"{prefix}{str}{suffix}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string WithColor(this string str, Color? color)
        {
            return color.HasValue ? $"<color=#{color.Value.ToHex()}>{str}</color>" : str;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToHex(this Color color)
        {
            return $"{(byte)(color.r * 255f):X2}{(byte)(color.g * 255f):X2}{(byte)(color.b * 255f):X2}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}