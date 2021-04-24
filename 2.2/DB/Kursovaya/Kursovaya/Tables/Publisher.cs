using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Publisher : Table
    {
        protected override string ClassName => "publisher";

        protected override string InsertQuery => throw new NotImplementedException();

        protected override string SelectQuery => $"SELECT {ClassName}.{ClassName}_id AS ID, " +
            $"{ClassName}.{ClassName}_name as Название, city.name_city AS Город, " +
            $"{ClassName}.phone AS Телефон, {ClassName}.create_date AS \"Дата создания\" " +
            $"FROM {ClassName}, city WHERE {ClassName}.city_id = city.id_city";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => throw new NotImplementedException();

        protected override List<string[]> ColumnError => throw new NotImplementedException();
    }
}
