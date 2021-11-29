﻿using DotOS.Models;
using DotOS.Utils;
using DotOS.ViewModels;
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
        private readonly Session _session;
        private readonly FileSystem _fileSystem;
        private readonly DiskWorker _diskWorker;
        private readonly MessageBus _messageBus;
        private string _directoryPath;
        private FileInfo _directory;

        public ReadDirectoryCall(DiskWorker diskworker, FileSystem fileSystem, Session session, MessageBus messageBus)
        {
            _messageBus = messageBus;
            _session = session;
            _fileSystem = fileSystem;
            _diskWorker = diskworker;
        }
        private List<FileInfo> ReadRootDirectory()
        {
            var list = new List<FileInfo>();
            var data = _diskWorker.Read(new byte[_fileSystem.RootDirectorySize * _fileSystem.BytesInSector], _fileSystem.BeginSectorRootDirectory * _fileSystem.BytesInSector).ArrayToString();
            for (int i = 0; i < data.Length; i += 32)
            {
                if (data[i] != '\0') 
                {
                    var file = new FileInfo() { Name = data.Substring(i, 9) };
                    list.Add(file);
                }
            }
            return list;
        }
        public bool CanExecute(string command) => Regex.IsMatch(command, @"show .dir");

        public Task Execute(string command)
        {
            var list = new FilesMessage();
            _directoryPath = command.Split(' ')[1];
            if (_directoryPath == ".dir")
                _directory = _session.CurrentDirectory;

            if (_directory.FirstClusterNumber != 0)
            {
                var data = _diskWorker.Read(new byte[_fileSystem.ClasterSize], _fileSystem.BeginSectorDataArea * _fileSystem.BytesInSector + (_directory.FirstClusterNumber - _fileSystem.BeginSectorFatTable * _fileSystem.BytesInSector) * _fileSystem.ClasterSize).ArrayToString();
            }
            else
            {
                list.FilesInfo = ReadRootDirectory();
            }
            _ =_messageBus.SendTo<FilesPageViewModel>(list);
            return Task.CompletedTask;
        }
    }
}
