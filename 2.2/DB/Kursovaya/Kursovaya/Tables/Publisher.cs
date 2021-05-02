using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Publisher : Table
    {
        protected override string ClassName => "publisher";
        protected override string PrimaryKey => "publisher_id";

        string Name, Telephone, City;
        int Date;
        public Publisher() { }
        public Publisher(string name, string city, string telephone, int date)
        {
            Name = name;
            City = city;
            Telephone = telephone;
            Date = date;
        }
        protected override string InsertQuery => $"INSERT INTO publisher (publisher_name, city_id, phone, create_date) VALUES " +
                            $"({Name}, (SELECT id_city FROM city WHERE name_city = {City}), {Telephone} , {Date})";

        protected override string SelectQuery => $"SELECT {ClassName}.{ClassName}_id AS ID, " +
            $"{ClassName}.{ClassName}_name as Название, city.name_city AS Город, " +
            $"{ClassName}.phone AS Телефон, {ClassName}.create_date AS \"Дата создания\" " +
            $"FROM {ClassName}, city WHERE {ClassName}.city_id = city.id_city";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"ck_create_date","\"Дата создания\""},
            new string[]{ "UQ_publisher_name", "\"Название (Уникальность)\"" }
        };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Название\""},
            new string[]{"phone","\"Телефон\""},
            new string[]{"city", "\"Город\""}
        };

        protected override void GenerateNode()
        {
            Name = GenerateLine(FileGeneratorPath[0]);
            City = GetNodeFromOtherTable(2);
            Date = new Random().Next(1900, 2021);
            Telephone = InputData.CheckString(new Random().Next(100, 9999).ToString() + new Random().Next(100, 9999).ToString() + new Random().Next(100, 9999).ToString() + new Random().Next(100, 9999).ToString());
        }
    }
}
