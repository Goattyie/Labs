using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils
{
    class SystemSettings
    {
        private readonly string filePath = "disk";
        public string Disk => filePath;
    }
}
