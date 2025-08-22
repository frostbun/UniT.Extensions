#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class CameraExtensions
    {
        public static void FitDiagonal(this Camera camera, Bounds bounds, Vector3 offsetPercent = default, Vector3 offset = default, float paddingPercent = 0, float padding = 0)
        {
            camera.Fit(bounds.center, bounds.extents.magnitude, offsetPercent, offset, paddingPercent, padding);
        }

        public static void FitDiagonal(this Camera camera, Rect rect, Vector3 offsetPercent = default, Vector3 offset = default, float paddingPercent = 0, float padding = 0)
        {
            camera.Fit(rect.center, rect.size.magnitude / 2, offsetPercent, offset, paddingPercent, padding);
        }

        public static void Fit(this Camera camera, Rect rect, Vector3 offsetPercent = default, Vector3 offset = default, Vector2 paddingPercent = default, Vector2 padding = default)
        {
            var size = Mathf.Max(
                rect.width / camera.aspect * (1 + paddingPercent.x) + padding.x,
                rect.height * (1 + paddingPercent.y) + padding.y
            );
            camera.Fit(rect.center, size / 2, offsetPercent, offset);
        }

        public static void Fit(this Camera camera, Vector3 center, float size, Vector3 offsetPercent = default, Vector3 offset = default, float paddingPercent = 0, float padding = 0)
        {
            center = Vector3.Scale(center, Vector3.one + offsetPercent) + offset;
            size   = size * (1 + paddingPercent) + padding;
            if (camera.orthographic)
            {
                camera.orthographicSize   = size;
                camera.transform.position = center - camera.transform.forward * 10f;
            }
            else
            {
                var distance = size / Mathf.Tan(camera.fieldOfView / 2 * Mathf.Deg2Rad);
                camera.transform.position = center - camera.transform.forward * distance;
            }
        }
    }
}