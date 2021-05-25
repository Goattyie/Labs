using System;
using System.Collections.Generic;

namespace Kursovaya
{
    class Author : MainTable
    {
        public override string ClassName => "author";
        string Name, SecondName, LastName;
        public Author() { }
        public Author(string name, string second_name, string last_name) 
        {
            Name = name;
            SecondName = second_name;
            LastName = last_name;
        }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name, second_name, last_name) VALUES ({Name}, {SecondName}, {LastName})";
        protected override string SelectQuery => $"SELECT id ID, second_name Фамилия, name Имя, last_name Отчество FROM {ClassName}";
        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"fio_uniq","\"Уникальность\"" } };
        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"last_name","\"Отчество\"" },
            new string[]{"second_name","\"Имя\""},
            new string[]{"name","\"Фамилия\""}
        };
        protected override string[][] TableNames => throw new NotImplementedException();
    }
}
