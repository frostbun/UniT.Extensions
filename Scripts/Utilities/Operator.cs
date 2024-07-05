#nullable enable
namespace UniT.Extensions
{
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
    }
}