using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class City : Table
    {
        protected override string ClassName => "city";
        protected override string PrimaryKey => "id_city";

        string Value;
        public City() { }
        public City(string value) { Value = value; }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name_{ClassName}) VALUES ({Value})";

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, name_{ClassName} AS \"Город\" FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"name","\"Город(Уникальность)\""},
            };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Город\""},
            };

    }
}
