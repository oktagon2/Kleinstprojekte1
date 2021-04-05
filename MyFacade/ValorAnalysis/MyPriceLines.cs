using System;
using System.Collections.Generic;
using System.Text;

namespace MyFacade.ValorAnalysis
{
    /// <summary>
    /// Singleton
    /// </summary>
    class MyPriceLines
    {
        public Dictionary<string,MyPriceLine> Items { get; private set; }

        private static MyPriceLines instance = null;
        
        public static MyPriceLines Instance
        {
            get
            {
                if( instance== null)
                {
                    instance = new MyPriceLines();
                }
                return instance;
            }
        }

        private MyPriceLines()
        {
            Items = new Dictionary<string, MyPriceLine>();

            Items.Add("NFLX", new MyPriceLine("NFLX", 4, 13, 22.71, 3, 20, 290.25));

        }
        
    }
}
