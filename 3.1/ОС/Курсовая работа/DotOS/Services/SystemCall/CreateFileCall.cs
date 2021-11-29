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
        private FileInfo _fileInfo;

        public CreateFileCall(FileSystem fileSystem)
        {
            _fileSystem = fileSystem;
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
            _fileInfo.Attribute = new ReadOnlyAttr();
            return _fileSystem.CreateFile(_fileInfo, "Hello world!");
        }
    }
}
