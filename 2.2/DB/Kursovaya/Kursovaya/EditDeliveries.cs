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
        string Shop = "NULL", Book = "NULL", Lang = "NULL", Date = "NULL";
        int Count;
        double Cost, FirstCost, Volume;
        bool PreOder = false;

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
                new EditShop().ShowDialog(this);
            else
            {
                button1.Text = e.ClickedItem.Text;
                Shop = InputData.CheckString(button1.Text);
            }
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
                new EditBook().ShowDialog(this);
            else
            {
                button2.Text = e.ClickedItem.Text;
                Book = InputData.CheckString(button2.Text);
            }
        }

        private void contextMenuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
                new EditSup("Язык").ShowDialog(this);
            else
            {
                button3.Text = e.ClickedItem.Text;
                Lang = InputData.CheckString(button3.Text);
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
                button1.Text = "Выбрать";
                button2.Text = "Выбрать";
                button3.Text = "Выбрать";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.Add("Добавить");
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT shop_name FROM shop", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip1.Items.Add($"{reader.GetString(0)}");
                }
                contextMenuStrip1.Show(this, new Point(button1.Location.X + button1.Size.Width / 2, button1.Location.Y));

                connect.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Items.Clear();
            contextMenuStrip2.Items.Add("Добавить");
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT book_name FROM book", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip2.Items.Add($"{reader.GetString(0)}");
                }
                contextMenuStrip2.Show(this, new Point(button2.Location.X + button2.Size.Width / 2, button2.Location.Y));

                connect.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Items.Clear();
            contextMenuStrip3.Items.Add("Добавить");
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_lang FROM lang", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip3.Items.Add($"{reader.GetString(0)}");
                }
                contextMenuStrip3.Show(this, new Point(button3.Location.X + button3.Size.Width / 2, button3.Location.Y));

                connect.Close();
            }
        }
    }
}
