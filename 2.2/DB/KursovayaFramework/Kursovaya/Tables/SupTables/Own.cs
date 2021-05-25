using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Own : SupTable
    {
        public override string ClassName => "own";
        public Own() { }
        public Own(string value) { Value = value; }
    }
}
