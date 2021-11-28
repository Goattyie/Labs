using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Services.SystemCall
{
    class ReadDirectoryCall : ISystemCall
    {
        public bool CanExecute(string command)
        {
            if (command.Contains("read"))
                return true;

            return false;
        }

        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }
}
