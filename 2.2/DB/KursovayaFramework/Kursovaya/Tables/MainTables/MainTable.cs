using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    abstract class MainTable: Table
    {
        protected virtual string UpdateQuery { get; } //+
        public void Update()
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(UpdateQuery, connect);
                    command.ExecuteNonQuery();
                    Message.Success();
                }
                catch (Npgsql.PostgresException ex)
                {
                    SQLError(ex);
                }
                connect.Close();
            }
        }//+
        public string ConvertColumnName(string ColumnName)
        {
            if (ColumnName == "id")
                return null;
            foreach(string[] item in TableNames)
            {
                if (ColumnName == item[0] && item[1].Contains("id"))
                    return item[1].Split('_')[1];
            }
            return null;
        }
        private string ConvertIdToQuery(string column, string value)
        {
            string TableName = column.Split('_')[1];
            return $"DELETE FROM {ClassName} USING {TableName} WHERE {ClassName}.id_{TableName} IN (SELECT id FROM {TableName} WHERE name = '{value}')";
        }
        public bool ColumnDelete(string tableName, string value)
        {
            for(int i = 0; i < TableNames.Length; i++)
            {
                if (tableName != TableNames[i][0])
                    continue;

                string Query;
                if (TableNames[i][1] != "id" && TableNames[i][1].Contains("id"))
                    Query = ConvertIdToQuery(TableNames[i][1], value);
                else Query = $"DELETE FROM {ClassName} WHERE {TableNames[i][1]} = '{value}'";

                using (NpgsqlConnection connect = SQL.GetConnection())
                {
                    connect.Open();
                    try
                    {
                        NpgsqlCommand command = new NpgsqlCommand(Query, connect);
                        command.ExecuteNonQuery();
                        Message.Success();
                    }
                    catch (Npgsql.PostgresException ex)
                    {
                        SQLError(ex);
                        connect.Close();
                        return false;
                    }
                    connect.Close();
                }
                return true;
            }
            return true;
        }
        protected virtual string[][] TableNames { get; } = new string[][]
        {
           new string[]{"id", "id"},
           new string[]{"Название", "name"}
        };
        public static MainTable ReturnMainTable(string name) 
        {
            if (name == "Магазины")
                return new Shop();
            else if (name == "Издательства")
                return new Publisher();
            else if (name == "Поставки")
                return new Deliveries();
            else if (name == "Книги")
                return new Book();
            else
                return new BookAuthor();
        }
    }
}
