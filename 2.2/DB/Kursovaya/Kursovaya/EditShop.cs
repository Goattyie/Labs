using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    public partial class EditShop : Form
    {
        public EditShop()
        {
            InitializeComponent();
        }


        private void Clear()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.comboBox2.Items.Clear();
            this.comboBox1.Items.Clear();
            own = null;
            address = null;
            name = null;
            area = null;
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            area = InputData.CheckString(comboBox1.Text);
            own = InputData.CheckString(comboBox2.Text);

            bool success;
            if (!InputData.CheckInt(textBox3.Text, "\"Дата открытия\""))
                return;

            name = InputData.CheckString(textBox1.Text);
            address = InputData.CheckString(textBox2.Text);

            success = new Shop(name, Convert.ToInt32(textBox3.Text), area, address, own).Insert();
            if(success)this.Clear();
        }
        string area = "NULL", own = "NULL", name, address;

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_own FROM own;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
                connect.Close();
            }
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_area FROM area;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
                }
                connect.Close();
            }
        }
    }
}
