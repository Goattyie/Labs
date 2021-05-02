using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Lang : Table
    {
        protected override string ClassName => "lang";
        protected override string PrimaryKey => "id_lang";

        string Value;
        public Lang() { }
        public Lang(string value) { Value = value; }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name_{ClassName}) VALUES ({Value})";

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, name_{ClassName} AS \"Язык\" FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"name","\"Язык(Уникальность)\""},
            };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Язык\""},
            };

        protected override void GenerateNode()
        {
            Value = GenerateLine(FileGeneratorPath[0]);
        }
    }
}
