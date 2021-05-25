using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Publisher : MainTable
    {
        public override string ClassName => "publisher";
        string Name, Telephone, City;
        int Date, Id;
        public Publisher() { }
        public Publisher(string name, string city, string telephone, int date)
        {
            Name = name;
            City = city;
            Telephone = telephone;
            Date = date;
        }
        public Publisher(int id, string name, string city, string telephone, int date)
        {
            Id = id;
            Name = name;
            City = city;
            Telephone = telephone;
            Date = date;
        }
        protected override string InsertQuery => $"INSERT INTO publisher (name, id_city, phone, date_create) VALUES " +
                            $"({Name}, (SELECT id FROM city WHERE name = {City}), {Telephone} , {Date})";

        protected override string SelectQuery => "SELECT p.id id, p.name Название, c.name Город, p.phone Телефон, p.date_create \"Дата создания\" FROM publisher p "+
"                           JOIN city c ON p.id_city = c.id";

        protected override string UpdateQuery => $"UPDATE {ClassName} SET name = {Name}, id_city = (SELECT id FROM city WHERE name = {City}), phone = {Telephone}, date_create = {Date} WHERE id = {Id}";

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"ck_create_date","\"Дата создания\""},
            new string[]{ "UQ_publisher_name", "\"Название (Уникальность)\"" }
        };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Название\""},
            new string[]{"phone","\"Телефон\""},
            new string[]{"city", "\"Город\""}
        };
        protected override string[][] TableNames => new string[][]
        {
            new string[]{ "id", "id"},
            new string[]{ "Город", "id_city"},
            new string[]{ "Телефон", "phone"},
            new string[]{ "Дата создания", "date_create"},
        };
    }
}
