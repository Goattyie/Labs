using DotOS.Utils.Exceptions;
using System.Collections.Generic;

namespace DotOS.Models
{
    class FileInfo
    {
        private string _stringName;
        public FileInfo() { }
        public FileInfo(string name, int firstClusterNumber)
        {
            Name = name;
            FirstClusterNumber = firstClusterNumber;
        }
        public string Name
        {
            get { return _stringName; }
            set
            {
                if (value.Length > 11)
                    throw new FilenameException();

                _stringName = value;
            }
        }
        public IAttribute Attribute { get; set; }
        public byte[] Reserved { get; set; } = new byte[10];
        public short Time { get; set; }
        public short Date { get; set; }
        public int FirstClusterNumber { get; set; }
        public int Size { get; set; }
    }
    interface IAttribute { byte Code { get; } }
    class ReadOnlyAttr : IAttribute
    {
        public byte Code => 1;
    }
    class HiddenAttr : IAttribute
    {
        public byte Code => 2;
    }
    class SystemAttr : IAttribute
    {
        public byte Code => 3;
    }
    class ArchiveAttr : IAttribute
    {
        public byte Code => 32;
    }
    class DirectoryAttr : IAttribute
    {
        public byte Code => 16;
    }

    class FilesMessage : IMessage
    {
        public List<FileInfo> FilesInfo { get; set; }
    }

    class AddFileMessage : IMessage
    {
        public FileInfo File { get; set; }
    }
}
