using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    class SQL
    {
        static string login, password;//postgres, 123321
        public SQL() { }
        public SQL(string login, string password) 
        {
            SQL.login = login;
            SQL.password = password;
        }
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id="+login+";Password="+password+";Database=Kursovaya;");
        }
        
        public static void SQLErrors(string error)
        {
            if (error == "shop_uniq")
                MessageBox.Show("Данный магазин уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "area_name")
                MessageBox.Show("Данный район уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "fio_uniq")
                MessageBox.Show("Данный автор уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "binding_name")
                MessageBox.Show("Данный переплет уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "book_uniq")
                MessageBox.Show("Данная книга уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "book_author_uniq")
                MessageBox.Show("Данная запись уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "city_name")
                MessageBox.Show("Данный город уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "lang_name")
                MessageBox.Show("Данный язык уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "own_name")
                MessageBox.Show("Данный тип собственности уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (error == "style_name")
                MessageBox.Show("Данный жанр уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Success() { MessageBox.Show("Запись добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        public static NpgsqlDataAdapter ViewShop() 
        {
            return new NpgsqlDataAdapter("SELECT shop.shop_id as ID," +
                    "shop.shop_name AS Название," +
                    "shop.date_open AS \"Дата открытия\"," +
                    "area.name_area AS Район," +
                    "shop.address AS Адресс," +
                    "own.name_own AS \"Тип собственности\"" +
                    "FROM shop," +
                    "area, own WHERE shop.id_area = area.id_area AND shop.id_own = own.id_own", GetConnection());
        }
        public static NpgsqlDataAdapter ViewSup(string table, string name)
        {
            return new NpgsqlDataAdapter("SELECT id_" + table +" AS ID, name_" + table + " AS " + name +" FROM " + table, GetConnection());
        }
        public static DialogResult DeleteWarning()
        {
            return MessageBox.Show("ВНИМЕНИЕ! Все записи из других таблиц, которые связаны с этой записью будут удалены. Хотите удалить?", "Успех", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
        public static void DeleteSup(string table, string name, NpgsqlConnection connect)
        {
            try
            {
                new NpgsqlCommand("DELETE FROM " + table + " WHERE name_" + table + " = '" + name + "'", connect).ExecuteNonQuery();
            }
            catch { MessageBox.Show("Невозможно удалить запись с именем '" +name+"'.", "Ошибка011", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
