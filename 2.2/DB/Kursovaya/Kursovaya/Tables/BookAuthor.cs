using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class BookAuthor : Table
    {
        protected override string ClassName => "book_author";
        protected override string PrimaryKey => "id_book_author";
        List<string[]> Authors = new List<string[]>();
        string[] Author;
        string BookName;
        public BookAuthor() { }
        public BookAuthor(string book_name, string[] authors)
        {
            
            BookName = book_name;
            foreach (string author in authors)
            {
                Authors.Add(author.Split(" "));
            }
        }
        protected override string InsertQuery => $"INSERT INTO book_author (id_book, id_author) VALUES " +
                            $"( (SELECT book_id FROM book WHERE book_name = {BookName})," +
                            $" ( SELECT id_author FROM author WHERE name_author = '{Author[1]}' AND second_name_author = '{Author[0]}' AND last_name_author = '{Author[2]}'))";
        public override bool Insert()
        {
            bool success = true;
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                for (int i = 0; i < Authors.Count; i++)
                {
                    Author = Authors[i];    
                    try
                    {
                        NpgsqlCommand command = new NpgsqlCommand(InsertQuery, connect);
                        command.ExecuteNonQuery();
                    }
                    catch (Npgsql.PostgresException ex) { SQLError(ex); success = false; }
                    
                }
                connect.Close();
            }
            return success;
        }

        protected override string SelectQuery => $"SELECT id_book_author AS ID, (SELECT book_name AS Книга FROM book WHERE book_id = book_author.id_book), (SELECT second_name_author AS Фамилия FROM author WHERE id_author = book_author.id_author), (SELECT name_author AS Имя FROM author WHERE id_author = book_author.id_author), (SELECT last_name_author AS Отчество FROM author WHERE id_author = book_author.id_author) FROM book_author";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{ "UQ_book_author", "\"Книга-автор (Уникальность)\""},
            };

        protected override List<string[]> ColumnError => null;

       
    }
}
