using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Shop : MainTable
    {
        protected override string ClassName => "shop";
        private int Id, Date;
        private string Name, Area, Address, Own;
        public Shop() { }
        public Shop(string name, int date, string area, string address, string own)
        {
            Name = name;
            Date = date;
            Area = area;
            Address = address;
            Own = own;
        }
        public Shop(int id, string name, int date, string area, string address, string own)
        {
            Id = id;
            Name = name;
            Date = date;
            Area = area;
            Address = address;
            Own = own;
        }

        protected override string SelectQuery => "SELECT s.id id, s.name Название, s.date_open \"Дата открытия\", s.address Адресс, a.name Район, o.name Собственность FROM shop s " +
            "JOIN area a ON s.id_area = a.id " +
            "JOIN own o ON s.id_own = o.id";
        protected override string InsertQuery => $"INSERT INTO shop (name, date_open, id_area, address, id_own) VALUES " +
                        $"({Name}, {Date}, " +
                        $"(SELECT area.id FROM area WHERE area.name = {Area}), " +
                        $"{Address}, (SELECT own.id FROM own WHERE own.name = {Own}))";
        protected override string UpdateQuery => $"UPDATE {ClassName} SET name = {Name}, id_area = (SELECT id FROM area WHERE name = {Area}), address = {Address}, id_own = (SELECT id FROM own WHERE name = {Own}), date_open = {Date} WHERE id = {Id}";

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"date_open","\"Дата открытия\""},
        };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Название\""},
            new string[]{"date_open","\"Дата открытия\""},
            new string[]{"own", "\"Тип собственности\""},
            new string[]{"address", "\"Адресс\""},
            new string[]{"area","\"Район\""},
            new string[]{ "UQ_shop", "\"Уникальность\"" }
        };

        protected override string[][] TableNames => new string[][] 
        { 
            new string[]{ "id", "id"},
            new string[]{ "Название", "name"},
            new string[]{ "Дата открытия", "date_open"},
            new string[]{ "Район", "id_area"},
            new string[]{ "Адресс", "address"},
            new string[]{ "Собственность", "id_own"}
        };
    }
}
