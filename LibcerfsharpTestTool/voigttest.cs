/* Library libcerf:
 * Compute complex error functions, based on a new implementation of
 * Faddeeva's w_of_z. Also provide Dawson and Voigt functions.
 *
 * File voigttest.cs:
 * Test the Voigt function
 *
 * Copyright:
 * (C) 2013 Forschungszentrum Jülich GmbH
 *
 * Licence:
 *   ../LICENSE
 *
 * Authors:
 * Joachim Wuttke, Forschungszentrum Jülich, 2013
 *
 * Website:
 * http://apps.jcns.fz-juelich.de/libcerf
 *
 * Revision history:
 *   ../CHANGELOG
 *
 * More information:
 * man 3 voigt
 */

/* Library Libcerfsharp:
* 
*  This is a port to C# from original C library libcerf test tool functions.
*
* File voigttest.cs
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


//#include "cerf.h"
//#include "testtool.h"
//#include <math.h>
//#include <stdio.h>


using System;
using Libcerfsharp;
using _cerf_cmplx = System.Numerics.Complex;

namespace LibcerfsharpTestTool
{

    /// <summary>
    /// Test class for Voigt function.
    /// </summary>
    public static class voigttest
    {
        //public voigttest()
        //{
        //}


        #region auxiliary methods

        private static double voigt(double x, double sigma, double gamma)
        {
            double FResult = Libcerf.Voigt(x, sigma, gamma);
            return FResult;
        }

        private static double sqrt(double x)
        {
            double FResult = Defs.sqrt(x);
            return FResult;
        }


        #endregion auxiliary methods



        public static int Run()
        {
            string fname = nameof(voigt);
            result_t result = new result_t(0, 0);

            // expected results analytically determined:
            TestTool.RTest(ref result, 1e-15, voigt(0, 1, 0), 1 / sqrt(6.283185307179586), fname);
            TestTool.RTest(ref result, 1e-15, voigt(0, 0, 1), 1 / 3.141592653589793, fname);
            TestTool.RTest(ref result, 1e-13, voigt(0, .5, .5), .41741856104074, fname);

            // expected results obtained from scipy.integrate:
            TestTool.RTest(ref result, 1e-12, voigt(1, .5, .5), .18143039885260323, fname);
            TestTool.RTest(ref result, 1e-12, voigt(1e5, .5e5, .5e5), .18143039885260323e-5, fname);
            TestTool.RTest(ref result, 1e-12, voigt(1e-5, .5e-5, .5e-5), .18143039885260323e5, fname);
            TestTool.RTest(ref result, 1e-12, voigt(1, .2, 5), 0.06113399719916219, fname);
            TestTool.RTest(ref result, 1e-12, voigt(1, 5, .2), 0.07582140674553575, fname);

            Console.WriteLine(string.Format("{0}/{1} tests failed", result.Failed, result.Total));

            //printf("%i/%i tests failed", result.failed, result.total);

            return ((result.Failed == 0) ? 1 : 0);
        }


    }

}

