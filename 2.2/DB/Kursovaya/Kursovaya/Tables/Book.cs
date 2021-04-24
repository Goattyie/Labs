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

        protected override string InsertQuery => throw new NotImplementedException();

        protected override string SelectQuery => $"SELECT {ClassName}.{ClassName}_id AS ID, " +
            $"{ClassName}.{ClassName}_name AS Название, " +
            $"0 AS Фото," +
            $" {ClassName}.{ClassName}_description AS Описание, " +
            $"lang.name_lang AS Язык, " +
            $"publisher.publisher_name AS Издательство, " +
            $"style.name_style AS Жанр," +
            $" binding.name_binding AS Переплет, " +
            $"{ClassName}.{ClassName}_date AS \"Дата создания\", " +
            $"{ClassName}.{ClassName}_date_public AS \"Дата публикации\" " +
            $"FROM {ClassName}, style, lang, binding, publisher " +
            $"WHERE {ClassName}.{ClassName}_lang_id = lang.id_lang AND " +
            $"{ClassName}.{ClassName}_publisher_id = publisher.publisher_id AND " +
            $"{ClassName}.{ClassName}_style_id = style.id_style AND " +
            $"{ClassName}.{ClassName}_binding_id = binding.id_binding";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => throw new NotImplementedException();

        protected override List<string[]> ColumnError => throw new NotImplementedException();
    }
}
