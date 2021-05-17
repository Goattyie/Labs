using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    abstract class SupTable:Table
    {
        protected string Value { get; set; }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name) VALUES ({Value})";
        protected override string SelectQuery => $"SELECT id ID, name Название FROM {ClassName}";
        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"name","\"Уникальность\""},
            };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Название\""},
            };
    }
}
