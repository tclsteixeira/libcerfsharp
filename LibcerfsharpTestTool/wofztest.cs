﻿/* Library libcerf:
 *   Compute complex error functions, based on a new implementation of
 *   Faddeeva's w_of_z. Also provide Dawson and Voigt functions.
 *
 * File wofztest.c
 *   Test Faddeeva's complex scaled error function,
 *      w(z) = exp(-z^2) * erfc(-i*z) .
 *   
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
* This is a port to C# from original C library libcerf test tool functions.
*
* File wofztest.cs
*    Test Faddeeva's complex scaled error function,
*       w(z) = exp(-z^2) * erfc(-i*z) .
*   
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

//#include <float.h>
//#include <math.h>
//#include <stdio.h>


using System;
using Libcerfsharp;
using _cerf_cmplx = System.Numerics.Complex;

namespace LibcerfsharpTestTool
{

    /// <summary>
    /// Tool class to test Faddeeva's w_of_z function.
    /// </summary>
    public static class wofztest
    {
        //public wofztest()
        //{
        //}


        // accuracy to 13 decimal places are expected
        private const double errBound = 1e-13;


        private static _cerf_cmplx w_of_z(_cerf_cmplx z)
        {
            _cerf_cmplx FResult = Libcerf.W_of_z(z);
            return FResult;
        }

        private static _cerf_cmplx C(double a1, double b1)
        {
            _cerf_cmplx FResult = Defs.C(a1, b1);
            return FResult;
        }

        private static double Inf = Defs.Inf;
        private static double NaN = Defs.NaN;


        /// <summary>
        /// Main function to run tests.
        /// </summary>
        /// <returns>Returns the number of failed results.</returns>
        public static int Run()
        {
            string C_FUNC_NAME = nameof(w_of_z);
            result_t result = new result_t(0, 0);
            /* w(z), computed with WolframAlpha
                             ... note that WolframAlpha is problematic
                             some of the above inputs, so I had to
                             use the continued-fraction expansion
                             in WolframAlpha in some cases, or switch
                             to Maple */
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(624.2, -0.26123)),
                C(-3.78270245518980507452677445620103199303131110e-7,
                  0.000903861276433172057331093754199933411710053155), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-0.4, 3.0)),
                C(0.1764906227004816847297495349730234591778719532788,
                  -0.02146550539468457616788719893991501311573031095617), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(0.6, 2.0)),
                C(0.2410250715772692146133539023007113781272362309451,
                  0.06087579663428089745895459735240964093522265589350), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-1.0, 1.0)),
                C(0.30474420525691259245713884106959496013413834051768,
                  -0.20821893820283162728743734725471561394145872072738), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-1.0, -9.0)),
                C(7.317131068972378096865595229600561710140617977e34,
                  8.321873499714402777186848353320412813066170427e34), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-1.0, 9.0)),
                C(0.0615698507236323685519612934241429530190806818395,
                  -0.00676005783716575013073036218018565206070072304635), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-0.0000000234545, 1.1234)),
                C(0.3960793007699874918961319170187598400134746631,
                  -5.593152259116644920546186222529802777409274656e-9), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-3.0, 5.1)),
                C(0.08217199226739447943295069917990417630675021771804,
                  -0.04701291087643609891018366143118110965272615832184), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-53, 30.1)),
                C(0.00457246000350281640952328010227885008541748668738,
                  -0.00804900791411691821818731763401840373998654987934), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(0.0, 0.12345)),
                C(0.8746342859608052666092782112565360755791467973338452, 0.0), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(11, 1)),
                C(0.00468190164965444174367477874864366058339647648741,
                  0.0510735563901306197993676329845149741675029197050), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-22, -2)),
                C(-0.0023193175200187620902125853834909543869428763219,
                  -0.025460054739731556004902057663500272721780776336), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(9, -28)),
                C(9.11463368405637174660562096516414499772662584e304,
                  3.97101807145263333769664875189354358563218932e305), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(21, -33)),
                C(-4.4927207857715598976165541011143706155432296e281,
                  -2.8019591213423077494444700357168707775769028e281), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e5, 1e5)),
                C(2.820947917809305132678577516325951485807107151e-6,
                  2.820947917668257736791638444590253942253354058e-6), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e14, 1e14)),
                C(2.82094791773878143474039725787438662716372268e-15,
                  2.82094791773878143474039725773333923127678361e-15), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-3001, -1000)),
                C(-0.0000563851289696244350147899376081488003110150498,
                  -0.000169211755126812174631861529808288295454992688), C_FUNC_NAME);
            // ZTest(
            //     result, 1e-13, w_of_z(C(1e160, -1e159)),
            //     C(-5.586035480670854326218608431294778077663867e-162,
            //       5.586035480670854326218608431294778077663867e-161), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-6.01, 0.01)),
                C(0.00016318325137140451888255634399123461580248456,
                  -0.095232456573009287370728788146686162555021209999), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-0.7, -0.7)),
                C(0.69504753678406939989115375989939096800793577783885,
                  -1.8916411171103639136680830887017670616339912024317), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(2.611780000000000e+01, 4.540909610972489e+03)),
                C(0.0001242418269653279656612334210746733213167234822,
                  7.145975826320186888508563111992099992116786763e-7), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(0.8e7, 0.3e7)),
                C(2.318587329648353318615800865959225429377529825e-8,
                  6.182899545728857485721417893323317843200933380e-8), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-20, -19.8081)),
                C(-0.0133426877243506022053521927604277115767311800303,
                  -0.0148087097143220769493341484176979826888871576145), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e-16, -1.1e-16)),
                C(1.00000000000000012412170838050638522857747934,
                  1.12837916709551279389615890312156495593616433e-16), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(2.3e-8, 1.3e-8)),
                C(0.9999999853310704677583504063775310832036830015,
                  2.595272024519678881897196435157270184030360773e-8), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(6.3, -1e-13)),
                C(-1.4731421795638279504242963027196663601154624e-15,
                  0.090727659684127365236479098488823462473074709), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(6.3, 1e-20)),
                C(5.79246077884410284575834156425396800754409308e-18,
                  0.0907276596841273652364790985059772809093822374), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e-20, 6.3)),
                C(0.0884658993528521953466533278764830881245144368,
                  1.37088352495749125283269718778582613192166760e-22), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e-20, 16.3)),
                C(0.0345480845419190424370085249304184266813447878,
                  2.11161102895179044968099038990446187626075258e-23), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(9, 1e-300)),
                C(6.63967719958073440070225527042829242391918213e-36,
                  0.0630820900592582863713653132559743161572639353), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(6.01, 0.11)),
                C(0.00179435233208702644891092397579091030658500743634,
                  0.0951983814805270647939647438459699953990788064762), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(8.01, 1.01e-10)),
                C(9.09760377102097999924241322094863528771095448e-13,
                  0.0709979210725138550986782242355007611074966717), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(28.01, 1e-300)),
                C(7.2049510279742166460047102593255688682910274423e-304,
                  0.0201552956479526953866611812593266285000876784321), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(10.01, 1e-200)),
                C(3.04543604652250734193622967873276113872279682e-44,
                  0.0566481651760675042930042117726713294607499165), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(10.01, -1e-200)),
                C(3.04543604652250734193622967873276113872279682e-44,
                  0.0566481651760675042930042117726713294607499165), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(10.01, 0.99e-10)),
                C(0.5659928732065273429286988428080855057102069081e-12,
                  0.056648165176067504292998527162143030538756683302), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(10.01, -0.99e-10)),
                C(-0.56599287320652734292869884280802459698927645e-12,
                  0.0566481651760675042929985271621430305387566833029), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e-20, 7.01)),
                C(0.0796884251721652215687859778119964009569455462,
                  1.11474461817561675017794941973556302717225126e-22), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-1, 7.01)),
                C(0.07817195821247357458545539935996687005781943386550,
                  -0.01093913670103576690766705513142246633056714279654), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(5.99, 7.01)),
                C(0.04670032980990449912809326141164730850466208439937,
                  0.03944038961933534137558064191650437353429669886545), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1, 0)),
                C(0.36787944117144232159552377016146086744581113103176,
                  0.60715770584139372911503823580074492116122092866515), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(55, 0)),
                C(0, 0.010259688805536830986089913987516716056946786526145), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(-0.1, 0)),
                C(0.99004983374916805357390597718003655777207908125383,
                  -0.11208866436449538036721343053869621153527769495574), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(1e-20, 0)),
                C(0.99999999999999999999999999999999999999990000,
                  1.12837916709551257389615890312154517168802603e-20), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(0, 5e-14)),
                C(0.999999999999943581041645226871305192054749891144158, 0), C_FUNC_NAME);
            TestTool.ZTest(
                ref result, 1e-13, w_of_z(C(0, 51)),
                C(0.0110604154853277201542582159216317923453996211744250, 0), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(Inf, 0)), C(0, 0), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(-Inf, 0)), C(0, 0), C_FUNC_NAME);
            // ZTest(result, 1e-13, w_of_z(C(0, Inf)), C(0, 0), C_FUNC_NAME);
            // ZTest(result, 1e-13, w_of_z(C(0, -Inf)), C(Inf, 0), C_FUNC_NAME);
            // ZTest(result, 1e-13, w_of_z(C(Inf, Inf)), C(0, 0), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(Inf, -Inf)), C(NaN, NaN), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(NaN, NaN)), C(NaN, NaN), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(NaN, 0)), C(NaN, NaN), C_FUNC_NAME);
            // ZTest(result, 1e-13, w_of_z(C(0, NaN)), C(NaN, 0), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(NaN, Inf)), C(NaN, NaN), C_FUNC_NAME);
            TestTool.ZTest(ref result, 1e-13, w_of_z(C(Inf, NaN)), C(NaN, NaN), C_FUNC_NAME);

            Console.WriteLine(string.Format("{0}/{1} tests failed", result.Failed, result.Total));
            //printf("%i/%i tests failed", result.failed, result.total);
            return result.Failed;
        }

    }
}

