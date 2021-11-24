using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils
{
    class Formatter
    {
        private readonly SuperBlockInfo _superBlockInfo;
        private readonly SystemSettings _settings;

        public Formatter(SystemSettings settings, SuperBlockInfo superBlockInfo)
        {
            _superBlockInfo = superBlockInfo;
            _settings = settings;
        }
        public Task Formatting()
        {
            using (var sw = new FileStream(_settings.Disk, FileMode.OpenOrCreate))
            {
                var array = Encoding.Default.GetBytes(_superBlockInfo.OsName);
                sw.Write(array, 0, array.Length);
                sw.Close();
            }
            return Task.CompletedTask;
        }

        
    }
}
