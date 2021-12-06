using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils.Exceptions
{
    class FreeBytesException : Exception
    {
        public override string Message => "Не найдено свободных байтов";
    }
}
