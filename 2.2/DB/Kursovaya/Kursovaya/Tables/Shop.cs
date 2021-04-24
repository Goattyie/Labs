using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Shop : Table
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

        protected override string SelectQuery => "SELECT shop.shop_id as ID," +
                    "shop.shop_name AS Название," +
                    "shop.date_open AS \"Дата открытия\"," +
                    "area.name_area AS Район," +
                    "shop.address AS Адресс," +
                    "own.name_own AS \"Тип собственности\"" +
                    "FROM shop," +
                    "area, own WHERE shop.id_area = area.id_area AND shop.id_own = own.id_own";
        protected override string InsertQuery => $"INSERT INTO shop (shop_name, date_open, id_area, address, id_own) VALUES " +
                        $"({Name}, {Date}, " +
                        $"(SELECT area.id_area FROM area WHERE area.name_area = {Area}), " +
                        $"{Address}, (SELECT own.id_own FROM own WHERE own.name_own = {Own}))";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> { 
            new string[]{"",""}, 
        };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Название\""},
            new string[]{"date_open","\"Дата открытия\""},
            new string[]{"own", "\"Тип собственности\""},
            new string[]{"address", "\"Адресс\""},
            new string[]{"area","\"Район\""},
        };
    }
}
