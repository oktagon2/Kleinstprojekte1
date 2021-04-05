using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFacade
{
    public class MyFile
    {
        public static int GetNumberOfLines( string path)
        {
            int ret = File.ReadAllLines( path).Length;
            return ret;
        }
    }
}
