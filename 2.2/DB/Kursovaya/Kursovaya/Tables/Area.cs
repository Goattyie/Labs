using System;
using System.Collections.Generic;
using System.IO;

namespace Kursovaya
{
    class Area : Table
    {

        protected override string ClassName => "area";
        protected override string PrimaryKey => "id_area";
        string Value;
        public Area() { }
        public Area(string value){  Value = value;  }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name_{ClassName}) VALUES ({Value})";


        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, name_{ClassName} AS Район FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"name","\"Район(Уникальность)\""},
            };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Район\""},
            };

        protected override void GenerateNode()
        {
            Value = GenerateLine(FileGeneratorPath[0]);
        }
    }
}
