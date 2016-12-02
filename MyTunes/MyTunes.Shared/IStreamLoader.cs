using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTunes
{
    public interface IStreamLoader
    {
        System.IO.Stream GetStreamForFilename(string filename);
    }
}
