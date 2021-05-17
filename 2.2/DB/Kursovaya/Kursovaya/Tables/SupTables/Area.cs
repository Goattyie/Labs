using System;
using System.Collections.Generic;
using System.IO;

namespace Kursovaya
{
    class Area : SupTable
    {

        protected override string ClassName => "area";
        public Area() { }
        public Area(string value){  Value = value;  }
    }
}
