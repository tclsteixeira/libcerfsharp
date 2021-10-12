
using System;
using System.Runtime.CompilerServices;

//using _cerf_cmplx_ext = ArisMathLib.ComplexExtensions;
using _cerf_cmplx = System.Numerics.Complex;
using ComplexNumber = System.Numerics.Complex;
//using _cerf_cmplx = ArisMathLib.ComplexNumber;
using System.Numerics;


namespace Libcerfsharp
{

    /// <summary>
    /// Declare soma global definitions used by Libcerf functions.
    /// </summary>
    public static class Defs
    {

        public static double Inf = double.PositiveInfinity;

        public static double fabs(double x)
        {
            return Math.Abs(x);
        }

        public static double sqrt(double x)
        {
            return Math.Sqrt(x);
        }

        /// <summary>
        /// Returns the natural logarithm of x (logarithm of x in base e).
        /// </summary>
        /// <returns>Returns the natural logarithm of x (logarithm of x in base e).</returns>
        /// <param name="x">The x value.</param>
        public static double log(double x)
        {
            return Math.Log(x);     // Returns the natural logarithm of x (logarithm of x in base e).
        }

        public static double exp(double x)
        {
            return Math.Exp(x);
        }

        public static _cerf_cmplx cexp(_cerf_cmplx z)
        {
            return _cerf_cmplx.Exp(z);
        }

        public static double sin(double x)
        {
            return Math.Sin(x);
        }

        public static double cos(double x)
        {
            return Math.Cos(x);
        }

        public static double NaN = double.NaN;

        public static bool isnan(double x)
        {
            return double.IsNaN(x);
        }

        public static double floor(double x)
        {
            return Math.Floor(x);
        }

        public static bool isinf(double x)
        {
            return double.IsInfinity(x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //static inline double sqr(double x) { return x * x; }
        public static double sqr(double x) { return x * x; }

        /// <summary>
        /// Gets real part of the specified complex argument <paramref name="z"/>.
        /// </summary>
        /// <returns>Returns the real part.</returns>
        /// <param name="z">The complex z.</param>
        public static double creal(_cerf_cmplx z)
        {
            return z.Real;
        }

        /// <summary>
        /// Gets imaginary part of the specified complex argument <paramref name="z"/>.
        /// </summary>
        /// <returns>Returns the imaginary part.</returns>
        /// <param name="z">The complex z.</param>
        public static double cimag(_cerf_cmplx z)
        {
            return z.Imaginary;
        }

        public static _cerf_cmplx C(double a1, double b1) { return new _cerf_cmplx(a1, b1); }


        /// <summary>
        /// Returns a value with magnitude of 'x' with sign of 'y'.
        /// </summary>
        /// <returns>The copysign.</returns>
        /// <param name="x">The magnitude to returns.</param>
        /// <param name="y">The sign of returned magnitude value.</param>
        /// <example>
        ///     copysign( 10.0,-1.0) = -10.0
        ///     copysign(-10.0,-1.0) = -10.0
        ///     copysign(-10.0, 1.0) = 10.0
        ///     copysign(-10.0, 0) = 10.0
        /// 
        /// </example>
        public static double copysign(double x, double y)
        {
            if (y < 0) return (-1 * Math.Abs(x));
            if (y >= 0) return Math.Abs(x);

            return Math.Abs(x);
            //if (y == 0) return Math.Abs(x);
        }


        public static double HUGE_VAL()
        {
            return double.PositiveInfinity;
            //throw new OverflowException("Floating-point constant is outside the range of type `double'");
        }

        public static double NEG_HUGE_VAL()
        {
            return double.NegativeInfinity;
            //throw new OverflowException("Floating-point constant is outside the range of type `double'");
        }

    }

}
