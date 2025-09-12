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
            var size = Mathf.Max(
                rect.width / camera.aspect * (1 + paddingBySize.x) + padding.x,
                rect.height * (1 + paddingBySize.y) + padding.y
            );
            camera.Fit(rect.center, size / 2, offsetBySize, offset);
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