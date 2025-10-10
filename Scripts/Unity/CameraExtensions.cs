#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class CameraExtensions
    {
        public static void FitDiagonal(this Camera camera, Bounds bounds, Vector3 offsetBySize = default, Vector3 offset = default, float paddingBySize = 0, float padding = 0)
        {
            camera.Fit(bounds.center, bounds.extents.magnitude, offsetBySize, offset, paddingBySize, padding);
        }

        public static void FitDiagonal(this Camera camera, Rect rect, Vector3 offsetBySize = default, Vector3 offset = default, float paddingBySize = 0, float padding = 0)
        {
            camera.Fit(rect.center, rect.size.magnitude / 2, offsetBySize, offset, paddingBySize, padding);
        }

        public static void Fit(this Camera camera, Rect rect, Vector3 offsetBySize = default, Vector3 offset = default, Vector2 paddingBySize = default, Vector2 padding = default)
        {
            var sizeWithPadding = rect.size * (Vector2.one + paddingBySize) + padding;
            camera.Fit(rect.center, Mathf.Max(sizeWithPadding.x / camera.aspect, sizeWithPadding.y) / 2, offsetBySize, offset);
        }

        public static void Fit(this Camera camera, Vector3 center, Vector2 size, Vector3 offsetBySize = default, Vector3 offset = default, Vector2 paddingBySize = default, Vector2 padding = default)
        {
            var sizeWithPadding = size * (Vector2.one + paddingBySize) + padding;
            camera.Fit(center, Mathf.Max(sizeWithPadding.x / camera.aspect, sizeWithPadding.y) / 2, offsetBySize, offset);
        }

        public static void Fit(this Camera camera, Vector3 center, float size, Vector3 offsetBySize = default, Vector3 offset = default, float paddingBySize = 0, float padding = 0)
        {
            center = center + size * offsetBySize + offset;
            size   = size * (1 + paddingBySize) + padding;
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