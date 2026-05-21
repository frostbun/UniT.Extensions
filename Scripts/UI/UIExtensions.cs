#nullable enable
namespace UniT.Extensions
{
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using UnityEngine.UI;

    public static class UIExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this Slider slider, float value)
        {
            slider.value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFillAmount(this Image image, float value)
        {
            image.fillAmount = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetColor(this Graphic graphic, Color color)
        {
            graphic.color = color;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAlpha(this Graphic graphic, float a)
        {
            graphic.color = graphic.color.WithAlpha(a);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetAlpha(this CanvasGroup canvasGroup, float a)
        {
            canvasGroup.alpha = a;
        }
    }
}