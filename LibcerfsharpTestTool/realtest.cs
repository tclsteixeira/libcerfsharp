/* Library libcerf:
 *   Compute complex error functions, based on a new implementation of
 *   Faddeeva's w_of_z. Also provide Dawson and Voigt functions.
 *
 * File realtest.c
 *   Compare complex and real function for various real arguments
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
 * File realtest.cs
 *   Compare complex and real function for various real arguments.
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


namespace LibcerfsharpTestTool
{


    public static class realtest
    {
        //public realtest()
        //{
        //}


        private const double errBound = 1e-13;


        #region auxiliary functions

        private static _cerf_cmplx cerf(_cerf_cmplx z)
        {
            _cerf_cmplx FResult = Libcerf.CErf(z);
            return FResult;
        }

        private static double erf(double x)
        {
            double FResult = Libcerf.Erf(x);
            return FResult;
        }


        private static _cerf_cmplx cerfi(_cerf_cmplx z)
        {
            _cerf_cmplx FResult = Libcerf.CErfi(z);
            return FResult;
        }

        private static double erfi(double x)
        {
            double FResult = Libcerf.Erfi(x);
            return FResult;
        }


        private static _cerf_cmplx cerfc(_cerf_cmplx z)
        {
            _cerf_cmplx FResult = Libcerf.CErfc(z);
            return FResult;
        }

        private static double erfc(double x)
        {
            double FResult = Libcerf.Erfc(x);
            return FResult;
        }


        private static _cerf_cmplx cerfcx(_cerf_cmplx z)
        {
            _cerf_cmplx FResult = Libcerf.CErfcx(z);
            return FResult;
        }

        private static double erfcx(double x)
        {
            double FResult = Libcerf.Erfcx(x);
            return FResult;
        }


        private static _cerf_cmplx cdawson(_cerf_cmplx z)
        {
            _cerf_cmplx FResult = Libcerf.CDawson(z);
            return FResult;
        }

        private static double dawson(double x)
        {
            double FResult = Libcerf.Dawson(x);
            return FResult;
        }


        #endregion auxiliary functions



        //ref result_t result, string name, Func<_cerf_cmplx, _cerf_cmplx> (* F)(_cerf_cmplx), double (* FRE) (double),


        // For testing the Dawson and error functions for the special case of a real argument
        private static void xTest(
            ref result_t result, string name, Func<_cerf_cmplx, _cerf_cmplx> F, Func<double, double> FRE,
            double isc, double xmin, double xmax)
        {
            //char info[30];
            const int n = 10000;
            string info = string.Empty;

            for (int i = 0; i<n; ++i)
            {
                double x = Math.Pow(10.0, -300.0 + i * 600.0 / (n - 1));
                if (x<xmin || x> xmax)
                    continue;

                info = string.Format("{0}({1})", name, x);
                info = info.Substring(0, Math.Min(30, info.Length));
                // snprintf(info, 30, "%s(%g)", name, x);

                TestTool.RTest(ref result, 1e-13, Defs.creal(F(Defs.C(x, x* isc))), FRE(x), info);
            }
        }


        // For testing the Dawson and error functions for the special case of an infinite argument
        private static void iTest(ref result_t result, string name, Func<_cerf_cmplx, _cerf_cmplx> F, Func<double, double> FRE)
        {
            string info = string.Empty;

            //char info[30];

            //snprintf(info, 30, "%s(Inf)", name);
            info = string.Format("{0}(Inf)", name);
            info = info.Substring(0, Math.Min(30, info.Length));
            TestTool.RTest(ref result, 1e-13, FRE(Defs.Inf), Defs.creal(F(Defs.C(Defs.Inf, 0.0))), info);

            //snprintf(info, 30, "%s(-Inf)", name);
            info = string.Format("{0}(-Inf)", name);
            info = info.Substring(0, Math.Min(30, info.Length));
            TestTool.RTest(ref result, 1e-13, FRE(-Defs.Inf), Defs.creal(F(Defs.C(-Defs.Inf, 0.0))), info);

            //snprintf(info, 30, "%s(NaN)", name);
            info = string.Format("{0}(NaN)", name);
            info = info.Substring(0, Math.Min(30, info.Length));
            TestTool.RTest(ref result, 1e-13, FRE(Defs.NaN), Defs.creal(F(Defs.C(Defs.NaN, 0.0))), info);
        }

        public static int Run()
        {
            result_t result = new result_t(0, 0);   // { 0, 0 };

            xTest(ref result, "erf", cerf, erf, 1e-20, 1e-300, 1e300);
            iTest(ref result, "erf", cerf, erf);

            xTest(ref result, "erfi", cerfi, erfi, 0, 1e-300, 1e300);
            iTest(ref result, "erfi", cerfi, erfi);

            xTest(ref result, "erfc", cerfc, erfc, 1e-20, 1e-300, 1e300);
            iTest(ref result, "erfc", cerfc, erfc);

            xTest(ref result, "erfcx", cerfcx, erfcx, 0, 1e-300, 1e300);
            // iTest(&result, "erfcx", cerfcx, erfcx);

            xTest(ref result, "dawson", cdawson, dawson, 1e-20, 1e-300, 1e150);
            iTest(ref result, "dawson", cdawson, dawson);

            // printf("%i/%i tests failed", result.failed, result.total);
            Console.WriteLine(string.Format("{0}/{1} tests failed", result.Failed, result.Total));

            return result.Failed;
        }


    }

}


//#include "cerf.h"
//#include "testtool.h"

