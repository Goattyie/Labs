using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Command
    {
        private static string SelectShop = "SELECT shop.shop_id as ID," +
                    "shop.shop_name AS Название," +
                    "shop.date_open AS \"Дата открытия\"," +
                    "area.name_area AS Район," +
                    "shop.address AS Адресс," +
                    "own.name_own AS \"Тип собственности\"" +
                    "FROM shop," +
                    "area, own WHERE shop.id_area = area.id_area AND shop.id_own = own.id_own";

        private static string SelectPublisher = "SELECT publisher.publisher_id AS ID, " +
            "publisher.publisher_name as Название, city.name_city AS Город, " +
            "publisher.phone AS Телефон, publisher.create_date AS \"Дата создания\" " +
            "FROM publisher, city WHERE publisher.city_id = city.id_city";

        private static string SelectBook = "SELECT book.book_id AS ID, " +
            "book.book_name AS Название, " +
            "0 AS Фото," +
            " book.book_description AS Описание, " +
            "lang.name_lang AS Язык, " +
            "publisher.publisher_name AS Издательство, " +
            "style.name_style AS Жанр," +
            " binding.name_binding AS Переплет, " +
            "book.book_date AS \"Дата создания\", " +
            "book.book_date_public AS \"Дата публикации\" " +
            "FROM book, style, lang, binding, publisher " +
            "WHERE book.book_lang_id = lang.id_lang AND " +
            "book.book_publisher_id = publisher.publisher_id AND " +
            "book.book_style_id = style.id_style AND " +
            "book.book_binding_id = binding.id_binding";


        public static NpgsqlDataAdapter ViewSup(string table, string name)
        {
            return new NpgsqlDataAdapter("SELECT id_" + table + " AS ID, name_" + table + " AS " + name + " FROM " + table, SQL.GetConnection());
        }
        public static NpgsqlDataAdapter ViewMain(string name)
        {
            string request = null;
            if (name == "Магазины")
                request = SelectShop;
            else if (name == "Издательства")
                request = SelectPublisher;
            else if (name == "Книги")
                request = SelectBook;
                
            return new NpgsqlDataAdapter(request, SQL.GetConnection());
        }
    }
}
