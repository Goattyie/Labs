using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Book : MainTable
    {

        public override string ClassName => "book";
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
            string[] filename = photo.Split('\\');
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
            Photo = InputData.CheckString(newName);
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
            string[] filename = photo.Split('\\');
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
            Photo = InputData.CheckString(newName);
        }
        protected override string InsertQuery => $"INSERT INTO book (name, photo, description, id_lang, date_create, id_publisher, id_style, id_binding, date_public) VALUES " +
                        $"({Name}, {Photo}, {Description}, " +
                        $"(SELECT id FROM lang WHERE name = {this.Lang}), {Date}, " +
                        $"(SELECT id FROM publisher WHERE name = {this.Publisher} LIMIT 1)," +
                        $"(SELECT id FROM style WHERE name = {this.Style})," +
                        $"(SELECT id FROM binding WHERE name = {this.Binding}), {PublishDate})";

        protected override string SelectQuery => $"SELECT b.id id, b.name Название, b.photo Фото, b.description Описание, l.name Язык,  p.name Издательство, s.name Жанр, bind.name Переплет, " +
            $"b.date_create \"Дата создания\", b.date_public \"Дата издания\" FROM book b " +
                    "JOIN lang l ON b.id_lang = l.id "+
                    "JOIN publisher p ON b.id_publisher = p.id "+
                    "JOIN style s ON b.id_style = s.id "+
                    "JOIN binding bind ON b.id_binding = bind.id";

        protected override string UpdateQuery => $"UPDATE book SET name = {Name},description = {Description}, id_lang = (SELECT id FROM lang WHERE name = {Lang}), id_publisher = (SELECT id FROM publisher WHERE name = {Publisher} LIMIT 1), id_style = (SELECT id FROM style WHERE name = {Style}), id_binding = (SELECT id FROM binding WHERE name = {Binding}), date_public = {PublishDate}, date_create = {Date}, photo = {Photo} WHERE id = {Id} ";

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
        protected override string[][] TableNames => new string[][]
                {
            new string[]{ "id", "id"},
            new string[]{ "Название", "name"},
            new string[]{ "Фото", "photo"},
            new string[]{ "Описание", "description"},
            new string[]{ "Язык", "id_lang"},
            new string[]{ "Издательство", "id_publisher"},
            new string[]{ "Жанр", "id_style"},
            new string[]{ "Переплет", "id_binding"},
            new string[]{ "Дата создания", "date_create"},
            new string[]{ "Дата издания", "date_public"},
                };
    }
}
