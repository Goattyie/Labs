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
        public EditShop(int id, string name, int date, string area, string address, string own)
        {
            InitializeComponent();
            Id = id;
            Title = name;
            Date = date;
            Area = area;
            Address = address;
            Own = own;
            State = false;
            textBox1.Text = Title;
            textBox2.Text = Address;
            textBox3.Text = Date.ToString();
            comboBox1.Text = Area;
            comboBox2.Text = Own;
        }
        string Area, Own, Title, Address;
        int Id, Date;
        bool State = true;

        private void Clear()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.comboBox2.Items.Clear();
            this.comboBox1.Items.Clear();
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!InputData.CheckInt(textBox3.Text, "\"Дата открытия\""))
                return;

            Area = InputData.CheckString(comboBox1.Text);
            Own = InputData.CheckString(comboBox2.Text);

            Date = Convert.ToInt32(textBox3.Text);
            Title = InputData.CheckString(textBox1.Text);
            Address = InputData.CheckString(textBox2.Text);

            bool success = false;
            if (State)
            {
                success = new Shop(Title, Date, Area, Address, Own).Insert();
                if (success) this.Clear();
            }
            else
                new Shop(Id, Title, Date, Area, Address, Own).Update();
        }
        

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox2.Items.Clear();
                //comboBox2.Items.Add("Добавить");
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
                //comboBox1.Items.Add("Добавить");
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
