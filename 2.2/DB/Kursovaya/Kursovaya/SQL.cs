using System;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    class SQL
    {
        static string login, password;//postgres, 123321
        static string[][] regex = new string[6][]
        { new string[2]{ "name", "\"Название\"" },
            new string[2] { "date_open", "\"Дата открытия\"" },
            new string[2] {"area", "\"Район\""},
        new string[2] {"own", "\"Тип собственности\""},
        new string[2] {"city", "\"Город\""},
        new string[2] {"phone", "\"Телефон\""}};

        public SQL() { }
        public SQL(string login, string password) 
        {
            SQL.login = login;
            SQL.password = password;
        }
        public static NpgsqlConnection GetConnection()
        {
            //return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id="+login+";Password="+password+";Database=Kursovaya;");
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=123321;Database=Kursovaya;");
        }
        public static void ErrorShow(string msg) { MessageBox.Show(msg, "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        private static void ConstraintError(string error)
        {
            if (error == "shop_uniq")
                ErrorShow("Данный магазин уже существует");
            else if (error == "area_name")
                ErrorShow("Данный район уже существует");
            else if (error == "fio_uniq")
                ErrorShow("Данный автор уже существует");
            else if (error == "binding_name")
                ErrorShow("Данный переплет уже существует");

            else if (error == "book_uniq")
                ErrorShow("Данная книга уже существует");
            else if (error == "book_author_uniq")
                ErrorShow("Данная запись уже существует");

            else if (error == "city_name")
                ErrorShow("Данный город уже существует");

            else if (error == "lang_name")
                ErrorShow("Данный язык уже существует");

            else if (error == "own_name")
                ErrorShow("Данный тип собственности уже существует");
            else if (error == "style_name")

                ErrorShow("Данный жанр уже существует");
        }
        private static void ColumnError(string error)
        {
            foreach(string[] temp in regex)
            {
                if (error.Contains(temp[0]))
                {
                    ErrorShow($"Неверно указано поле {temp[1]}.");
                    break;
                }
            }
        }
        public static void SQLErrors(Npgsql.PostgresException ex)
        {
            string error;
            if (ex.ConstraintName != null)
                ConstraintError(ex.ConstraintName);
            else if (ex.ColumnName != null)
                ColumnError(ex.ColumnName);
            else ErrorShow("Значение одного из полей слишком велико");

        }
        public static void Success() { MessageBox.Show("Запись добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        
        
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
