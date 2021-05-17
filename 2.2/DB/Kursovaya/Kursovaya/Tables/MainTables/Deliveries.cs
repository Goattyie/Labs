using System;
using System.Collections.Generic;

namespace Kursovaya
{
    class Deliveries : MainTable
    {
        string Shop, Book, Lang;
        bool PreOrder;
        string Date;
        Double Cost, Size, DefCost;
        int Count, Id;
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
        public Deliveries(int id, string shop, string book, string lang, int count, string date, double cost, double size, double defcost, bool preorder)
        {
            Id = id;
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

        public override string ClassName => "deliveries";
        protected override string InsertQuery => $"INSERT INTO deliveries (id_shop, id_book, count_book, date_come, cost, id_lang, size, pre_order, def_cost) VALUES (" +
            $"(SELECT shop.id FROM shop WHERE shop.name = {Shop} LIMIT 1), (SELECT book.id FROM book WHERE book.name = {Book} LIMIT 1)," +
            $"{Count}, {Date}, {Cost}, (SELECT lang.id FROM lang WHERE lang.name = {Lang}), {Size}, {PreOrder}, {DefCost})";

        protected override string SelectQuery => "SELECT d.id id, s.name Магазин, b.name Книга, d.count_book Количество, d.date_come \"Дата поступления\", d.cost \"Цена для магазина\"," +
            " d.def_cost \"Цена для поставщика\", l.name Язык, d.size Объем, d.pre_order Предзаказ FROM deliveries d " +
        "JOIN shop s ON d.id_shop = s.id " +
        "JOIN book b ON d.id_book = b.id " +
        "JOIN lang l ON d.id_lang = l.id;";
        protected override string UpdateQuery => $"UPDATE {ClassName} SET id_shop = (SELECT id FROM shop WHERE name = {Shop} LIMIT 1), id_book = (SELECT id FROM book WHERE name = {Book}  LIMIT 1), count_book = {Count}, date_come = {Date}, cost = {Cost}, id_lang = (SELECT id FROM lang WHERE name = {Lang}), size = {Size}, pre_order = {PreOrder}, def_cost = {DefCost} WHERE id = {Id};";
        protected override List<string[]> Constraint => new List<string[]> {
            new string[]{"count","\"Количество экземпляров\""},
            new string[]{"ck_cost_value","\"Цена (для магазина)\""},
            new string[]{"ck_size_value", "\"Объем\""},
            new string[]{"ck_defcost_value", "\"Цена (для поставщиков)\""},
            new string[]{"ck_date_come","\"Дата поступления\""},
            new string[]{"deliver_uniq","\"Уникальность\""}
        };
        protected override List<string[]> ColumnError => new List<string[]> {
            new string[]{"shop","\"Магазин\""},
            new string[]{"book","\"Книга\""},
            new string[]{"lang", "\"Язык\""},
        };
        protected override string[][] TableNames => new string[][]
        {
            new string[]{ "id", "id"},
            new string[]{ "Магазин", "id_shop"},
            new string[]{ "Книга", "id_book"},
            new string[]{ "Количество", "count_book"},
            new string[]{ "Дата поступления", "date_come"},
            new string[]{ "Цена для магазина", "cost"},
            new string[]{ "Цена для поставщика", "def_cost"},
            new string[]{ "Язык", "id_lang"},
            new string[]{ "Объем", "size"},
            new string[]{ "Предзаказ", "pre_order"}
        };
    }
}
