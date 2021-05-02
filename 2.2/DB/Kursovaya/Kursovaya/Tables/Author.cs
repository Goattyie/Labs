using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            $" last_name_{ClassName} AS Отчество FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => throw new NotImplementedException();

        protected override List<string[]> ColumnError => throw new NotImplementedException();

        protected override string[] FileGeneratorPath => new string[] { "author/Male/Name.txt", "author/Male/SName.txt", "author/Male/LName.txt",
            "author/Female/Name.txt", "author/Female/SName.txt", "author/Female/LName.txt", };
        protected override void GenerateNode()
        {
            Random rand = new Random();
            int i = 0;
            if (rand.Next(100) % 2 == 0)
                i += 3;
            Name = GenerateLine(FileGeneratorPath[i]);
            SecondName = GenerateLine(FileGeneratorPath[i + 1]);
            LastName = GenerateLine(FileGeneratorPath[i + 2]);
        }
    }
}
