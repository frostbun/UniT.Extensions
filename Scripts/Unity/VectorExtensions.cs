#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using UnityEngine;

    public static class VectorExtensions
    {
        [Pure]
        public static Vector2 WithX(this Vector2 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        [Pure]
        public static Vector2 WithY(this Vector2 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        [Pure]
        public static Vector3 WithX(this Vector3 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        [Pure]
        public static Vector3 WithY(this Vector3 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        [Pure]
        public static Vector3 WithZ(this Vector3 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        [Pure]
        public static Vector4 WithX(this Vector4 vector, float x)
        {
            vector.x = x;
            return vector;
        }

        [Pure]
        public static Vector4 WithY(this Vector4 vector, float y)
        {
            vector.y = y;
            return vector;
        }

        [Pure]
        public static Vector4 WithZ(this Vector4 vector, float z)
        {
            vector.z = z;
            return vector;
        }

        [Pure]
        public static Vector4 WithW(this Vector4 vector, float w)
        {
            vector.w = w;
            return vector;
        }

        [Pure]
        public static Vector2Int WithX(this Vector2Int vector, int x)
        {
            vector.x = x;
            return vector;
        }

        [Pure]
        public static Vector2Int WithY(this Vector2Int vector, int y)
        {
            vector.y = y;
            return vector;
        }

        [Pure]
        public static Vector3Int WithX(this Vector3Int vector, int x)
        {
            vector.x = x;
            return vector;
        }

        [Pure]
        public static Vector3Int WithY(this Vector3Int vector, int y)
        {
            vector.y = y;
            return vector;
        }

        [Pure]
        public static Vector3Int WithZ(this Vector3Int vector, int z)
        {
            vector.z = z;
            return vector;
        }

        [Pure]
        public static Vector2 Clamp(this Vector2 vector, Vector2 min, Vector2 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            return vector;
        }

        [Pure]
        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            vector.z = Mathf.Clamp(vector.z, min.z, max.z);
            return vector;
        }

        [Pure]
        public static Vector4 Clamp(this Vector4 vector, Vector4 min, Vector4 max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            vector.z = Mathf.Clamp(vector.z, min.z, max.z);
            vector.w = Mathf.Clamp(vector.w, min.w, max.w);
            return vector;
        }

        [Pure]
        public static Vector2Int Clamp(this Vector2Int vector, Vector2Int min, Vector2Int max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            return vector;
        }

        [Pure]
        public static Vector3Int Clamp(this Vector3Int vector, Vector3Int min, Vector3Int max)
        {
            vector.x = Mathf.Clamp(vector.x, min.x, max.x);
            vector.y = Mathf.Clamp(vector.y, min.y, max.y);
            vector.z = Mathf.Clamp(vector.z, min.z, max.z);
            return vector;
        }

        [Pure]
        public static bool IsInBounds(this Vector2 vector, Vector2 size)
        {
            return vector.x >= 0
                && vector.x < size.x
                && vector.y >= 0
                && vector.y < size.y;
        }

        [Pure]
        public static bool IsInBounds(this Vector3 vector, Vector3 size)
        {
            return vector.x >= 0
                && vector.x < size.x
                && vector.y >= 0
                && vector.y < size.y
                && vector.z >= 0
                && vector.z < size.z;
        }

        [Pure]
        public static bool IsInBounds(this Vector4 vector, Vector4 size)
        {
            return vector.x >= 0
                && vector.x < size.x
                && vector.y >= 0
                && vector.y < size.y
                && vector.z >= 0
                && vector.z < size.z
                && vector.w >= 0
                && vector.w < size.w;
        }

        [Pure]
        public static bool IsInBounds(this Vector2Int vector, Vector2Int size)
        {
            return vector.x >= 0
                && vector.x < size.x
                && vector.y >= 0
                && vector.y < size.y;
        }

        [Pure]
        public static bool IsInBounds(this Vector3Int vector, Vector3Int size)
        {
            return vector.x >= 0
                && vector.x < size.x
                && vector.y >= 0
                && vector.y < size.y
                && vector.z >= 0
                && vector.z < size.z;
        }
    }
}