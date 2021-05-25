﻿using Npgsql;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace Kursovaya
{
    abstract class Table
    {
        protected string TruncateQuery => $"TRUNCATE TABLE {ClassName} RESTART IDENTITY CASCADE"; //+
        protected string FileGeneratorPath => $"sql/{ClassName}.sql";
        public abstract string ClassName { get; }//+
        protected string PrimaryKey => "id";//+
        protected abstract string InsertQuery { get;}  //+
        protected abstract string SelectQuery { get; }//+
        protected string DeleteQuery => $"DELETE FROM {ClassName} WHERE {PrimaryKey} = ";//+
        protected abstract List<string[]> Constraint { get; }//+
        protected abstract List<string[]> ColumnError { get; } //+
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
                catch { }
                connect.Close();
            }
        }//+
        public static void TruncateAll()//+
        {
            new Area().Truncate();
            new Own().Truncate();
            new Binding().Truncate();
            new City().Truncate();
            new Style().Truncate();
            new Author().Truncate();
            new Lang().Truncate();
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
        }//+
        public bool Insert()
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                bool result;
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(InsertQuery, connect);
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Npgsql.PostgresException ex)
                {
                    SQLError(ex); result = false;
                }
                connect.Close();
                return result;
            }
        }//+
        public void Delete(int[] id)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                foreach (int ID in id)
                {
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
        }//+
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
        }//+
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
        }//+
        public void SQLError(Npgsql.PostgresException ex)
        {
            if (ex.ConstraintName != null)
                ValidateConstraint(ex.ConstraintName);
            else if (ex.ColumnName != null)
                ValidateColumn(ex.ColumnName);
            else ErrorShow("Не все поля заполнены верно.");

        }//+
        public static void ErrorShow(string msg) { MessageBox.Show(msg, "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error); }//+
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
            else if(name == "Книги-авторы")
                return new BookAuthor();
            //Справочники
                else if (name == "Район")
                return new Area();
            else if (name == "Автор")
                return new Author();
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
        public static void Generate()
        {
            if (Message.Warning("Для генерации данных все записи будут удалены. Продолжить?") == DialogResult.Cancel)
                return;

            TruncateAll();
            new Area().GenerateTable();
            new Lang().GenerateTable();
            new Own().GenerateTable();
            new Binding().GenerateTable();
            new City().GenerateTable();
            new Style().GenerateTable();
            new Shop().GenerateTable();
            new Publisher().GenerateTable();
            new Book().GenerateTable();
            new Author().GenerateTable();
            new BookAuthor().GenerateTable();
            new Deliveries().GenerateTable();
            Message.Success();

        }//+
        protected bool GenerateTable()
        {
            if (!File.Exists(FileGeneratorPath))
            {
                Message.ErrorShow("Присутствуют не все файлы генерации.");
                return false;
            }
            string GenerateQuery = File.ReadAllText(FileGeneratorPath);

            if (GenerateQuery == null)
            {
                Message.ErrorShow("Один из файлов генерации пуст.");
                return false;
            }

            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(GenerateQuery, connect);
                    command.ExecuteNonQuery();
                }
                catch (Npgsql.PostgresException ex) { }
                connect.Close();
            }
            return true;
        }//+
    }
}
