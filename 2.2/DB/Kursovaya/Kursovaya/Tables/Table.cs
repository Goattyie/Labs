using Npgsql;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Kursovaya
{
    abstract class Table
    {
        protected abstract string ClassName { get; }
        protected abstract string PrimaryKey { get; }
        protected abstract string InsertQuery { get; }
        protected abstract string SelectQuery { get; }
        protected abstract string UpdateQuery { get; }
        protected  string DeleteQuery => $"DELETE FROM {ClassName} WHERE {PrimaryKey} = ";
        protected abstract List<string[]> Constraint { get; }
        protected abstract List<string[]> ColumnError { get; }
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
                catch (Npgsql.PostgresException ex) { SQLError(ex); result = false; }
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
            else ErrorShow("Значение одного из полей слишком велико");

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
        
    }
}
