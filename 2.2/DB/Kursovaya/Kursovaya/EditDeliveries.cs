using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class EditDeliveries : Form
    {
        public EditDeliveries()
        {
            InitializeComponent();

        }
        string Shop, Book, Lang, Date;
        int Count;
        double Cost, FirstCost, Volume;

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox3.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_lang FROM lang", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox3.Items.Add($"{reader.GetString(0)}");
                }
                connect.Close();
            }
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox2.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT book_name FROM book", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add($"{reader.GetString(0)}");
                }
                connect.Close();
            }
        }
        bool PreOder = false;

       

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT shop_name FROM shop", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add($"{reader.GetString(0)}");
                }
                connect.Close();
            }
        }

 

        private void button4_Click(object sender, EventArgs e)
        {
            if(!InputData.CheckDouble(textBox1.Text, "Цена книги(для магазина)"))
                return;
            if (!InputData.CheckDouble(textBox2.Text, "Цена книги(поставщика)"))
                return;
            if (!InputData.CheckDouble(textBox3.Text, "Объем"))
                return;
            if (radioButton1.Checked == true)
                PreOder = true;
            else PreOder = false;

            Shop = InputData.CheckString(comboBox1.Text);
            Book = InputData.CheckString(comboBox2.Text);
            Lang = InputData.CheckString(comboBox3.Text);

            Cost = Convert.ToDouble(textBox1.Text);
            FirstCost = Convert.ToDouble(textBox2.Text);
            Date = InputData.CheckString(dateTimePicker1.Value.ToShortDateString());
            Count = Convert.ToInt32(numericUpDown1.Value);
            Volume = Convert.ToDouble(textBox3.Text);

            bool success = new Deliveries(Shop, Book, Lang, Count, Date, Cost, Volume, FirstCost, PreOder).Insert();


            if (success)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = null;
                comboBox2.Text = null;
                comboBox3.Text = null;
            }

        }
    }
}
