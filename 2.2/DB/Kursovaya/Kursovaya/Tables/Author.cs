using System;
using System.Collections.Generic;

namespace Kursovaya
{
    class Author : Table
    {
        protected override string ClassName => "author";
        protected override string PrimaryKey => "id_author";

        string Name, SecondName, LastName;
        public Author() { }
        public Author(string name, string second_name, string last_name) 
        {
            Name = name;
            SecondName = second_name;
            LastName = last_name;
        }
        protected override string InsertQuery => $"INSERT INTO {ClassName} (name_{ClassName}, second_name_{ClassName}, last_name_{ClassName}) VALUES ({Name}, {SecondName}, {LastName})";

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, second_name_{ClassName} AS Фамилия, name_{ClassName} AS Имя," +
            $" last_name_{ClassName} AS Отчество FROM {ClassName} ORDER BY publisher_id ASC";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"fio_uniq","\"Уникальность\"" } };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"last_name","\"Отчество\"" },
            new string[]{"second_name","\"Имя\""},
            new string[]{"name","\"Фамилия\""}
        };

        

    }
}
