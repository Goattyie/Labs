using DotOS.Models;
using DotOS.Utils;
using DotOS.ViewModels;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotOS.Services.SystemCall
{
    class ReadFileCall : ISystemCall
    {
        private readonly MessageBus _messageBus;
        private readonly FileSystem _fileSystem;
        private readonly DiskWorker _diskWorker;
        private FileInfo _file;

        public bool CanExecute(string command) => Regex.IsMatch(command, @"read [A-Za-z0-9]*.dot");

        public ReadFileCall(MessageBus messageBus, FileSystem fileSystem, DiskWorker diskWorker)
        {
            _messageBus = messageBus;
            _fileSystem = fileSystem;
            _diskWorker = diskWorker;
        }
        public Task Execute(string command)
        {
            var filename = command.Split(' ')[1];
            var list = _fileSystem.ReadRootDirectory();
            _file = list.FirstOrDefault(x => x.Name == filename);
            if (_file != null)
            {
                var data = _diskWorker.Read(new byte[_fileSystem.ClasterSize], _fileSystem.BeginSectorDataArea * _fileSystem.BytesInSector + _file.FirstClusterNumber * _fileSystem.ClasterSize).ArrayToString();
                _ = _messageBus.SendTo<FilesPageViewModel>(new FileText() { Text = data });
            }
            return Task.CompletedTask;
        }
    }

    class FileText : IMessage
    {
        public string Text { get; set; }
    }
}
