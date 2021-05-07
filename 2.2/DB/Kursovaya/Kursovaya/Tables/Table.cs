using Npgsql;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System;
using System.Threading;

namespace Kursovaya
{
    abstract class Table
    {
        protected static string[] RandomNodeQuery => new string[]
            {"SELECT name_area FROM area ORDER BY RANDOM() LIMIT 1",
            "SELECT name_own FROM own ORDER BY RANDOM() LIMIT 1",
            "SELECT name_city FROM city ORDER BY RANDOM() LIMIT 1",
            "SELECT name_lang FROM lang ORDER BY RANDOM() LIMIT 1",
            "SELECT publisher_name FROM publisher ORDER BY RANDOM() LIMIT 1",
            "SELECT name_style FROM style ORDER BY RANDOM() LIMIT 1",
            "SELECT name_binding FROM binding ORDER BY RANDOM() LIMIT 1",
            "SELECT name_author, second_name_author, last_name_author FROM author ORDER BY RANDOM() LIMIT 1",
            "SELECT book_name FROM book ORDER BY RANDOM() LIMIT 1",
            "SELECT shop_name FROM shop ORDER BY RANDOM() LIMIT 1",
            };
        protected string TruncateQuery => $"TRUNCATE TABLE {ClassName} RESTART IDENTITY CASCADE";
        protected virtual string[] FileGeneratorPath => new string[] { ($"{ClassName}/{ClassName}.txt") };
        protected abstract string ClassName { get; }
        protected abstract string PrimaryKey { get; }
        protected abstract string InsertQuery { get; }
        protected abstract string SelectQuery { get; }
        protected abstract string UpdateQuery { get; }
        protected  string DeleteQuery => $"DELETE FROM {ClassName} WHERE {PrimaryKey} = ";
        protected abstract List<string[]> Constraint { get; }
        protected abstract List<string[]> ColumnError { get; }
        protected void Truncate()
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(TruncateQuery, connect);
                    command.ExecuteNonQuery();
                }
                catch{}
                connect.Close();
            }
        }
        public static void TruncateAll()
        {
            new Area().Truncate();
            new Own().Truncate();
            new Binding().Truncate();
            new City().Truncate();
            new Style().Truncate();
            new Author().Truncate();
            new BookAuthor().Truncate();
            new Shop().Truncate();
            new Publisher().Truncate();
            new Book().Truncate();
            new BookAuthor().Truncate();
            new Deliveries().Truncate();
            Message.Success();
        }
        public NpgsqlDataAdapter Select()
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlDataAdapter nda = new NpgsqlDataAdapter(SelectQuery, SQL.GetConnection());
                connect.Close();
                return nda;
            }
        }
        public virtual bool Insert() {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                bool result;
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(InsertQuery, connect);
                    command.ExecuteNonQuery();
                    Message.Success();
                    result = true;
                }
                catch (Npgsql.PostgresException ex) { 
                    SQLError(ex); result = false; }
                connect.Close();
                return result;
            }
        }
        public void  Update() { }
        public void  Delete(int[] id) 
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                foreach (int ID in id) {
                    try
                    {
                        NpgsqlCommand command = new NpgsqlCommand(DeleteQuery + ID, connect);
                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException ex) { SQLError(ex); }
                }
                connect.Close();
                Message.Success();
            }
        }
        protected void ValidateError(Npgsql.PostgresException ex) { }
        protected void ValidateConstraint(string error) 
        {
            foreach (string[] temp in Constraint)
            {
                if (error.Contains(temp[0]))
                {
                    ErrorShow($"Неверно указано поле {temp[1]}.");
                    break;
                }
            }
        }
        protected void ValidateColumn(string error)
        {
            foreach (string[] temp in ColumnError)
            {
                if (error.Contains(temp[0]))
                {
                    ErrorShow($"Неверно указано поле {temp[1]}.");
                    break;
                }
            }
        }
        public void SQLError(Npgsql.PostgresException ex)
        {
            if (ex.ConstraintName != null)
                ValidateConstraint(ex.ConstraintName);
            else if (ex.ColumnName != null)
                ValidateColumn(ex.ColumnName);
            else ErrorShow("Не все поля заполнены верно.");

        }
        public static void ErrorShow(string msg) { MessageBox.Show(msg, "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        public static Table ReturnTable(string name)
        {
            //Основные таблицы
            if (name == "Магазины")
                return new Shop();
            else if (name == "Издательства")
                return new Publisher();
            else if (name == "Поставки")
                return new Deliveries();
            else if (name == "Книги")
                return new Book();
            else if (name == "Книги-авторы")
                return new BookAuthor();
            //Справочники
            else if (name == "Район")
                return new Area();
            else if (name == "Город")
                return new City();
            else if (name == "Язык")
                return new Lang();
            else if (name == "Тип собственности")
                return new Own();
            else if (name == "Жанр")
                return new Style();
            else if (name == "Тип переплета")
                return new Binding();
            else return new Author();
        }
        public bool FileExist()
        {
            foreach (string filename in FileGeneratorPath)
            {
                if (!File.Exists(filename))
                    return false;
            }
            return true;
        }
        delegate void Something(int count);
        void ss(int count) { }
        public static void Generate()
        {
            Message.OperationStart();
            new Area().GenerateTable(100);
            new Lang().GenerateTable(100);
            new Own().GenerateTable(100);
            new Binding().GenerateTable(100);
            new City().GenerateTable(100);
            new Style().GenerateTable(10);
            new Shop().GenerateTable(10);
            new Publisher().GenerateTable(20);
            new Book().GenerateTable(20);
            new Author().GenerateTable(20);
            new BookAuthor().GenerateTable(20);
            new Deliveries().GenerateTable(20);
            
        }
        protected void GenerateTable(int count)
        {
            if (!FileExist())
            {
                ErrorShow($"Ошибка создание записей таблицы \"{ClassName}\". Файла генерации не существует");
                return;
            }

            //Создание записей
            for(int i = 0; i < count; i++)
            {
                GenerateNode();

                //Добавление записи
                using (NpgsqlConnection connect = SQL.GetConnection())
                {
                    connect.Open();
                    try
                    {
                        NpgsqlCommand command = new NpgsqlCommand(InsertQuery, connect);
                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException ex) { }
                    connect.Close();
                }
            }
        }
        protected abstract void GenerateNode(); // Создает из строчки параметры
        protected virtual string GenerateLine(string path) //Выбирает строчку из файла
        {
            StreamReader sr = new StreamReader(path);
            try
            {
                Random rand = new Random();
                int i = rand.Next(File.ReadAllLines(path).Length);

                while (i > 0)
                {
                    sr.ReadLine();
                    i--;
                }
            }
            catch { }
            return InputData.CheckString(sr.ReadLine());
        }
        protected virtual string GetNodeFromOtherTable(int i)
        {
            if (i >= RandomNodeQuery.Length)
                return "NULL";


            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                string line = null;
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(RandomNodeQuery[i], connect);
                    command.ExecuteNonQuery().ToString();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            line = reader.GetString(0);
                        }
                        catch {};
                    }
                }
                catch { }
                connect.Close();
                if (line != "NULL")
                    return InputData.CheckString(line);
                else return "NULL";
            }
        }

    }
}
