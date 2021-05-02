using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Binding : Table
    {
        protected override string ClassName => "binding";
        protected override string PrimaryKey => "id_binding";

        string Value;
        public Binding() { }
        public Binding(string value) { Value = value; }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name_{ClassName}) VALUES ({Value})";

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, name_{ClassName} AS \"Тип переплета\" FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"name","\"Тип переплета(Уникальность)\""},
            };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Тип переплета\""},
            };

        protected override void GenerateNode()
        {
            Value = GenerateLine(FileGeneratorPath[0]);
        }
    }
}
