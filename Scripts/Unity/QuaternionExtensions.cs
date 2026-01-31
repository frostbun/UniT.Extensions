#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using UnityEngine;

    public static class QuaternionExtensions
    {
        [Pure]
        public static Quaternion WithX(this Quaternion quaternion, float x)
        {
            quaternion.x = x;
            return quaternion;
        }

        [Pure]
        public static Quaternion WithY(this Quaternion quaternion, float y)
        {
            quaternion.y = y;
            return quaternion;
        }

        [Pure]
        public static Quaternion WithZ(this Quaternion quaternion, float z)
        {
            quaternion.z = z;
            return quaternion;
        }

        [Pure]
        public static Quaternion WithW(this Quaternion quaternion, float w)
        {
            quaternion.w = w;
            return quaternion;
        }

        [Pure]
        public static Quaternion WithEulerAngleX(this Quaternion quaternion, float x)
        {
            return Quaternion.Euler(quaternion.eulerAngles.WithX(x));
        }

        [Pure]
        public static Quaternion WithEulerAngleY(this Quaternion quaternion, float y)
        {
            return Quaternion.Euler(quaternion.eulerAngles.WithY(y));
        }

        [Pure]
        public static Quaternion WithEulerAngleZ(this Quaternion quaternion, float z)
        {
            return Quaternion.Euler(quaternion.eulerAngles.WithZ(z));
        }
    }
}