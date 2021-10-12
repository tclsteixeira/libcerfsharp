/* Library libcerf:
 *   Compute complex error functions, based on a new implementation of
 *   Faddeeva's w_of_z. Also provide Dawson and Voigt functions.
 *
 * File widthtest.c:
 *   Test function voigt_hwhm
 *
 * Copyright:
 *   (C) 2021 Forschungszentrum Jülich GmbH
 *
 * Licence:
 *   ../LICENSE
 *
 * Authors:
 *   Joachim Wuttke, Forschungszentrum Jülich, 2021
 *
 * Website:
 *   http://apps.jcns.fz-juelich.de/libcerf
 *
 * Revision history:
 *   ../CHANGELOG
 *
 * More information:
 *   man 3 voigt_hwhm
 */

/* Library Libcerfsharp:
 * 
 *  This is a port to C# from original C library libcerf test tool functions.
 *
 * File widthtest.cs:
 *   Test function voigt_hwhm
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

//# include "cerf.h"
//# include "testtool.h"


using System;
using Libcerfsharp;

namespace LibcerfsharpTestTool
{

    public static class widthtest
    {
        //public widthtest()
        //{
        //}



        private static double sqrt(double x)
        {
            double FResult = Defs.sqrt(x);
            return FResult;
        }

        private static double log(double x)
        {
            double FResult = Defs.log(x);
            return FResult;
        }

        private static double pow(double x, double y)
        {
            double FResult = Math.Pow(x, y);
            return FResult;
        }


        // excellent approximation [Olivero & Longbothum, 1977], used as starting value in voigt_hwhm
        private static double hwhm0(double sigma, double gamma)
        {
            return .5 * (1.06868 * gamma + sqrt(0.86743 * gamma * gamma + 4 * 2 * log(2) * sigma * sigma));
        }

        private static void widtest(ref result_t result, double limit, double sigma, double gamma)
        {
            ++result.Total;
            double expected = sigma * hwhm0(1.0, gamma / sigma);
            double computed = Libcerf.Voigt_hwhm(sigma, gamma);
            double re = TestTool.relerr(computed, expected);
            if (re > limit)
            {
                //printf("failure in subtest %i: sigma=%14.9e, gamma=%14.9e\n", result->total, sigma, gamma);
                //printf("- fct value %20.15g\n", computed);
                //printf("- expected  %20.15g\n", expected);
                //printf("=> error %6.2g above limit %6.2g\n", re, limit);

                Console.WriteLine(string.Format("failure in subtest {0}: sigma={1:e14}, gamma={2:e14}", result.Total, sigma, gamma));
                Console.WriteLine(string.Format("-fct value {0:g20}", computed));
                Console.WriteLine(string.Format("- expected  {0:g20}", expected));
                Console.WriteLine(string.Format("=> error {0:g6} above limit {1:g6}", re, limit));

                ++result.Failed;
            }
        }


        public static int Run()
        {
            result_t result = new result_t(0, 0);   // { 0, 0 };
            const int N = 100;
            const int M = 10000;
            for (int i = 0; i <= N; ++i)
            {
                double sigma = pow(10.0, 180 * (i - N / 2) / (N / 2));
                for (int j = 0; j <= M; ++j)
                {
                    double gamma = sigma * pow(10.0, 17 * (j - M / 2) / (M / 2));

                    try
                    {
                        widtest(ref result, 1e-2, sigma, gamma);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            // printf("%i/%i tsts failed\n", result.failed, result.total);
            Console.WriteLine(string.Format("{0}/{1} tests failed", result.Failed, result.Total));

            return (result.Failed != 0) ? 1 : 0;
        }


    }

}




