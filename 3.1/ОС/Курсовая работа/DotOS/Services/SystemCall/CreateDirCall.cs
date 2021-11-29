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
    class CreateDirCall : ISystemCall
    {
        private readonly Session _session;
        private readonly FileSystem _fileSystem;
        private readonly DiskWorker _diskWorker;
        private FileInfo _fileInfo;

        public CreateDirCall(DiskWorker diskworker, FileSystem fileSystem, Session session)
        {
            _session = session;
            _fileSystem = fileSystem;
            _diskWorker = diskworker;
        }


        public bool CanExecute(string command)
        {
            if (Regex.IsMatch(command, @"create.dir [A-Za-z0-9]*"))
            {
                _fileInfo = new FileInfo() { Name = command.Split(' ')[1] + ".dir", Attribute = new DirectoryAttr() };
                return true;
            }
            return false;
        }

        public Task Execute()
        {
            return Task.CompletedTask;
        }
    }
}
