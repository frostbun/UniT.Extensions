#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class QuaternionExtensions
    {
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithX(this Quaternion quaternion, float x)
        {
            quaternion.x = x;
            return quaternion;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithY(this Quaternion quaternion, float y)
        {
            quaternion.y = y;
            return quaternion;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithZ(this Quaternion quaternion, float z)
        {
            quaternion.z = z;
            return quaternion;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithW(this Quaternion quaternion, float w)
        {
            quaternion.w = w;
            return quaternion;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithEulerAngleX(this Quaternion quaternion, float x)
        {
            return Quaternion.Euler(quaternion.eulerAngles.WithX(x));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithEulerAngleY(this Quaternion quaternion, float y)
        {
            return Quaternion.Euler(quaternion.eulerAngles.WithY(y));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Quaternion WithEulerAngleZ(this Quaternion quaternion, float z)
        {
            return Quaternion.Euler(quaternion.eulerAngles.WithZ(z));
        }
    }
}