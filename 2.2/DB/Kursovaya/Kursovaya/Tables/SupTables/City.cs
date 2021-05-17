using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class City : SupTable
    {
        public override string ClassName => "city";
        public City() { }
        public City(string value) { Value = value; }
    }
}
