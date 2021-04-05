using System;
using System.Collections.Generic;
using System.Text;

namespace MyFacade.ValorAnalysis
{
    class MyPriceLine
    {
        public string Symbol { get; set; }
        public DateTime Date1 { get; set; }
        public double Price1 { get; set; }
        public DateTime Date2 { get; set; }
        public double Price2 { get; set; }

        public MyPriceLine( string symbol, int month1, int year1, double price1, int month2, int year2, double price2)
        {
            if (year1 < 2000) year1 += 2000;
            if (year2 < 2000) year2 += 2000;
            Symbol = symbol;
            Date1 = new DateTime(year1, month1, 1);
            Price1 = price1;
            Date2 = new DateTime(year2, month2, 1);
            Price2 = price2;
        }
    }
}
