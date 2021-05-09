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
        private void button1_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip1.Items.Clear();
                contextMenuStrip1.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_area FROM area;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip1.Items.Add(reader.GetString(0));
                }
                contextMenuStrip1.Show(this, new Point(button1.Location.X + button1.Size.Width / 2, button1.Location.Y));

                connect.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip2.Items.Clear();
                contextMenuStrip2.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_own FROM own;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip2.Items.Add(reader.GetString(0));
                }
                contextMenuStrip2.Show(this, new Point(button2.Location.X + button2.Size.Width / 2, button2.Location.Y));

                connect.Close();
            }
            
        }
        private void Clear()
        {
            this.textBox1.Clear();
            this.textBox2.Clear();
            this.textBox3.Clear();
            this.contextMenuStrip1.Items.Clear();
            this.contextMenuStrip2.Items.Clear();
            own = null;
            address = null;
            name = null;
            area = null;
            button1.Text = "Выбрать";
            button2.Text = "Выбрать";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            bool success;
            if (!InputData.CheckInt(textBox3.Text, "\"Дата открытия\""))
                return;

            name = InputData.CheckString(textBox1.Text);
            address = InputData.CheckString(textBox2.Text);

            success = new Shop(name, Convert.ToInt32(textBox3.Text), area, address, own).Insert();
            if(success)this.Clear();
        }
        string area = "NULL", own = "NULL", name, address;
        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditSup editsup = new EditSup("Тип собственности");
                editsup.ShowDialog(this);
                own = editsup.GetResult();
                if (own == "")
                    own = null;
            }
            else
            {
                button2.Text = e.ClickedItem.Text;
                own = InputData.CheckString(button2.Text);
            }
        }
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditSup editsup = new EditSup("Район");
                editsup.ShowDialog(this);
                area = editsup.GetResult();
                if (area == "")
                    area = null;
            }
            else
            {
                button1.Text = e.ClickedItem.Text;
                area = InputData.CheckString(button1.Text);
            }
        }
    }
}
