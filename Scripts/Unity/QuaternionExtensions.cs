#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class QuaternionExtensions
    {
        public static Quaternion WithX(this Quaternion quaternion, float x)
        {
            quaternion.x = x;
            return quaternion;
        }

        public static Quaternion WithY(this Quaternion quaternion, float y)
        {
            quaternion.y = y;
            return quaternion;
        }

        public static Quaternion WithZ(this Quaternion quaternion, float z)
        {
            quaternion.z = z;
            return quaternion;
        }

        public static Quaternion WithW(this Quaternion quaternion, float w)
        {
            quaternion.w = w;
            return quaternion;
        }

        public static Quaternion WithEulerAngleX(this Quaternion quaternion, float x)
        {
            var eulerAngles = quaternion.eulerAngles;
            return Quaternion.Euler(x, eulerAngles.y, eulerAngles.z);
        }

        public static Quaternion WithEulerAngleY(this Quaternion quaternion, float y)
        {
            var eulerAngles = quaternion.eulerAngles;
            return Quaternion.Euler(eulerAngles.x, y, eulerAngles.z);
        }

        public static Quaternion WithEulerAngleZ(this Quaternion quaternion, float z)
        {
            var eulerAngles = quaternion.eulerAngles;
            return Quaternion.Euler(eulerAngles.x, eulerAngles.y, z);
        }
    }
}