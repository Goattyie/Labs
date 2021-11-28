using DotOS.Models;
using DotOS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotOS.Services.SystemCall
{
    class CreateFileCall : ISystemCall
    {
        private readonly FileSystem _fileSystem;
        private readonly DiskWorker _diskWorker;
        private FileInfo _fileInfo;

        public CreateFileCall(DiskWorker diskworker, FileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _diskWorker = diskworker;
        }

        public bool CanExecute(string command)
        {
            if (Regex.IsMatch(command, @"create [A-Za-z0-9]*.[a-z]*"))
            {
                _fileInfo = new FileInfo() { Name = command.Split(' ')[1] };
                return true;
            }
            return false;
        }

        public Task Execute()
        {
            var startIndex = _fileSystem.FindFreeBytesInRoot(32);
            _diskWorker.Write(_fileInfo.Name.ToByteArray(), startIndex);
            startIndex += 11;
            _diskWorker.Write(new byte[1] { _fileInfo.Attribute }, startIndex);
            startIndex += 1;
            _diskWorker.Write(_fileInfo.Reserved, startIndex);
            startIndex += 10;
            _diskWorker.Write(_fileInfo.Time.ToByteArray(), startIndex);
            startIndex += 2;
            _diskWorker.Write(_fileInfo.Date.ToByteArray(), startIndex);
            startIndex += 2;
            _diskWorker.Write(_fileInfo.FirstClusterNumber.ToByteArray(), startIndex);
            startIndex += 4;
            _diskWorker.Write(_fileInfo.Size.ToByteArray(), startIndex);

            return Task.CompletedTask;
        }
    }
}
