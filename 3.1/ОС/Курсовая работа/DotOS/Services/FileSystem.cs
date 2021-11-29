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
        /// <summary>
        /// Название ОС.
        /// </summary>
        public string SystemName { get; private set; }
        /// <summary>
        /// Количество байтор в кластере.
        /// </summary>
        public int ClasterSize { get; private set; }
        /// <summary>
        /// Количество секторов в 1 кластере.
        /// </summary>
        public int ClasterSectorsCount { get; private set; }
        /// <summary>
        /// Размер сектора в байтах.
        /// </summary>
        public int BytesInSector { get; private set; }
        /// <summary>
        /// Количество секторов суперблока.
        /// </summary>
        public int SuperBlockSectorsCount { get; private set; }
        /// <summary>
        /// Количество Fat таблиц.
        /// </summary>
        public short CountTables { get; private set; } 
        /// <summary>
        /// Количество секторов, занимаемых одной таблицей.
        /// </summary>
        public int TableSectorsCount { get; private set; }
        /// <summary>
        /// Количество секторов корневого каталога
        /// </summary>
        public int RootDirectorySize { get; private set; }
        /// <summary>
        /// Размер области информации пользователей в секторах
        /// </summary>
        public int UsersInfoSectorCount { get; private set; }
        /// <summary>
        /// Размер области данных
        /// </summary>
        public int DataAreaSectorSize { get; private set; }
        /// <summary>
        /// Начальный сектор таблицы FAT #1
        /// </summary>
        public int BeginSectorFatTable { get; private set; }
        /// <summary>
        /// Начальный сектор корневой директории
        /// </summary>
        public int BeginSectorRootDirectory { get; private set; }
        /// <summary>
        /// Начальный сектор области с пользователями
        /// </summary>
        public int BeginSectorUsersInfo { get; private set; }
        /// <summary>
        /// Начальный сектор области данных
        /// </summary>
        public int BeginSectorDataArea { get; private set; }
        public FileSystem(DiskWorker diskWorker, Session session)
        {
            _session = session;
            _diskWorker = diskWorker;
        }
        public int GetPositionOfClearBytesInRootDirectory(int bytesCapacity)
        {
            bool isClear;
            for (int i = BeginSectorRootDirectory * BytesInSector; i < BeginSectorDataArea * BytesInSector; i+= bytesCapacity)
            {
                isClear = true;
                var data = _diskWorker.Read(new byte[bytesCapacity], i);
                for(int j = 0; j < data.Length; j++)
                {
                    if (data[j] != 0)
                    {
                        isClear = false;
                        break;
                    }
                }
                if (isClear)
                    return i;
            }
            throw new FreeBytesException();
        }
        public int GetPositionOfClearByteInFatTable()
        {
            for(int i = BeginSectorFatTable * BytesInSector; i < (BeginSectorFatTable + TableSectorsCount) * BytesInSector; i++)
            {
                if (_diskWorker.Read(new byte[1], i)[0] == 0)
                    return i;
            }
            throw new FreeBytesException();
        }
        public void Formating()
        {
            File.Delete(DiskName);
            _diskWorker.OpenOrCreate();

            var systemNameBytes = "DotOS".ToByteArray();
            _diskWorker.Write(systemNameBytes);

            var clusterSectorsCount = ((short)32).ToByteArray();
            _diskWorker.Write(clusterSectorsCount);

            var bytesInSector = 512.ToByteArray();
            _diskWorker.Write(bytesInSector);

            var superBlockSectorsCount = 1000.ToByteArray();
            _diskWorker.Write(superBlockSectorsCount);

            var countTables = ((short)2).ToByteArray();
            _diskWorker.Write(countTables);

            var tableSectorsCount = 62.ToByteArray();
            _diskWorker.Write(tableSectorsCount);

            var usersInfoSectorsCount = 5.ToByteArray();
            _diskWorker.Write(usersInfoSectorsCount);

            var rootDirectorySizeSectors = 2.ToByteArray();
            _diskWorker.Write(rootDirectorySizeSectors);

            var dataAreaSectorSize = 1984.ToByteArray();
            _diskWorker.Write(dataAreaSectorSize);

            Boot();
        }
        public void Boot()
        {
            _diskWorker.OpenOrCreate();

            SystemName = _diskWorker.Read(new byte[5], 0).ArrayToString();
            ClasterSectorsCount = _diskWorker.Read(new byte[2], 5).ArrayToShort();
            BytesInSector = _diskWorker.Read(new byte[4], 7).ArrayToInt();
            ClasterSize = ClasterSectorsCount * BytesInSector;
            SuperBlockSectorsCount = _diskWorker.Read(new byte[4], 11).ArrayToInt();
            CountTables = _diskWorker.Read(new byte[2], 15).ArrayToShort();
            TableSectorsCount = _diskWorker.Read(new byte[4], 17).ArrayToInt();
            UsersInfoSectorCount = _diskWorker.Read(new byte[4], 21).ArrayToInt();
            RootDirectorySize = _diskWorker.Read(new byte[4], 25).ArrayToInt();
            DataAreaSectorSize = _diskWorker.Read(new byte[4], 29).ArrayToInt();
            //Расчеты начальных значений данных
            BeginSectorFatTable = SuperBlockSectorsCount;
            BeginSectorUsersInfo = BeginSectorFatTable + 2 * TableSectorsCount;
            BeginSectorRootDirectory = BeginSectorUsersInfo + UsersInfoSectorCount;
            BeginSectorDataArea = BeginSectorRootDirectory + RootDirectorySize;
        }
    }
    class ClusterStatus
    {
        public static int Free = 1;
        public static int Busy = 2;
        public static int End = 3;
    }
}
