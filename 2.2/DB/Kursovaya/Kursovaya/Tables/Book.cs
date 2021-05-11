using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Book : Table
    {
        
        protected override string ClassName => "book";
        protected override string PrimaryKey => "book_id";
        string Name, Description, Lang, Publisher, Style, Binding;
        int PublishDate, Date;
        string Photo;
        public Book() { }
        public Book(string name, string photo, string description, string lang, int date, string publisher, string style, string binding, int pub_date)
        {
            Name = name;
            Photo = photo;
            Description = description;
            Lang = lang;
            Date = date;
            Publisher = publisher;
            Style = style;
            Binding = binding;
            PublishDate = pub_date;
        }
        protected override string InsertQuery => $"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES " +
                        $"({Name}, '{Photo}', {Description}, " +
                        $"(SELECT id_lang FROM lang WHERE name_lang = {this.Lang}), {Date}, " +
                        $"(SELECT publisher_id FROM publisher WHERE publisher_name = {this.Publisher} LIMIT 1)," +
                        $"(SELECT id_style FROM style WHERE name_style = {this.Style})," +
                        $"(SELECT id_binding FROM binding WHERE name_binding = {this.Binding}), {PublishDate})";

        protected override string SelectQuery => $"SELECT {ClassName}.{ClassName}_id AS ID, " +
            $"{ClassName}.{ClassName}_name AS Название, " +
            $"{ClassName}.{ClassName}_photo AS Фото," +
            $" {ClassName}.{ClassName}_description AS Описание, " +
            $"lang.name_lang AS Язык, " +
            $"publisher.publisher_name AS Издательство, " +
            $"style.name_style AS Жанр," +
            $" binding.name_binding AS Переплет, " +
            $"{ClassName}.{ClassName}_date AS \"Дата издания(автором)\", " +
            $"{ClassName}.{ClassName}_date_public AS \"Дата выпуска(издательством)\" " +
            $"FROM {ClassName}, style, lang, binding, publisher " +
            $"WHERE {ClassName}.{ClassName}_lang_id = lang.id_lang AND " +
            $"{ClassName}.{ClassName}_publisher_id = publisher.publisher_id AND " +
            $"{ClassName}.{ClassName}_style_id = style.id_style AND " +
            $"{ClassName}.{ClassName}_binding_id = binding.id_binding  ORDER BY book.book_id";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"UQ_book", "\"Название, Фото (Уникальность)\""},
            new string[]{"date_public", "\"Дата публикации\""},
            new string[]{"date", "\"Дата создания\""}};

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"name","\"Название\""},
            new string[]{"description","\"Описание\""},
            new string[]{"style", "\"Жанр\""},
            new string[]{"publisher", "\"Издательство\""},
            new string[]{"lang", "\"Язык\""},
            new string[]{"binding", "\"Тип переплета\""}
        };

    }
}
