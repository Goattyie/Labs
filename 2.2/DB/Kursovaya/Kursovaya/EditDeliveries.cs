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
        public EditDeliveries(string id, string shop, string book, string count, string date, string cost, string firstCost, string lang, string volume, string preOrder)
        {
            InitializeComponent();
            Id = Convert.ToInt32(id);
            textBox1.Text = cost.ToString();
            textBox2.Text = firstCost.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(date);
            numericUpDown1.Value = Convert.ToInt32(count);
            textBox3.Text = volume.ToString();
            comboBox1.Text = shop;
            comboBox2.Text = book;
            comboBox3.Text = lang;
            if (Convert.ToBoolean(preOrder))
                radioButton1.Checked = true;
            else radioButton2.Checked = true;
            State = false;
        }
        string Shop, Book, Lang, Date;
        int Count, Id;
        double Cost, FirstCost, Volume;
        bool State = true;

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

            if (State)
            {
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
            else
            {
                new Deliveries(Id,Shop, Book, Lang, Count, Date, Cost, Volume, FirstCost, PreOder).Update();
            }

        }
    }
}
