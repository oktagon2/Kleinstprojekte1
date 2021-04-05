using System;
using System.Collections.Generic;
using System.Text;

namespace ScanRenamer2
{
    class ScannedFile
    {
        public string Filename { get; private set; }
        public string Path { get; private set; }

        public ScannedFile( string filename, string path)
        {
            Filename = filename;
            this.Path = path;
        }
    }
}
