using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class BookAuthor : MainTable
    {
        public override string ClassName => "book_author";
        List<string[]> Authors = new List<string[]>();
        string[] Author;
        string BookName;
        int BookId;
        public BookAuthor() { }
        public BookAuthor(string book_name, string[] authors)
        {
            BookName = book_name;
            foreach (string author in authors)
            {
                Authors.Add(author.Split(" "));
            }
        }
        public BookAuthor(int id, string[] authors)
        {
            BookId = id;
            foreach (string author in authors)
            {
                Authors.Add(author.Split(" "));
            }
        }
        protected override string InsertQuery => $"INSERT INTO book_author (id_book, id_author) VALUES " +
                            $"( (SELECT id FROM book WHERE name = {BookName} LIMIT 1)," +
                            $" ( SELECT id FROM author WHERE name = '{Author[1]}' AND second_name = '{Author[0]}' AND last_name = '{Author[2]}'))";
        protected override string SelectQuery => "SELECT ba.id id, b.name Книга, a.second_name Фамилия, a.name Имя, a.last_name Отчество FROM book_author ba "+
        "JOIN book b ON ba.id_book = b.id "+
        "JOIN author a ON ba.id_author = a.id";
        protected override List<string[]> Constraint => new List<string[]> {new string[]{ "UQ_book_author", "\"Книга-автор (Уникальность)\""},};
        protected override List<string[]> ColumnError => null;
        protected override string[][] TableNames => throw new NotImplementedException();
        public void InsertAllAuthors()
        {
            for(int i = 0; i < Authors.Count; i++)
            {
                Author = Authors[i];
                Insert();
            }
        }
    }
}
