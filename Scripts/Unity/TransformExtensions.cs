namespace UniT.Extensions
{
    using UnityEngine;

    public static class TransformExtensions
    {
        public static bool Overlaps(this RectTransform t1, RectTransform t2)
        {
            var rect1 = new Rect(t1.position.x, t1.position.y, t1.rect.width, t1.rect.height);
            var rect2 = new Rect(t2.position.x, t2.position.y, t2.rect.width, t2.rect.height);
            return rect1.Overlaps(rect2);
        }
    }
}