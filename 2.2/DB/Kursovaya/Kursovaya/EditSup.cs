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
    public partial class EditSup : Form
    {
        public EditSup(string type)
        {
            InitializeComponent();
            this.type = type;
        }
        string type, edit = "Создание";
        private void EditSup_Load(object sender, EventArgs e)
        {
            label1.Text = edit + " записи в таблице " + type;
            label2.Text = type + ':';
            if (edit == "Создание")
            button1.Text = "Создать";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {
                if(edit == "Создание")
                {
                    AddNode();
                }
                this.Hide();
            }
        }
        string table;
        private void AddNode()
        {
            if (type == "Район")
                table = "area";
            else if (type == "Тип собственности")
                table = "own";

            using (NpgsqlConnection connect = new SQL().GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("INSERT INTO " + table + " (name_" + table + ")  VALUES ('"+textBox1.Text+"');", connect);
                command.ExecuteNonQuery();
                connect.Close();
            }
        }
        private void EditSup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason != new CloseReason())
            {
                textBox1.Text = null;
            }
        }

        public string GetResult() { return textBox1.Text; }
    }
}
