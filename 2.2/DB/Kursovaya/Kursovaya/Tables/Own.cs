using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Own : Table
    {
        protected override string ClassName => "own";
        protected override string PrimaryKey => "id_own";

        string Value;
        public Own() { }
        public Own(string value) { Value = value; }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name_{ClassName}) VALUES ({Value})";

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, name_{ClassName} AS \"Тип собственности\" FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"name","\"Тип собственности(Уникальность)\""},
            };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Тип собственности\""},
            };

        protected override void GenerateNode()
        {
            Value = GenerateLine(FileGeneratorPath[0]);
        }
    }
}
