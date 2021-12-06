using DotOS.Models;
using DotOS.Utils;
using DotOS.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Services
{
    class FileSystem
    {

        public static readonly string DiskName = "disk";
        private readonly Session _session;
        private readonly DiskWorker _diskWorker;
        private readonly Superblock _superblock;

        public FileSystem(DiskWorker diskWorker, Session session, Superblock superblock)
        {
            _session = session;
            _diskWorker = diskWorker;
            _superblock = superblock;
        }

        public void Formating()
        {
            File.Delete(DiskName);
            _diskWorker.OpenOrCreate();

            _diskWorker.Write(new byte[33 + 2 * 800 + 3010 + 6400 + 3276800]);

            var systemNameBytes = "DotOS".ToByteArray();
            _diskWorker.Write(systemNameBytes);

            var clusterSize = ((short)16384).ToByteArray();
            _diskWorker.Write(clusterSize);

            var superblockSize = 33.ToByteArray();
            _diskWorker.Write(superblockSize);

            var fatTableSize = 800.ToByteArray();
            _diskWorker.Write(fatTableSize);

            var usersInfoSize = 3010.ToByteArray();
            _diskWorker.Write(usersInfoSize);

            var rootDirectorySize = 6400.ToByteArray();
            _diskWorker.Write(rootDirectorySize);

            var dataAreaSize = 3276800.ToByteArray();
            _diskWorker.Write(dataAreaSize);

            _superblock.Load();

        }
    }
    class ClusterStatus
    {
        public static int Free = 1;
        public static int Busy = 2;
        public static int End = 3;
    }
}
