using System;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    public partial class EditPublisher : Form
    {
        int State;
        public EditPublisher()
        {
            InitializeComponent();
            State = 1;
        }
        public EditPublisher(int id, string name, string city, int date, string phone)
        {
            InitializeComponent();
            State = 2;
            textBox1.Text = name;
            textBox2.Text = date.ToString();
            textBox3.Text = phone;
            this.city = city;
            this.id = id;
        }
        private int id;
        private string name;
        private string telephone;
        private string city = "NULL";
        private int date;
        private void button2_Click(object sender, EventArgs e)
        {
            name = InputData.CheckString(textBox1.Text);
            telephone = InputData.CheckString(textBox3.Text);
            city = InputData.CheckString(comboBox1.Text);

            if (!InputData.CheckInt(textBox2.Text, "\"Дата создания\""))
                return;
            date = Convert.ToInt32(textBox2.Text);
            if (this.State == 1)
            {
                bool success = new Publisher(name, city, telephone, date).Insert();
                if (success)
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
            }
            else
                new Publisher(id, name, city, telephone, date).Update();
        }
        private void Clear()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }
        public string GetResult() { return this.name; }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_city FROM city;", connect);
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
