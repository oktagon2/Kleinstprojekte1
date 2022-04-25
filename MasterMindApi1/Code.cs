using System;
using System.Collections.Generic;
using System.Text;

namespace MasterMindApi1
{
    public class Code
    {
        public List<int> Colors { get; set; }
        public bool Ok { get; set; }


        public bool Check()
        {
            bool ret = false;
            if( Colors.Count== 4)
            {
                List<int> colorsToCheck = new List<int>();
                foreach( int color in Colors)
                {
                    if(( color>= 1)&&( color<= 8))
                    {
                        if( !colorsToCheck.Contains( color))
                        {
                            colorsToCheck.Add(color);
                        }
                    }
                }
                ret = colorsToCheck.Count == 4;
            }
            Ok = ret;
            return ret;
        }

        public Code( params int[] colors)
        {
            Colors = new List<int>(colors);
            Ok = Check();
        }

        public Code( string colors)
        {
            Colors = new List<int>();
            foreach( char c in colors)
            {
                Colors.Add(Convert.ToInt32(c) - 48);
            }
            Ok = Check();
        }

        public override string ToString()
        {
            string ret = "";
            foreach( int color in Colors)
            {
                ret = ret + Convert.ToChar(color + 48);
            }
            return ret;
        }
    }
}
