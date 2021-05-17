using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Style : SupTable
    {
        protected override string ClassName => "style";
        public Style() { }
        public Style(string value) { Value = value; }
    }
}
