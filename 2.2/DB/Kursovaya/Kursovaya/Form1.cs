using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SQL sql;
        private void Form1_Load(object sender, EventArgs e)
        {
            sql = new SQL();
            using(NpgsqlConnection connect = sql.GetConnection())
            {
                connect.Open();
                if (connect.State != ConnectionState.Open)
                {
                    MessageBox.Show("Невозможно подключиться к базе данных", "Error 001", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();

        }
    }
}
