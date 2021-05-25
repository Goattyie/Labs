using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Binding : SupTable
    {
        public override string ClassName => "binding";
        public Binding() { }
        public Binding(string value) { Value = value; }
    }
}
