#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;
    using UnityEngine.UI;

    public static class ColorExtensions
    {
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float a)
        {
            spriteRenderer.color = spriteRenderer.color.WithAlpha(a);
        }

        public static void SetAlpha(this Material material, float a)
        {
            material.color = material.color.WithAlpha(a);
        }

        public static void SetAlpha(this Graphic graphic, float a)
        {
            graphic.color = graphic.color.WithAlpha(a);
        }

        public static Color WithAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        public static string WithColor(this string str, Color color)
        {
            return $"<color=#{color.ToHex()}>{str}</color>";
        }

        public static string ToHex(this Color color)
        {
            return $"{(byte)(color.r * 255):X2}{(byte)(color.g * 255):X2}{(byte)(color.b * 255):X2}";
        }
    }
}