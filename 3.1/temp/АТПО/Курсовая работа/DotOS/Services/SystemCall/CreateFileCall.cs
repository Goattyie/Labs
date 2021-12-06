using DotOS.Models;
using DotOS.Utils;
using DotOS.ViewModels;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotOS.Services.SystemCall
{
    class CreateFileCall : ISystemCall
    {
        private readonly MessageBus _messageBus;
        private readonly DiskWorker _diskWorker;
        private readonly FileSystem _fileSystem;
        private FileInfo _fileInfo;
        private string _data;

        public CreateFileCall(FileSystem fileSystem, DiskWorker diskWorker, MessageBus messageBus)
        {
            _messageBus = messageBus;
            _diskWorker = diskWorker;
            _fileSystem = fileSystem;
        }

        public bool CanExecute(string command) => Regex.IsMatch(command, @"create [A-Za-z0-9]*.[a-z]*");

        public Task Execute(string command)
        {
            _fileInfo = new FileInfo() { Name = command.Split(' ', '>')[1] };
            if (command.Split('>').Length == 2)
                _data = command.Split('>')[1];

            _fileInfo.Attribute = new ReadOnlyAttr();
            var rootIndex = _fileSystem.GetPositionOfClearBytesInRootDirectory(16);
            var fatIndex = _fileSystem.GetPositionOfClearByteInFatTable();
            _diskWorker.Write(_fileInfo.Name.ToByteArray(), rootIndex);
            rootIndex += _fileInfo.Name.Length;
            _diskWorker.Write(new byte[1] { _fileInfo.Attribute.Code }, rootIndex);
            rootIndex += 1;
            _diskWorker.Write(_fileInfo.Reserved, rootIndex);
            rootIndex += _fileInfo.Reserved.Length;
            _diskWorker.Write(_fileInfo.Date.ToByteArray(), rootIndex);
            rootIndex += 2;
            _diskWorker.Write(_fileInfo.Time.ToByteArray(), rootIndex);
            rootIndex += 2;
            _diskWorker.Write((fatIndex - _fileSystem.BeginSectorFatTable * _fileSystem.BytesInSector).ToByteArray(), rootIndex);
            rootIndex += 4;
            _diskWorker.Write(_data.Length.ToByteArray(), rootIndex);

            //Определяем, вместятся ли данные в кластер
            if (_data.Length < _fileSystem.ClasterSize)
            {
                //Записываем в Fat таблицы значение 
                _diskWorker.Write(ClusterStatus.End.ToByteArray(), fatIndex);
                _diskWorker.Write(ClusterStatus.End.ToByteArray(), fatIndex + _fileSystem.TableSectorsCount * _fileSystem.BytesInSector);
                _diskWorker.Write(_data.ToByteArray(), _fileSystem.BeginSectorDataArea * _fileSystem.BytesInSector + (fatIndex - _fileSystem.BeginSectorFatTable * _fileSystem.BytesInSector) * _fileSystem.ClasterSize);
                //Записываем данные в кластер в индексом fatIndex; BeginSectorDataArea * BytesInSector + (fatIndex - BeginSectorFatTable * BytesInSector) * ClasterSize

            }
            else
            {
                var diff = _data.Length / _fileSystem.ClasterSize;
                for (int j = 0; j < diff; j++)
                {
                    var partOfData = _data.Substring(j, _fileSystem.ClasterSize);
                    _diskWorker.Write(ClusterStatus.Busy.ToByteArray(), fatIndex);
                    _diskWorker.Write(ClusterStatus.Busy.ToByteArray(), fatIndex + _fileSystem.TableSectorsCount);

                    _diskWorker.Write(partOfData.ToByteArray(), _fileSystem.BeginSectorDataArea + (fatIndex - _fileSystem.BeginSectorFatTable * _fileSystem.BytesInSector) * _fileSystem.ClasterSize);
                }
            }

            _ = _messageBus.SendTo<FilesPageViewModel>(new AddFileMessage() { File = _fileInfo });

            return Task.CompletedTask;
        }
    }
}
