﻿using DotOS.Models;
using DotOS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotOS.Services.SystemCall
{
    class ReadDirectoryCall : ISystemCall
    {
        private readonly FileSystem _fileSystem;
        private readonly DiskWorker _diskWorker;
        private string _directoryPath;

        public ReadDirectoryCall(DiskWorker diskworker, FileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _diskWorker = diskworker;
        }
        public bool CanExecute(string command)
        {
            if (Regex.IsMatch(command, @"go [A-Za-z0-9]*.dir"))
            {
                _directoryPath = command.Split(' ')[1];
                return true;
            }

            return false;
        }


        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
