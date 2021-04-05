using System;
using System.Collections.Generic;
using System.Text;

namespace MyFacade
{
    public class MyConsole
    {
        static public void PressAnyKeyToEndTheProgram()
        {
            Console.Write("Press any key to end the program.");
            Console.ReadKey();
        }
    }
}
