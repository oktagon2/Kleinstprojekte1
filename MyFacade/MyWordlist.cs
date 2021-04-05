using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyFacade
{
    public class MyWordlist
    {
        public static string FilePath
        {
            get
            {
                return @"C:\Users\Ruedi\Documents\Dokumentation\Systeme\Wortliste\tu-chemnitz-de-en.txt";
            }

        }

        public List<string> Words { get; private set; }


        public MyWordlist()
        {
            Words = new List<string>();
            foreach(string line in File.ReadLines(FilePath))
            {
                if( line[0]!= '#')
                {
                    int pos = line.IndexOf(' ');
                    string word = line.Substring(0, pos);
                    if( !word.EndsWith( ';'))
                    {
                        Words.Add(word);
                    }
                }
            }
        }

        public string RandomWord
        {
            get
            {
                string ret = Words[MyRandomNumberGenerator.GetNumber(0, NumberOfEntries - 1)];
                return ret;
            }
        }

        public int NumberOfEntries
        {
            get
            {
                int ret = Words.Count;
                return ret;
            }
        }
    }
}
