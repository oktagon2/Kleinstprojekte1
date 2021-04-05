using System;
using System.Collections.Generic;
using System.Text;

namespace MyFacade
{
    public class MyRandomNumberGenerator
    {
        public static int GetNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            int ret = random.Next(minValue, maxValue);
            return ret;
        }
    }
}
