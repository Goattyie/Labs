using System;
using System.Collections.Generic;

namespace Kursovaya
{
    class Deliveries : Table
    {
        string Shop, Book, Lang;
        bool PreOrder;
        string Date;
        Double Cost, Size, DefCost;
        int Count;
        public Deliveries() { }
        public Deliveries(string shop, string book, string lang, int count, string date, double cost, double size, double defcost, bool preorder)
        {
            Shop = shop;
            Book = book;
            Lang = lang;
            Date = date;
            Cost = cost;
            Size = size;
            DefCost = defcost;
            Count = count;
            PreOrder = preorder;
        }
        
        protected override string ClassName => "deliveries";
        protected override string PrimaryKey => "deliveries_id";

        protected override string InsertQuery => $"INSERT INTO deliveries (shop_id, book_id, book_count, date_come, cost, lang_id, size, pre_order, def_cost) VALUES (" +
            $"(SELECT shop.shop_id FROM shop WHERE shop.shop_name = {Shop} LIMIT 1), (SELECT book.book_id FROM book WHERE book.book_name = {Book} LIMIT 1)," +
            $"{Count}, {Date}, {Cost}, (SELECT lang.id_lang FROM lang WHERE lang.name_lang = {Lang}), {Size}, {PreOrder}, {DefCost})";

        protected override string SelectQuery => "SELECT deliveries_id AS ID, (SELECT shop_name FROM shop WHERE shop_id = deliveries.shop_id) AS Магазин," +
            "(SELECT book_name FROM book WHERE book_id = deliveries.book_id) AS Книга," +
            "book_count AS Количество," +
            "date_come AS \"Дата поступления\"," +
            "cost AS \"Цена для магазина\"," +
            "def_cost AS \"Цена для поставщика\"," +
            "(SELECT name_lang FROM lang WHERE id_lang = deliveries.lang_id) AS Язык," +
            "size AS Объем," +
            "pre_order AS Предзаказ FROM deliveries";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"count","\"Количество экземпляров\""},
            new string[]{"ck_cost_value","\"Цена (для магазина)\""},
            new string[]{"ck_size_value", "\"Объем\""},
            new string[]{"ck_defcost_value", "\"Цена (для поставщиков)\""},
            new string[]{"ck_date_come","\"Дата поступления\""},
        };

        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"shop","\"Магазин\""},
            new string[]{"book","\"Книга\""},
            new string[]{"lang", "\"Язык\""},
        };
      

    }
}
