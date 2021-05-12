using System;
using System.Collections.Generic;
using System.IO;
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
        int PublishDate, Date, Id;
        string Photo;
        public Book() { }
        public Book(string name, string photo, string description, string lang, int date, string publisher, string style, string binding, int pub_date)
        {
            Name = name;
            Description = description;
            Lang = lang;
            Date = date;
            Publisher = publisher;
            Style = style;
            Binding = binding;
            PublishDate = pub_date;
            if (photo == null || photo == "NULL")
            {
                Photo = photo;
                return;
            }
            string[] filename = photo.Split("\\");
            string newName = null;
            try
            {
                newName = filename[filename.Length - 1];
                File.Copy(photo, $"images/{newName}");
            }
            catch 
            {
                //newName = $"images/{filename[filename.Length - 1]}{new Random().NextDouble()}";
                //File.Copy(photo, newName); 
            }
            Photo = newName;
        }
        public Book(int id, string name, string photo, string description, string lang, int date, string publisher, string style, string binding, int pub_date)
        {
            Id = id;
            Name = name;
            Description = description;
            Lang = lang;
            Date = date;
            Publisher = publisher;
            Style = style;
            Binding = binding;
            PublishDate = pub_date;
            if (photo == null || photo == "NULL")
            {
                Photo = photo;
                return;
            }
            string[] filename = photo.Split("\\");
            string newName = null;
            try
            {
                newName = filename[filename.Length - 1];
                File.Copy(photo, $"images/{newName}");
            }
            catch
            {
                //newName = $"images/{filename[filename.Length - 1]}{new Random().NextDouble()}";
                //File.Copy(photo, newName); 
            }
            Photo = newName;
        }
        protected override string InsertQuery => $"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES " +
                        $"({Name}, {Photo}, {Description}, " +
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

        protected override string UpdateQuery => $"UPDATE book SET book_name = {Name},book_description = {Description}, book_lang_id = (SELECT id_lang FROM lang WHERE name_lang = {Lang}), book_publisher_id = (SELECT publisher_id FROM publisher WHERE publisher_name = {Publisher} LIMIT 1), book_style_id = (SELECT id_style FROM style WHERE name_style = {Style}), book_binding_id = (SELECT id_binding FROM binding WHERE name_binding = {Binding}), book_date_public = {PublishDate}, book_date = {Date}, book_photo = {Photo} WHERE book_id = {Id} ";

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
