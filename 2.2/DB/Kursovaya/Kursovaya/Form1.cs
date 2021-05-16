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
            textBox1.Text = "postgres";
            textBox2.Text = "123321";
        }
        SQL sql;

        private void button1_Click(object sender, EventArgs e)
        {
            sql = new SQL(textBox1.Text, textBox2.Text);
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                try
                {
                    connect.Open();
                    if (connect.State != ConnectionState.Open)
                    {
                        Message.AutorizationError();
                        Application.Exit();
                    }
                    connect.Close();
                    Main main = new Main();
                    main.Show();
                    this.Hide();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка 000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
