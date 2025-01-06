#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class TransformExtensions
    {
        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = transform.position.WithX(x);
        }

        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = transform.position.WithY(y);
        }

        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = transform.position.WithZ(z);
        }

        public static void SetLocalPositionX(this Transform transform, float x)
        {
            transform.localPosition = transform.localPosition.WithX(x);
        }

        public static void SetLocalPositionY(this Transform transform, float y)
        {
            transform.localPosition = transform.localPosition.WithY(y);
        }

        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            transform.localPosition = transform.localPosition.WithZ(z);
        }

        public static void SetEulerAngleX(this Transform transform, float x)
        {
            transform.eulerAngles = transform.eulerAngles.WithX(x);
        }

        public static void SetEulerAngleY(this Transform transform, float y)
        {
            transform.eulerAngles = transform.eulerAngles.WithY(y);
        }

        public static void SetEulerAngleZ(this Transform transform, float z)
        {
            transform.eulerAngles = transform.eulerAngles.WithZ(z);
        }

        public static void SetLocalEulerAngleX(this Transform transform, float x)
        {
            transform.localEulerAngles = transform.localEulerAngles.WithX(x);
        }

        public static void SetLocalEulerAngleY(this Transform transform, float y)
        {
            transform.localEulerAngles = transform.localEulerAngles.WithY(y);
        }

        public static void SetLocalEulerAngleZ(this Transform transform, float z)
        {
            transform.localEulerAngles = transform.localEulerAngles.WithZ(z);
        }

        public static void SetRotationX(this Transform transform, float x)
        {
            transform.rotation = transform.rotation.WithX(x);
        }

        public static void SetRotationY(this Transform transform, float y)
        {
            transform.rotation = transform.rotation.WithY(y);
        }

        public static void SetRotationZ(this Transform transform, float z)
        {
            transform.rotation = transform.rotation.WithZ(z);
        }

        public static void SetRotationW(this Transform transform, float w)
        {
            transform.rotation = transform.rotation.WithW(w);
        }

        public static void SetLocalRotationX(this Transform transform, float x)
        {
            transform.localRotation = transform.localRotation.WithX(x);
        }

        public static void SetLocalRotationY(this Transform transform, float y)
        {
            transform.localRotation = transform.localRotation.WithY(y);
        }

        public static void SetLocalRotationZ(this Transform transform, float z)
        {
            transform.localRotation = transform.localRotation.WithZ(z);
        }

        public static void SetLocalRotationW(this Transform transform, float w)
        {
            transform.localRotation = transform.localRotation.WithW(w);
        }

        public static void SetScale(this Transform transform, Vector3 scale)
        {
            var lossyScale = transform.lossyScale;
            var localScale = transform.localScale;
            transform.localScale = new Vector3(
                scale.x / lossyScale.x * localScale.x,
                scale.y / lossyScale.y * localScale.y,
                scale.z / lossyScale.z * localScale.z
            );
        }

        public static void SetScale(this Transform transform, float scale)
        {
            transform.SetScale(Vector3.one * scale);
        }

        public static void SetScaleX(this Transform transform, float x)
        {
            transform.SetScale(transform.localScale.WithX(x));
        }

        public static void SetScaleY(this Transform transform, float y)
        {
            transform.SetScale(transform.localScale.WithY(y));
        }

        public static void SetScaleZ(this Transform transform, float z)
        {
            transform.SetScale(transform.localScale.WithZ(z));
        }

        public static void SetLocalScale(this Transform transform, Vector3 scale)
        {
            transform.localScale = scale;
        }

        public static void SetLocalScale(this Transform transform, float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        public static void SetLocalScaleX(this Transform transform, float x)
        {
            transform.localScale = transform.localScale.WithX(x);
        }

        public static void SetLocalScaleY(this Transform transform, float y)
        {
            transform.localScale = transform.localScale.WithY(y);
        }

        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            transform.localScale = transform.localScale.WithZ(z);
        }

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