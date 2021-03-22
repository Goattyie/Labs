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

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Clear();
            contextMenuStrip1.Items.Add("Добавить");
            contextMenuStrip1.Show(this, new Point(button1.Location.X + button1.Size.Width / 2, button1.Location.Y));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Items.Clear();
            contextMenuStrip2.Items.Add("Добавить");
            contextMenuStrip2.Show(this, new Point(button2.Location.X + button2.Size.Width / 2, button2.Location.Y));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (address == null || own == null) //Не заполнены поля
                //return;

            SQL sql = new SQL();
            using (NpgsqlConnection connect = sql.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM shop WHERE shop_name = '" + textBox1.Text + "';", connect);
            }
        }

        string address, own;

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            own = e.ClickedItem.Text;
            if(own == "Добавить")
                return;
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            address = e.ClickedItem.Text;
            if (address == "Добавить")
                return;
        }
    }
}
