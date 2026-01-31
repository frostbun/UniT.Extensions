#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using UnityEngine;
    using UnityEngine.UI;

    public static class ColorExtensions
    {
        public static void SetColor(this SpriteRenderer spriteRenderer, Color color)
        {
            spriteRenderer.color = color;
        }

        public static void SetAlpha(this SpriteRenderer spriteRenderer, float a)
        {
            spriteRenderer.color = spriteRenderer.color.WithAlpha(a);
        }

        public static void SetColor(this Material material, Color color)
        {
            material.color = color;
        }

        public static void SetAlpha(this Material material, float a)
        {
            material.color = material.color.WithAlpha(a);
        }

        public static void SetColor(this Graphic graphic, Color color)
        {
            graphic.color = color;
        }

        public static void SetAlpha(this Graphic graphic, float a)
        {
            graphic.color = graphic.color.WithAlpha(a);
        }

        [Pure]
        public static Color WithAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        [Pure]
        public static Color32 WithAlpha(this Color32 color, byte a)
        {
            color.a = a;
            return color;
        }
    }
}