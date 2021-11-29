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
        public FileSystem(DiskWorker diskWorker)
        {
            _diskWorker = diskWorker;
        }
        private int GetPositionOfClearBytesInRootDirectory(int bytesCapacity)
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
        private int GetPositionOfClearByteInFatTable()
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

            var usersInfoSectorCount = 5.ToByteArray();
            _diskWorker.Write(usersInfoSectorCount);

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
        public Task CreateFile(Models.FileInfo file, string data)
        {
            var rootIndex = GetPositionOfClearBytesInRootDirectory(16);
            var fatIndex = GetPositionOfClearByteInFatTable();
            _diskWorker.Write(file.Name.ToByteArray(), rootIndex);
            rootIndex += file.Name.Length;
            _diskWorker.Write(new byte[1] { file.Attribute.Code }, rootIndex);
            rootIndex += 1;
            _diskWorker.Write(file.Reserved, rootIndex);
            rootIndex += file.Reserved.Length;
            _diskWorker.Write(file.Date.ToByteArray(), rootIndex);
            rootIndex += 2;
            _diskWorker.Write(file.Time.ToByteArray(), rootIndex);
            rootIndex += 2;
            _diskWorker.Write((fatIndex - BeginSectorFatTable * BytesInSector).ToByteArray(), rootIndex);
            rootIndex += 4;
            _diskWorker.Write(data.Length.ToByteArray(), rootIndex);

            //Определяем, вместятся ли данные в кластер
            if (data.Length < ClasterSize)
            {
                //Записываем в Fat таблицы значение 
                _diskWorker.Write(ClusterStatus.End.ToByteArray(), fatIndex);
                _diskWorker.Write(ClusterStatus.End.ToByteArray(), fatIndex + TableSectorsCount * BytesInSector);
                _diskWorker.Write(data.ToByteArray(), BeginSectorDataArea * BytesInSector + (fatIndex - BeginSectorFatTable * BytesInSector) * ClasterSize);
                //Записываем данные в кластер в индексом fatIndex; BeginSectorDataArea * BytesInSector + (fatIndex - BeginSectorFatTable * BytesInSector) * ClasterSize

            }
            else
            {
                var diff = data.Length / ClasterSize;
                for (int j = 0; j < diff; j++) 
                {
                    var partOfData = data.Substring(j, ClasterSize);
                    _diskWorker.Write(ClusterStatus.Busy.ToByteArray(), fatIndex);
                    _diskWorker.Write(ClusterStatus.Busy.ToByteArray(), fatIndex + TableSectorsCount);

                    _diskWorker.Write(partOfData.ToByteArray(), BeginSectorDataArea + (fatIndex - BeginSectorFatTable * BytesInSector) * ClasterSize);
                }
            }

            return Task.CompletedTask;
        }
    }
    class ClusterStatus
    {
        public static int Free = 1;
        public static int Busy = 2;
        public static int End = 3;
    }
}
