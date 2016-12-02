using MyTunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyTunes
{
    class StreamLoader : IStreamLoader
    {
        public Stream GetStreamForFilename(string filename)
        {
            return File.OpenRead(filename);
        }
    }
}
