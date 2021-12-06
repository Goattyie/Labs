using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Models
{
    class Superblock
    {
        public string FSName { get; private set; }
        public short OneBlockSize { get; private set; }
        public short FatTablesCount { get; private set; }
        public int OneTableSize { get; private set; }
        public int UserAreaSize { get; private set; }
        public int RootDirectorySize { get; private set; }
        public int DataAreaSize { get; private set; }
        public Task Load(byte[] arr)
        {
            return Task.CompletedTask;
        }
    }
}
