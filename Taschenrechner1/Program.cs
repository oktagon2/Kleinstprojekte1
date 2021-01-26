using System;

namespace Taschenrechner1
{
    class Program
    {
        static double SquareRoot( double value, int depth)
        {
            double ret = 1.0;

            double valueTemp = value;
            while(valueTemp > 10.0)
            {
                ret *= 3.0;
                valueTemp /= 10.0;
            }

            for( int cnt= 0; cnt< depth; cnt++)
            {
                double y = ret * ret - value;
                double yDeriv = ret * 2;
                double betterRet = ret - y / yDeriv;
                ret = betterRet;
            }

            return ret;
        }

        static void Main(string[] args)
        {
            double value = 443556;
            Console.WriteLine("Math.Sqrt(): {0}", Math.Sqrt(value));
            for( int depth= 1; depth< 10; depth++)
            {
                Console.WriteLine( "{0}: {1}", depth, SquareRoot(value, depth));
            }
        }
    }
}
