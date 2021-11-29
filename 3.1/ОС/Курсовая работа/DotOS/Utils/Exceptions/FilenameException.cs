using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils.Exceptions
{
    class FilenameException : Exception
    {
        public override string Message => "Название файла не должно превышать 5 символов";
    }
}
