/* Library libcerf:
 *   Compute complex error functions, based on a new implementation of
 *   Faddeeva's w_of_z. Also provide Dawson and Voigt functions.
 *
 * File testtool.h
 *   Auxiliary functions and preprocessor macros for numeric tests.
 *
 * Copyright:
 *   (C) 2012 Massachusetts Institute of Technology
 *   (C) 2013 Forschungszentrum Jülich GmbH
 *
 * Licence:
 *   ../LICENSE
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
 */

/* Library Libcerfsharp:
 * 
 *  This is a port to C# from original C library libcerf test tool functions.
 *
 * File TestTool.cs
 *   Auxiliary functions and preprocessor macros for numeric tests.
 *
 * Copyright:
 *   (C) 2021 Tiago Teixeira
 *   
 *
 * Licence:
 *   ../LICENSE
 *
 * Revision history:
 *   ../CHANGELOG
 */


using System;
using Libcerfsharp;
using _cerf_cmplx = System.Numerics.Complex;
using System.Text;

namespace LibcerfsharpTestTool
{

    public struct result_t
    {
        private int mfailed;
        private int mtotal;

        public result_t(int _failed, int _total)
        {
            this.mfailed = _failed;
            this.mtotal = _total;
        }


        public int Failed
        {
            get { return this.mfailed; }
            set { this.mfailed = value; }
        }

        public int Total
        {
            get { return this.mtotal; }
            set { this.mtotal = value; }
        }
    }


    /// <summary>
    /// Test tool to compare results accuracy with Maple results.
    /// </summary>
    public static class TestTool
    {
        //public TestTool()
        //{
        //}

        /// <summary>
        /// Isfinite returns a non-zero value (true) if number is not an infinity and not a NaN,
        /// otherwise returns zero.
        /// </summary>
        /// <returns>
        /// Returns a non-zero value (true) if number is not an infinity and not a NaN,
        /// otherwise returns zero.
        /// </returns>
        /// <param name="a">The floating point value to test.</param>
        private static bool isfinite(double a)
        {
            if ((double.IsInfinity(a)) || (double.IsNaN(a)))
                return false;
            else
                return true;
        }


        private static void assert(bool boolCondition)
        {
            System.Diagnostics.Debug.Assert(boolCondition);
        }


        // Compute relative error |b-a|/|a|, handling case of NaN and Inf,
        public static double relerr(double a, double b)
        {
            if (!isfinite(a))
                return !isfinite(b) ? 0 : Defs.Inf;
            if (!isfinite(b))
            {
                assert(isfinite(a)); // implied by the above
                return Defs.Inf;
            }
            if (a == 0)
                return b == 0 ? 0 : Defs.Inf;
            return Defs.fabs((b - a) / a);
        }



        // Test whether complex numbers 'computed' and 'expected' agree within relative error bound 'limit'
        public static void ZTest(
                ref result_t result, double limit, _cerf_cmplx computed, _cerf_cmplx expected, string name)
        {
            ++result.Total;
            double re_r = relerr(Defs.creal(computed), Defs.creal(expected));
            double re_i = relerr(Defs.cimag(computed), Defs.cimag(expected));

            if (re_r > limit || re_i > limit) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("failure in subtest {0}: {1}", result.Total, name));
                sb.AppendLine(string.Format("- fct value {0:g20}+{1:g20}", Defs.creal(computed), Defs.cimag(computed)));
                sb.AppendLine(string.Format("- expected  {0:g20}+{1:g20}", Defs.creal(expected), Defs.cimag(expected)));
                sb.AppendLine(string.Format("=> error {0:g6} or {1:g6} above limit {2:g6}", re_r, re_i, limit));

                //printf("failure in subtest %i: %s\n", result->total, name);
                //printf("- fct value %20.15g%+20.15g\n", Defs.creal(computed), Defs.cimag(computed));
                //printf("- expected  %20.15g%+20.15g\n", Defs.creal(expected), Defs.cimag(expected));
                //printf("=> error %6.2g or %6.2g above limit %6.2g\n", re_r, re_i, limit);
                ++result.Failed;

                Console.WriteLine(sb.ToString());
            }
        }


        // Test whether real numbers 'computed' and 'expected' agree within relative error bound 'limit'
        public static void RTest(ref result_t result, double limit, double computed, double expected, string name)
        {
            ++result.Total;
            double re = relerr(computed, expected);
            if (re > limit) {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("failure in subtest {0}: {1}", result.Total, name));
                sb.AppendLine(string.Format("- fct value {0:g20}", computed));
                sb.AppendLine(string.Format("- expected  {0:g20}", expected));
                sb.AppendLine(string.Format("=> error {0:g6} above limit {1:g6}", re, limit));

                //printf("failure in subtest %i: %s\n", result->total, name);
                //printf("- fct value %20.15g\n", computed);
                //printf("- expected  %20.15g\n", expected);
                //printf("=> error %6.2g above limit %6.2g\n", re, limit);

                ++result.Failed;

                Console.WriteLine(sb.ToString());
            }
        }


    }

}
