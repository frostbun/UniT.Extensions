#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class TransformExtensions
    {
        public static bool Overlaps(this RectTransform t1, RectTransform t2)
        {
            var p1    = t1.position;
            var p2    = t2.position;
            var r1    = t1.rect;
            var r2    = t2.rect;
            var rect1 = new Rect(p1.x, p1.y, r1.width, r1.height);
            var rect2 = new Rect(p2.x, p2.y, r2.width, r2.height);
            return rect1.Overlaps(rect2);
        }
    }
}