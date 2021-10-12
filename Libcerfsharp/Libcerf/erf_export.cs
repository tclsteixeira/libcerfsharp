/* Library libcerf v1.14 2020:
 *   Compute complex error functions, based on a new implementation of
 *   Faddeeva's w_of_z. Also provide Dawson and Voigt functions.
 *
 * File cerf.h:
 *   Declare exported functions.
 *
 * Copyright:
 *   (C) 2012 Massachusetts Institute of Technology
 *   (C) 2013 Forschungszentrum Jülich GmbH
 *
 * Licence:
 *   Permission is hereby granted, free of charge, to any person obtaining
 *   a copy of this software and associated documentation files (the
 *   "Software"), to deal in the Software without restriction, including
 *   without limitation the rights to use, copy, modify, merge, publish,
 *   distribute, sublicense, and/or sell copies of the Software, and to
 *   permit persons to whom the Software is furnished to do so, subject to
 *   the following conditions:
 *
 *   The above copyright notice and this permission notice shall be
 *   included in all copies or substantial portions of the Software.
 *
 *   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 *   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 *   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 *   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
 *   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
 *   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
 *   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 *
 * Authors:
 *   Steven G. Johnson, Massachusetts Institute of Technology, 2012, core author
 *   Joachim Wuttke, Forschungszentrum Jülich, 2013, package maintainer
 *
 * Website:
 *   http://apps.jcns.fz-juelich.de/libcerf
 *
 * Revision history:
 *   ../CHANGELOG
 *
 * Man pages:
 *   w_of_z(3), dawson(3), voigt(3), cerf(3), erfcx(3), erfi(3)
 */


using System;
using _cerf_cmplx = System.Numerics.Complex;
//using _cerf_cmplx = ArisMathLib.ComplexNumber;

namespace Libcerfsharp
{

    /// <summary>
    /// Declare exported functions (public Libcerf error functions).
    /// </summary>
    public static partial class Libcerf
    {

        /// <summary>
        /// Faddeeva's scaled complex error function.
        /// </summary>
        /// <remarks>Compute w(z) = exp(-z^2) * erfc(-iz)</remarks>
        /// <returns>Returns result.</returns>
        /// <param name="z">The Faddeeva's function argument.</param>
        public static _cerf_cmplx W_of_z(_cerf_cmplx z)
        {
            return w_of_z(z);
        }


        /// <summary>
        /// Faddeeva's scaled complex error function (Gnuplot faddeeva function).
        /// </summary>
        /// <remarks>Compute w(z) = exp(-z^2) * erfc(-iz)</remarks>
        /// <returns>Returns result.</returns>
        /// <param name="z">The Faddeeva's function argument.</param>
        public static _cerf_cmplx Faddeeva(_cerf_cmplx z)
        {
            return w_of_z(z);
        }


        /// <summary>
        /// Voigt/Faddeeva function y/π*∫( (exp(−t**2))/((x−t)**2+y**2) )dt
        /// Note:  voigt(x,y) = real(faddeeva(x+iy))
        /// Faddeeva's scaled complex error function (Gnuplot faddeeva function).
        /// </summary>
        /// <remarks>
        ///     Note: voigt(x,y) = real(faddeeva(x+iy)
        /// 
        ///     This is the Gnuplot voigt(x, y) function
        /// </remarks>
        /// <returns>Returns result.</returns>
        /// <param name="X">The complex number real part function argument.</param>
        /// <param name="Y">The complex number imaginary part function argument.</param>
        public static double Voigt(double X, double Y)
        {
            return Defs.creal( Faddeeva( Defs.C(X, Y) ) );
        }


        /// <summary>
        /// Compute scaled Dawson integral im_w_of_x(x) = 2*dawson(x)/sqrt(pi),
        /// equivalent to the imaginary part of the Faddeeva function w(x) for real x.
        /// </summary>
        /// <remarks>Special case Im[w(x)] of real x</remarks>
        /// <returns>Returns result.</returns>
        /// <param name="x">The function argument.</param>
        public static double Im_w_of_x(double x) 
        {
            return im_w_of_x(x);
        }


        /// <summary>
        /// Computes the real part of result of Faddeeva's scaled complex error function.
        /// </summary>
        /// <returns>Returns the real part of result of Faddeeva's scaled complex error function.</returns>
        /// <param name="x">The x argument (real part of complex number).</param>
        /// <param name="y">The y argument (imaginary part of complex number).</param>
        public static double Re_w_of_z(double x, double y)
        {
            return re_w_of_z(x, y);
        }


        /// <summary>
        /// Computes the imaginary part of result of Faddeeva's scaled complex error function.
        /// </summary>
        /// <returns>Returns the imaginary part of result of Faddeeva's scaled complex error function.</returns>
        /// <param name="x">The x argument (real part of complex number).</param>
        /// <param name="y">The y argument (imaginary part of complex number).</param>
        public static double Im_w_of_z(double x, double y)
        {
            return im_w_of_z(x, y);
        }


        /// <summary>
        /// Computes the complex error function (the error function of complex arguments).
        /// </summary>
        /// <returns>Returns function result.</returns>
        /// <param name="z">The z argument (complex number).</param>
        /// <remarks>
        /// Steven G. Johnson, October 2012.
        /// Compute erf(z), the complex error function,
        /// using w_of_z except for certain regions.
        /// </remarks>
        public static _cerf_cmplx CErf(_cerf_cmplx z)
        {
            return cerf(z);
        }


        /// <summary>
        /// Compute the complex complementary error function,
        /// using w_of_z (Faddeeva's scaled complex error function) except for certain regions.
        /// </summary>
        /// <returns>Returns function result.</returns>
        /// <param name="z">The z argument (complex number).</param>
        /// <remarks>
        /// Steven G. Johnson, October 2012.
        ///
        /// Compute erfc(z) = 1 - erf(z), the complex complementary error function,
        /// using w_of_z except for certain regions.
        /// </remarks>
        public static _cerf_cmplx CErfc(_cerf_cmplx z)
        {
            return cerfc(z);
        }


        // compute erfcx(z) = exp(z^2) erfc(z), an underflow-compensated version of erfc


        /// <summary>
        /// Computes the complex underflow-compensated complementary error function at aspecified z,
        /// trivially related to Faddeeva's w_of_z.
        /// </summary>
        /// <remarks>
        /// Compute erfcx(z) = exp(z^2) erfc(z),
        /// the complex underflow-compensated complementary error function,
        /// trivially related to Faddeeva's w_of_z.
        /// </remarks>
        /// <returns>The cerfcx.</returns>
        /// <param name="z">The z coordinate.</param>
        public static _cerf_cmplx CErfcx(_cerf_cmplx z)
        {
            return cerfcx(z);
        }


        /// <summary>
        /// Compute erfcx(x) = exp(x^2) erfc(x) function, for real x,
        /// using a novel algorithm that is much faster than DERFC of SLATEC.
        /// </summary>
        /// <remarks>
        /// This function is used in the computation of Faddeeva, Dawson, and
        /// other complex error functions.
        /// </remarks>
        /// <returns>Returns the result.</returns>
        /// <param name="x">The x (real) argument.</param>
        public static double Erfcx(double x) // special case for real x
        {
            return erfcx(x);
        }


        // compute erfi(z) = -i erf(iz), the imaginary error function

        /// <summary>
        /// Computes the rotated complex error function at a specified z.
        /// </summary>
        /// <remarks>
        /// Compute erfi(z) = -i erf(iz), the rotated complex error function.
        /// </remarks>
        /// <returns>Returns the result.</returns>
        /// <param name="z">The z argument.</param>
        public static _cerf_cmplx CErfi(_cerf_cmplx z)
        {
            return cerfi(z);
        }


        /// <summary>
        /// Computes the imaginary error function for a specific real argument x.
        /// </summary>
        /// <remarks>
        /// Compute erfi(x) = -i * erf(ix).
        /// </remarks>
        /// <returns>Returns the result.</returns>
        /// <param name="x">The x (real) argument.</param>
        public static double Erfi(double x) // special case for real x
        {
            return erfi(x);
        }


        /// <summary>
        /// Dawson's integral for a complex argument, using w_of_z 
        /// (Faddeeva's scaled complex error function) except for certain regions.
        /// </summary>
        /// <remarks>
        /// Steven G. Johnson, October 2012.
        ///
        /// Compute Dawson(z) = sqrt(pi)/2  *  exp(-z^2) * erfi(z),
        /// Dawson's integral for a complex argument,
        /// using w_of_z except for certain regions.
        /// </remarks>
        /// <returns>Returns the result (complex number).</returns>
        /// <param name="z">The z argument (complex number).</param>
        public static _cerf_cmplx CDawson(_cerf_cmplx z)
        {
            return cdawson(z);
        }


        /// <summary>
        /// Computes the Dawson's integral for a real argument.
        /// </summary>
        /// <remarks>
        /// Compute dawson(x) = sqrt(pi)/2 * exp(-x^2) * erfi(x)
        /// </remarks>
        /// <returns>Returns the result.</returns>
        /// <param name="x">The x (real) argument.</param>
        public static double Dawson(double x) // special case for real x
        {
            return dawson(x);
        }


        /// <summary>
        /// Compute Voigt's convolution of a Gaussian and a Lorentzian
        /// G(x,sigma) = 1/sqrt(2*pi)/|sigma| * exp(-x^2/2/sigma^2)
        /// and a Lorentzian L(x,gamma) = |gamma| / pi / ( x^2 + gamma^2 ).
        /// </summary>
        /// <returns>Returns the result.</returns>
        /// <param name="x">The x value.</param>
        /// <param name="sigma">Argument for Gaussian function.</param>
        /// <param name="gamma">Argument for Lorentzian function.</param>
        /// <remarks>
        /// 
        ///  Joachim Wuttke, January 2013.
        /// 
        /// Compute Voigt's convolution of a Gaussian
        ///    G(x,sigma) = 1/sqrt(2*pi)/|sigma| * exp(-x^2/2/sigma^2)
        /// and a Lorentzian
        ///    L(x,gamma) = |gamma| / pi / ( x^2 + gamma^2 ),
        /// namely
        ///    voigt(x,sigma,gamma) =
        ///          \int_{-infty}^{infty} dx' G(x',sigma) L(x-x',gamma)
        /// using the relation
        ///    voigt(x,sigma,gamma) = Re{ w(z) } / sqrt(2*pi) / |sigma|
        /// with
        ///    z = (x+i*|gamma|) / sqrt(2) / |sigma|.
        ///
        /// Reference: Abramowitz&Stegun (1964), formula (7.4.13).
        ///
        /// </remarks>
        public static double Voigt(double x, double sigma, double gamma)
        {
            return voigt(x, sigma, gamma);
        }


        /// <summary>
        /// Voight profile (Gnuplot VP function).
        /// </summary>
        /// <returns>Returns the result.</returns>
        /// <param name="x">The x value.</param>
        /// <param name="sigma">Argument for Gaussian function.</param>
        /// <param name="gamma">Argument for Lorentzian function.</param>
        public static double VP(double x, double sigma, double gamma)
        {
            return voigt(x, sigma, gamma);
        }


        /// <summary>
        /// Compute the full width at half maximum of the Voigt function.
        /// </summary>
        /// <remarks>
        /// Computate voigt_hwhm, using Newton's iteration.
        /// </remarks>
        /// <returns>Returns the result.</returns>
        /// <param name="sigma">The Gaussian Sigma.</param>
        /// <param name="gamma">The Lorentzian Gamma.</param>
        public static double Voigt_hwhm(double sigma, double gamma)
        {
            return voigt_hwhm(sigma, gamma);
        }

        //// EXPERIMENTAL
        //public static double cerf_experimental_imw(double x, double y);
        //public static double cerf_experimental_rew(double x, double y);


    }


}
