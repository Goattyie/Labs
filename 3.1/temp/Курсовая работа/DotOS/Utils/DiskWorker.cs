using DotOS.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils
{
    class DiskWorker
    {
        FileStream _streamer;
        
        public DiskWorker() { }
        public void OpenOrCreate()
        {
            if(_streamer == null)
                _streamer = new FileStream(FileSystem.DiskName, FileMode.OpenOrCreate);
        }

        public byte[] Read(byte[] buffer, long seek, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            _streamer.Seek(seek, seekOrigin);
            _streamer.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        public async Task<byte[]> ReadAsync(byte[] buffer, long seek, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            _streamer.Seek(seek, seekOrigin);
            await _streamer.ReadAsync(buffer, 0, buffer.Length);
            return buffer;
        }

        public void Write(byte[] buffer, long seek = 0, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            if(seek != 0)
                _streamer.Seek(seek, seekOrigin);

            _streamer.Write(buffer, 0, buffer.Length);
            _streamer.Flush();
        }

        public async Task WriteAsync(byte[] buffer, long seek = 0, SeekOrigin seekOrigin = SeekOrigin.Begin)
        {
            if (seek != 0)
                _streamer.Seek(seek, seekOrigin);

            await _streamer.WriteAsync(buffer, 0, buffer.Length);
        }
        public void Close()
        {
            _streamer?.Close();
        }
    }
}
