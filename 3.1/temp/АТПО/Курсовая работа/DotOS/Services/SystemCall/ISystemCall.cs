using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Services.SystemCall
{
    interface ISystemCall
    {
        public bool CanExecute(string command);
        public Task Execute(string command);
    }
}
