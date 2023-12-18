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

        public static Quaternion WithEulerX(this Quaternion quaternion, float x)
        {
            var euler = quaternion.eulerAngles;
            return Quaternion.Euler(x, euler.y, euler.z);
        }

        public static Quaternion WithEulerY(this Quaternion quaternion, float y)
        {
            var euler = quaternion.eulerAngles;
            return Quaternion.Euler(euler.x, y, euler.z);
        }

        public static Quaternion WithEulerZ(this Quaternion quaternion, float z)
        {
            var euler = quaternion.eulerAngles;
            return Quaternion.Euler(euler.x, euler.y, z);
        }
    }
}