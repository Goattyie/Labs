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
           
            return Task.CompletedTask;
        }
    }
}
