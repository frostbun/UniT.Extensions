#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class ColorExtensions
    {
        public static string WithColor(this string str, Color color)
        {
            return $"<color=#{color.ToHex()}>{str}</color>";
        }

        public static string ToHex(this Color color)
        {
            return $"{(byte)(color.r * 255):X2}{(byte)(color.g * 255):X2}{(byte)(color.b * 255):X2}";
        }

        public static Color WithR(this Color color, float r)
        {
            color.r = r;
            return color;
        }

        public static Color WithG(this Color color, float g)
        {
            color.g = g;
            return color;
        }

        public static Color WithB(this Color color, float b)
        {
            color.b = b;
            return color;
        }

        public static Color WithA(this Color color, float a)
        {
            color.a = a;
            return color;
        }
    }
}