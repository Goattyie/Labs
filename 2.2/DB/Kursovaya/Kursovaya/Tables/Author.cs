using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Author : Table
    {
        protected override string ClassName => "author";

        protected override string InsertQuery => throw new NotImplementedException();

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, second_name_{ClassName} AS Фамилия, name_{ClassName} AS Имя," +
            $" last_name_{ClassName} AS Отчество FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => throw new NotImplementedException();

        protected override List<string[]> ColumnError => throw new NotImplementedException();
    }
}
