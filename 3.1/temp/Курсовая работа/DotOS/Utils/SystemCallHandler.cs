using DotOS.Services.SystemCall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils
{
    class SystemCallHandler
    {
        private readonly IEnumerable<ISystemCall> _systemCalls;

        public SystemCallHandler(IEnumerable<ISystemCall> systemCalls)
        {
            _systemCalls = systemCalls;
        }
        public Task Handle(string command) 
        {
            var call = _systemCalls.FirstOrDefault(x => x.CanExecute(command));
            call?.Execute(command);
            return Task.CompletedTask;
        }
    }
}
