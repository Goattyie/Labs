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
    public partial class EditPublisher : Form
    {
        public EditPublisher()
        {
            InitializeComponent();
        }
        private string name;
        private string telephone;
        private string city;
        private int date;
        private void button2_Click(object sender, EventArgs e)
        {
            name = InputData.CheckString(textBox1.Text);
            city = InputData.CheckString(city);
            telephone = InputData.CheckString(textBox3.Text);


            if (!InputData.CheckInt(textBox2.Text, "\"Дата создания\""))
                return;
            date = Convert.ToInt32(textBox2.Text);

            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                try
                {
                    connect.Open();
                    string request = $"INSERT INTO publisher (publisher_name, city_id, phone, create_date) VALUES " +
                            $"({name}, (SELECT id_city FROM city WHERE name_city = {city}), {telephone} , {date})";

                    NpgsqlCommand command = new NpgsqlCommand(request, connect);
                    command.ExecuteNonQuery();
                    SQL.Success();
                    this.Clear();
                    connect.Close();
                }catch (Npgsql.PostgresException ex) { SQL.SQLErrors(ex); return; }
                SQL.Success();

            }
        }
        private void Clear()
        {
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            button1.Text = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip1.Items.Clear();
                contextMenuStrip1.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_city FROM city;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip1.Items.Add(reader.GetString(0));
                }
                contextMenuStrip1.Show(this, new Point(button1.Location.X + button1.Size.Width / 2, button1.Location.Y));

                connect.Close();
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditSup editsup = new EditSup("Город");
                editsup.ShowDialog(this);
                city = editsup.GetResult();
                if (city == "")
                    city = null;
            }
            else
            {
                city = e.ClickedItem.Text;
                button1.Text = city;
            }
        }
        public string GetResult() { return this.name; }
    }
    
}
