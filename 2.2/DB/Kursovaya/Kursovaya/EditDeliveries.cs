using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class EditDeliveries : Form
    {
        public EditDeliveries()
        {
            InitializeComponent();
        }

        private void EditDeliveries_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = InputData.CheckString(textBox1.Text);
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                try
                {
                    //Добавление записи в книгу
                    NpgsqlCommand command = new NpgsqlCommand("", connect);
                    command.ExecuteNonQuery();
                    SQL.Success();
                }
                catch (Npgsql.PostgresException ex) { SQL.SQLErrors(ex); }
                connect.Close();
            }
        }
    }
}
