using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    class Own : Table
    {
        protected override string ClassName => "own";

        protected override string InsertQuery => throw new NotImplementedException();

        protected override string SelectQuery => $"SELECT id_{ClassName} AS ID, name_{ClassName} AS \"Тип собственности\" FROM {ClassName}";

        protected override string UpdateQuery => throw new NotImplementedException();

        protected override List<string[]> Constraint => throw new NotImplementedException();

        protected override List<string[]> ColumnError => throw new NotImplementedException();
    }
}
