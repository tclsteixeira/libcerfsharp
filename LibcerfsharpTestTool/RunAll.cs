using System;

namespace LibcerfsharpTestTool
{

    /// <summary>
    /// Runs all tests.
    /// </summary>
    public static class RunAll
    {

        public static void Run()
        {
            Console.WriteLine("Start testing libcerf math functions");
            Console.WriteLine("* wofztest *");
            wofztest.Run();

            Console.WriteLine();
            Console.WriteLine("* realtest *");
            realtest.Run();

            Console.WriteLine();
            Console.WriteLine("* cerftest *");
            cerftest.Run();

            Console.WriteLine();
            Console.WriteLine("* dawsontest *");
            dawsontest.Run();

            Console.WriteLine();
            Console.WriteLine("* voigttest *");
            voigttest.Run();

            Console.WriteLine();
            Console.WriteLine("* widthtest *");
            widthtest.Run();

            Console.WriteLine();
            Console.WriteLine("End of tests --------------");
        }

    }

}
