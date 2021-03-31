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
    public partial class EditShop : Form
    {
        public EditShop()
        {
            InitializeComponent();
        }

        //NpgsqlConnection connect = sql.GetConnection();
        static SQL sql = new SQL();
        private void button1_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = sql.GetConnection())
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
            using (NpgsqlConnection connect = sql.GetConnection())
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
            this.contextMenuStrip1.Items.Clear();
            this.contextMenuStrip2.Items.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (area == null) //Не заполнены поля
            {
                MessageBox.Show("Район не был указан", "Ошибка 002", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Неверная дата", "Ошибка 006", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (own == null)
            {
                MessageBox.Show("Тип собственности не был указан", "Ошибка 003", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox1.Text == null)
            {
                MessageBox.Show("Название магазина не было указано", "Ошибка 004", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (textBox2.Text == null)
            {
                MessageBox.Show("Адресс не был указан", "Ошибка 005", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            using (NpgsqlConnection connect = new SQL().GetConnection())
            {
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand("INSERT INTO shop (shop_name, date_open, id_area, address, id_own) VALUES " +
                        "('" + textBox1.Text + "', '" + dateTimePicker1.Value.Date.ToShortDateString() + "', " +
                        "(SELECT area.id_area FROM area WHERE area.name_area = '" + area + "'), " +
                        "'" + textBox2.Text + "', (SELECT own.id_own FROM own WHERE own.name_own = '" + own + "'))", connect);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запись добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Clear();
                }
                catch(Npgsql.PostgresException ex)
                {
                    SQL.SQLErrors(ex.ConstraintName);
                }
                connect.Close();
            }
        }

        string area, own;

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
                own = e.ClickedItem.Text;
                button2.Text = own;
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
                area = e.ClickedItem.Text;
                button1.Text = area;
            }
        }
    }
}
