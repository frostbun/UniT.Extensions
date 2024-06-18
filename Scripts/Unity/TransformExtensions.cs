#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class TransformExtensions
    {
        public static bool Overlaps(this RectTransform transform1, RectTransform transform2)
        {
            var rect1 = transform1.rect;
            var rect2 = transform2.rect;
            rect1.position += (Vector2)transform1.position;
            rect2.position += (Vector2)transform2.position;
            return rect1.Overlaps(rect2);
        }
    }
}