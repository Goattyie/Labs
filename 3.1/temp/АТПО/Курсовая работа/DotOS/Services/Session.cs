using DotOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Services
{
    class Session
    {
        public FileInfo CurrentDirectory { get; set; }
        public Session()
        {
            CurrentDirectory = new FileInfo();
        }
    }
}
