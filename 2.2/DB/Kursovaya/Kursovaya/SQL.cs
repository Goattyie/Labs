using System;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    class SQL
    {
        static string login, password;//postgres, 123321
        public SQL() { }
        public SQL(string login, string password)
        {
            SQL.login = login;
            SQL.password = password;
        }
        public static NpgsqlConnection GetConnection()
        {
            //return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id="+login+";Password="+password+";Database=Kursovaya;");
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id=postgres;Password=123321;Database=Kursovaya;");
        }

    }
}
