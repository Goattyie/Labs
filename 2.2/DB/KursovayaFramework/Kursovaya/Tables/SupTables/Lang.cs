using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Lang : SupTable
    {
        public override string ClassName => "lang";
        public Lang() { }
        public Lang(string value) { Value = value; }
    }
}
