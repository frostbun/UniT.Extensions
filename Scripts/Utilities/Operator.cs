#nullable enable
namespace UniT.Extensions
{
    public static class Operator
    {
        #region int

        public static int Sum(int a, int b) => a + b;

        public static int Subtract(int a, int b) => a - b;

        public static int Multiply(int a, int b) => a * b;

        public static int Divide(int a, int b) => a / b;

        public static int Modulo(int a, int b) => a % b;

        public static int And(int a, int b) => a & b;

        public static int Or(int a, int b) => a | b;

        public static int Xor(int a, int b) => a ^ b;

        #endregion

        #region long

        public static long Sum(long a, long b) => a + b;

        public static long Subtract(long a, long b) => a - b;

        public static long Multiply(long a, long b) => a * b;

        public static long Divide(long a, long b) => a / b;

        public static long Modulo(long a, long b) => a % b;

        public static long And(long a, long b) => a & b;

        public static long Or(long a, long b) => a | b;

        public static long Xor(long a, long b) => a ^ b;

        #endregion

        #region uint

        public static uint Sum(uint a, uint b) => a + b;

        public static uint Subtract(uint a, uint b) => a - b;

        public static uint Multiply(uint a, uint b) => a * b;

        public static uint Divide(uint a, uint b) => a / b;

        public static uint Modulo(uint a, uint b) => a % b;

        public static uint And(uint a, uint b) => a & b;

        public static uint Or(uint a, uint b) => a | b;

        public static uint Xor(uint a, uint b) => a ^ b;

        #endregion

        #region ulong

        public static ulong Sum(ulong a, ulong b) => a + b;

        public static ulong Subtract(ulong a, ulong b) => a - b;

        public static ulong Multiply(ulong a, ulong b) => a * b;

        public static ulong Divide(ulong a, ulong b) => a / b;

        public static ulong Modulo(ulong a, ulong b) => a % b;

        public static ulong And(ulong a, ulong b) => a & b;

        public static ulong Or(ulong a, ulong b) => a | b;

        public static ulong Xor(ulong a, ulong b) => a ^ b;

        #endregion

        #region float

        public static float Sum(float a, float b) => a + b;

        public static float Subtract(float a, float b) => a - b;

        public static float Multiply(float a, float b) => a * b;

        public static float Divide(float a, float b) => a / b;

        public static float Modulo(float a, float b) => a % b;

        #endregion

        #region double

        public static double Sum(double a, double b) => a + b;

        public static double Subtract(double a, double b) => a - b;

        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double a, double b) => a / b;

        public static double Modulo(double a, double b) => a % b;

        #endregion

        #region decimal

        public static decimal Sum(decimal a, decimal b) => a + b;

        public static decimal Subtract(decimal a, decimal b) => a - b;

        public static decimal Multiply(decimal a, decimal b) => a * b;

        public static decimal Divide(decimal a, decimal b) => a / b;

        public static decimal Modulo(decimal a, decimal b) => a % b;

        #endregion

        #region bool

        public static bool And(bool a, bool b) => a & b;

        public static bool Or(bool a, bool b) => a | b;

        public static bool Xor(bool a, bool b) => a ^ b;

        #endregion
    }
}