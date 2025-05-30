#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class Operator
    {
        #region int

        public static int Add(int a, int b) => a + b;

        public static int Subtract(int a, int b) => a - b;

        public static int Multiply(int a, int b) => a * b;

        public static int Divide(int a, int b) => a / b;

        public static int Modulo(int a, int b) => a % b;

        public static int And(int a, int b) => a & b;

        public static int Or(int a, int b) => a | b;

        public static int Xor(int a, int b) => a ^ b;

        public static int Min(int a, int b) => a < b ? a : b;

        public static int Max(int a, int b) => a > b ? a : b;

        public static int Difference(int a, int b) => a > b ? a - b : b - a;

        #endregion

        #region long

        public static long Add(long a, long b) => a + b;

        public static long Subtract(long a, long b) => a - b;

        public static long Multiply(long a, long b) => a * b;

        public static long Divide(long a, long b) => a / b;

        public static long Modulo(long a, long b) => a % b;

        public static long And(long a, long b) => a & b;

        public static long Or(long a, long b) => a | b;

        public static long Xor(long a, long b) => a ^ b;

        public static long Min(long a, long b) => a < b ? a : b;

        public static long Max(long a, long b) => a > b ? a : b;

        public static long Difference(long a, long b) => a > b ? a - b : b - a;

        #endregion

        #region uint

        public static uint Add(uint a, uint b) => a + b;

        public static uint Subtract(uint a, uint b) => a - b;

        public static uint Multiply(uint a, uint b) => a * b;

        public static uint Divide(uint a, uint b) => a / b;

        public static uint Modulo(uint a, uint b) => a % b;

        public static uint And(uint a, uint b) => a & b;

        public static uint Or(uint a, uint b) => a | b;

        public static uint Xor(uint a, uint b) => a ^ b;

        public static uint Min(uint a, uint b) => a < b ? a : b;

        public static uint Max(uint a, uint b) => a > b ? a : b;

        public static uint Difference(uint a, uint b) => a > b ? a - b : b - a;

        #endregion

        #region ulong

        public static ulong Add(ulong a, ulong b) => a + b;

        public static ulong Subtract(ulong a, ulong b) => a - b;

        public static ulong Multiply(ulong a, ulong b) => a * b;

        public static ulong Divide(ulong a, ulong b) => a / b;

        public static ulong Modulo(ulong a, ulong b) => a % b;

        public static ulong And(ulong a, ulong b) => a & b;

        public static ulong Or(ulong a, ulong b) => a | b;

        public static ulong Xor(ulong a, ulong b) => a ^ b;

        public static ulong Min(ulong a, ulong b) => a < b ? a : b;

        public static ulong Max(ulong a, ulong b) => a > b ? a : b;

        public static ulong Difference(ulong a, ulong b) => a > b ? a - b : b - a;

        #endregion

        #region float

        public static float Add(float a, float b) => a + b;

        public static float Subtract(float a, float b) => a - b;

        public static float Multiply(float a, float b) => a * b;

        public static float Divide(float a, float b) => a / b;

        public static float Modulo(float a, float b) => a % b;

        public static float Min(float a, float b) => a < b ? a : b;

        public static float Max(float a, float b) => a > b ? a : b;

        public static float Difference(float a, float b) => a > b ? a - b : b - a;

        #endregion

        #region double

        public static double Add(double a, double b) => a + b;

        public static double Subtract(double a, double b) => a - b;

        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b) => a / b;

        public static double Modulo(double a, double b) => a % b;

        public static double Min(double a, double b) => a < b ? a : b;

        public static double Max(double a, double b) => a > b ? a : b;

        public static double Difference(double a, double b) => a > b ? a - b : b - a;

        #endregion

        #region decimal

        public static decimal Add(decimal a, decimal b) => a + b;

        public static decimal Subtract(decimal a, decimal b) => a - b;

        public static decimal Multiply(decimal a, decimal b) => a * b;

        public static decimal Divide(decimal a, decimal b) => a / b;

        public static decimal Modulo(decimal a, decimal b) => a % b;

        public static decimal Min(decimal a, decimal b) => a < b ? a : b;

        public static decimal Max(decimal a, decimal b) => a > b ? a : b;

        public static decimal Difference(decimal a, decimal b) => a > b ? a - b : b - a;

        #endregion

        #region bool

        public static bool And(bool a, bool b) => a & b;

        public static bool Or(bool a, bool b) => a | b;

        public static bool Xor(bool a, bool b) => a ^ b;

        #endregion

        #region Vector2

        public static Vector2 Add(Vector2 a, Vector2 b) => a + b;

        public static Vector2 Subtract(Vector2 a, Vector2 b) => a - b;

        public static Vector2 Multiply(Vector2 a, Vector2 b) => Vector2.Scale(a, b);

        public static Vector2 Divide(Vector2 a, Vector2 b) => new Vector2(a.x / b.x, a.y / b.y);

        public static Vector2 Min(Vector2 a, Vector2 b) => Vector2.Min(a, b);

        public static Vector2 Max(Vector2 a, Vector2 b) => Vector2.Max(a, b);

        public static Vector2 Difference(Vector2 a, Vector2 b) => new Vector2(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y));

        #endregion

        #region Vector3

        public static Vector3 Add(Vector3 a, Vector3 b) => a + b;

        public static Vector3 Subtract(Vector3 a, Vector3 b) => a - b;

        public static Vector3 Multiply(Vector3 a, Vector3 b) => Vector3.Scale(a, b);

        public static Vector3 Divide(Vector3 a, Vector3 b) => new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);

        public static Vector3 Min(Vector3 a, Vector3 b) => Vector3.Min(a, b);

        public static Vector3 Max(Vector3 a, Vector3 b) => Vector3.Max(a, b);

        public static Vector3 Difference(Vector3 a, Vector3 b) => new Vector3(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z));

        #endregion

        #region Vector4

        public static Vector4 Add(Vector4 a, Vector4 b) => a + b;

        public static Vector4 Subtract(Vector4 a, Vector4 b) => a - b;

        public static Vector4 Multiply(Vector4 a, Vector4 b) => Vector4.Scale(a, b);

        public static Vector4 Divide(Vector4 a, Vector4 b) => new Vector4(a.x / b.x, a.y / b.y, a.z / b.z);

        public static Vector4 Min(Vector4 a, Vector4 b) => Vector4.Min(a, b);

        public static Vector4 Max(Vector4 a, Vector4 b) => Vector4.Max(a, b);

        public static Vector4 Difference(Vector4 a, Vector4 b) => new Vector4(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z), Mathf.Abs(a.w - b.w));

        #endregion

        #region Vector2Int

        public static Vector2Int Add(Vector2Int a, Vector2Int b) => a + b;

        public static Vector2Int Subtract(Vector2Int a, Vector2Int b) => a - b;

        public static Vector2Int Multiply(Vector2Int a, Vector2Int b) => Vector2Int.Scale(a, b);

        public static Vector2Int Divide(Vector2Int a, Vector2Int b) => new Vector2Int(a.x / b.x, a.y / b.y);

        public static Vector2Int Min(Vector2Int a, Vector2Int b) => Vector2Int.Min(a, b);

        public static Vector2Int Max(Vector2Int a, Vector2Int b) => Vector2Int.Max(a, b);

        public static Vector2Int Difference(Vector2Int a, Vector2Int b) => new Vector2Int(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y));

        #endregion

        #region Vector3Int

        public static Vector3Int Add(Vector3Int a, Vector3Int b) => a + b;

        public static Vector3Int Subtract(Vector3Int a, Vector3Int b) => a - b;

        public static Vector3Int Multiply(Vector3Int a, Vector3Int b) => Vector3Int.Scale(a, b);

        public static Vector3Int Divide(Vector3Int a, Vector3Int b) => new Vector3Int(a.x / b.x, a.y / b.y);

        public static Vector3Int Min(Vector3Int a, Vector3Int b) => Vector3Int.Min(a, b);

        public static Vector3Int Max(Vector3Int a, Vector3Int b) => Vector3Int.Max(a, b);

        public static Vector3Int Difference(Vector3Int a, Vector3Int b) => new Vector3Int(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y));

        #endregion
    }
}