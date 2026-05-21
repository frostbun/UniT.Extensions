#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class ColorExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColor(this SpriteRenderer spriteRenderer, Color color)
        {
            spriteRenderer.color = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float a)
        {
            spriteRenderer.color = spriteRenderer.color.WithAlpha(a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColor(this Material material, Color color)
        {
            material.color = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAlpha(this Material material, float a)
        {
            material.color = material.color.WithAlpha(a);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color WithAlpha(this Color color, float a)
        {
            color.a = a;
            return color;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color32 WithAlpha(this Color32 color, byte a)
        {
            color.a = a;
            return color;
        }
    }
}