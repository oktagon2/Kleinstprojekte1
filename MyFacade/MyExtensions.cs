using System;

namespace MyFacade
{
    public static class MyExtensions
    {
        public static int ConsoleReadInt()
        {
            int ret = 0;
            string input = Console.ReadLine();
            ret = int.Parse(input);
            return ret;
        }
        public static double ConsoleReadDouble()
        {
            double ret = 0;
            string input = Console.ReadLine();
            ret = double.Parse(input);
            return ret;
        }
    }
}
