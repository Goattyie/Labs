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
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT s.id FROM deliveries d JOIN shop s ON s.id = d.id_shop WHERE d.id = {id}", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    shop = reader.GetValue(0) + " " + shop;
                }
                connect.Close();
            }
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT b.id FROM deliveries d JOIN book b ON b.id = d.id_book WHERE d.id = {id}", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    book = reader.GetValue(0) + " " + book;
                }
                connect.Close();
            }
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
        string Lang, Date;
        int Count, Id, Shop, Book;
        double Cost, FirstCost, Volume;
        bool State = true;

        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox3.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name FROM lang", connect);
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, name FROM book", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add($"{reader.GetValue(0)} {reader.GetValue(1)}");
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, name FROM shop", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add($"{reader.GetValue(0)} {reader.GetValue(1)}");
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

            Shop = Convert.ToInt32(comboBox1.Text.Split(' ')[0]);
            Book = Convert.ToInt32(comboBox2.Text.Split(' ')[0]);
            Lang = InputData.CheckString(comboBox3.Text);

            Cost = Convert.ToDouble(textBox1.Text);
            FirstCost = Convert.ToDouble(textBox2.Text);
            Date = InputData.CheckString(dateTimePicker1.Value.ToShortDateString());
            Count = Convert.ToInt32(numericUpDown1.Value);
            Volume = Convert.ToDouble(textBox3.Text);

            if(Cost < FirstCost)
            {
                Message.ErrorShow("Цена для магазина не может быть меньше цены для поставщика.");
                return;
            }
            if (State)
            {
                bool success = new Deliveries(Shop, Book, Lang, Count, Date, Cost, Volume, FirstCost, PreOder).Insert();
                if (success)
                {
                    Message.Success();
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
