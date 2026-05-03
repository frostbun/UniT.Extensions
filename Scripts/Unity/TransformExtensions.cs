#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class TransformExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPosition(this Transform transform, Vector3 position)
        {
            transform.position = position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionX(this Transform transform, float x)
        {
            transform.position = transform.position.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionY(this Transform transform, float y)
        {
            transform.position = transform.position.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetPositionZ(this Transform transform, float z)
        {
            transform.position = transform.position.WithZ(z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPosition(this Transform transform, Vector3 position)
        {
            transform.localPosition = position;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPositionX(this Transform transform, float x)
        {
            transform.localPosition = transform.localPosition.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPositionY(this Transform transform, float y)
        {
            transform.localPosition = transform.localPosition.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalPositionZ(this Transform transform, float z)
        {
            transform.localPosition = transform.localPosition.WithZ(z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerAngle(this Transform transform, Vector3 eulerAngle)
        {
            transform.eulerAngles = eulerAngle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerAngleX(this Transform transform, float x)
        {
            transform.eulerAngles = transform.eulerAngles.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerAngleY(this Transform transform, float y)
        {
            transform.eulerAngles = transform.eulerAngles.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEulerAngleZ(this Transform transform, float z)
        {
            transform.eulerAngles = transform.eulerAngles.WithZ(z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalEulerAngle(this Transform transform, Vector3 localEulerAngle)
        {
            transform.localEulerAngles = localEulerAngle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalEulerAngleX(this Transform transform, float x)
        {
            transform.localEulerAngles = transform.localEulerAngles.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalEulerAngleY(this Transform transform, float y)
        {
            transform.localEulerAngles = transform.localEulerAngles.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalEulerAngleZ(this Transform transform, float z)
        {
            transform.localEulerAngles = transform.localEulerAngles.WithZ(z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotation(this Transform transform, Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotationX(this Transform transform, float x)
        {
            transform.rotation = transform.rotation.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotationY(this Transform transform, float y)
        {
            transform.rotation = transform.rotation.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotationZ(this Transform transform, float z)
        {
            transform.rotation = transform.rotation.WithZ(z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRotationW(this Transform transform, float w)
        {
            transform.rotation = transform.rotation.WithW(w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalRotation(this Transform transform, Quaternion localRotation)
        {
            transform.localRotation = localRotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalRotationX(this Transform transform, float x)
        {
            transform.localRotation = transform.localRotation.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalRotationY(this Transform transform, float y)
        {
            transform.localRotation = transform.localRotation.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalRotationZ(this Transform transform, float z)
        {
            transform.localRotation = transform.localRotation.WithZ(z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalRotationW(this Transform transform, float w)
        {
            transform.localRotation = transform.localRotation.WithW(w);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScale(this Transform transform, Vector3 scale)
        {
            var lossyScale = transform.lossyScale;
            var localScale = transform.localScale;
            transform.localScale = new(
                scale.x / lossyScale.x * localScale.x,
                scale.y / lossyScale.y * localScale.y,
                scale.z / lossyScale.z * localScale.z
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScale(this Transform transform, float scale)
        {
            transform.SetScale(Vector3.one * scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleX(this Transform transform, float x)
        {
            transform.SetScale(transform.lossyScale.WithX(x));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleY(this Transform transform, float y)
        {
            transform.SetScale(transform.lossyScale.WithY(y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetScaleZ(this Transform transform, float z)
        {
            transform.SetScale(transform.lossyScale.WithZ(z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalScale(this Transform transform, Vector3 scale)
        {
            transform.localScale = scale;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalScale(this Transform transform, float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalScaleX(this Transform transform, float x)
        {
            transform.localScale = transform.localScale.WithX(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalScaleY(this Transform transform, float y)
        {
            transform.localScale = transform.localScale.WithY(y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            transform.localScale = transform.localScale.WithZ(z);
        }

        [Pure]
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