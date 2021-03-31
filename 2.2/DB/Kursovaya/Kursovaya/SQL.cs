using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Server=localhost;Port=5432;User Id="+login+";Password="+password+";Database=Kursovaya;");
        }
        
        public static void SQLErrors(string er_name)
        {
            if (er_name == "shop_uniq")
                MessageBox.Show("Данный магазин уже существует", "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
