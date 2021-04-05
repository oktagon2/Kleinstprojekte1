using System;

namespace RandomWords1
{
    class Program
    {
        static void Main(string[] args)
        {
            MyFacade.MyWordlist myWordlist = new MyFacade.MyWordlist();
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(myWordlist.RandomWord);
            }
            MyFacade.MyConsole.PressAnyKeyToEndTheProgram();
        }
    }
}
