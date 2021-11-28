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

        /// <summary>
        /// Название ОС.
        /// </summary>
        public string SystemName { get; private set; }
        /// <summary>
        /// Количество секторов на кластер.
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
        /// Сектор, с которого начинается первая таблица.
        /// </summary>
        public int TablesBeginSector { get; private set; }
        /// <summary>
        /// Количество Fat таблиц.
        /// </summary>
        public short CountTables { get; private set; } 
        /// <summary>
        /// Количество секторов, занимаемых одной таблицей.
        /// </summary>
        public int TableSectorsCount { get; private set; }
        /// <summary>
        /// Начальный сектор корневого каталога
        /// </summary>
        public int BeginSectorRootDirectory { get; private set; }
        /// <summary>
        /// Размер корневого каталога в секторах
        /// </summary>
        public int RootDirectorySize { get; private set; }

        private readonly DiskWorker _diskWorker;
        public FileSystem(DiskWorker diskWorker)
        {
            _diskWorker = diskWorker;
        }
        public void Formating()
        {
            File.Delete(DiskName);
            _diskWorker.OpenOrCreate();

            var systemNameBytes = "DotOS".ToByteArray();
            _diskWorker.Write(systemNameBytes);

            var blockSizeBytes = ((short)32).ToByteArray();
            _diskWorker.Write(blockSizeBytes);

            var bytesInSector = 512.ToByteArray();
            _diskWorker.Write(bytesInSector);

            var superBlockSectorsCountBytes = 6332.ToByteArray();
            _diskWorker.Write(superBlockSectorsCountBytes);

            var countTablesBytes = ((short)2).ToByteArray();
            _diskWorker.Write(countTablesBytes);

            var tableSectorsCount = 500.ToByteArray();
            _diskWorker.Write(tableSectorsCount);

            var rootDirectorySizeSectors = 4000.ToByteArray();
            _diskWorker.Write(rootDirectorySizeSectors);

            int i = 25;
            int maxSectors = 6332 + (500*2);
            while (i <= maxSectors)
            {
                _diskWorker.Write(0.ToByteArray(), i);
                i += 4;
            }

        }
        public void Boot()
        {
            _diskWorker.OpenOrCreate();

            SystemName = _diskWorker.Read(new byte[5], 0).ArrayToString();
            ClasterSectorsCount = _diskWorker.Read(new byte[2], 5).ArrayToShort();
            BytesInSector = _diskWorker.Read(new byte[4], 7).ArrayToInt();
            ClasterSize = ClasterSectorsCount * BytesInSector;
            SuperBlockSectorsCount = _diskWorker.Read(new byte[4], 11).ArrayToInt();
            TablesBeginSector = BytesInSector * SuperBlockSectorsCount;
            CountTables = _diskWorker.Read(new byte[2], 15).ArrayToShort();
            TableSectorsCount = _diskWorker.Read(new byte[4], 17).ArrayToInt();
            RootDirectorySize = _diskWorker.Read(new byte[4], 21).ArrayToInt();
            BeginSectorRootDirectory = SuperBlockSectorsCount + CountTables * TableSectorsCount;
            
        }
        /// <summary>
        /// Возвращает начальный индекс байта, после которого расположены size свободных байтов
        /// </summary>
        /// <param name="size">Количество свободных байтов</param>
        /// <returns></returns>
        public int FindFreeBytesInRoot(int size)
        {
            var startByte = BeginSectorRootDirectory * BytesInSector;
            var endByte = (BeginSectorRootDirectory + RootDirectorySize) * BytesInSector;
            for (int i = startByte; i < endByte; i++)
            {
                bool isFree = true;
                var buffer = new byte[size];
                buffer = _diskWorker.Read(new byte[size], i);
                buffer.ToList().ForEach(x => { if (x != 0) { isFree = false; return; } });
                if (isFree)
                    return i;

                i += size;
            }
            throw new FreeBytesException();
        }
    }
}
