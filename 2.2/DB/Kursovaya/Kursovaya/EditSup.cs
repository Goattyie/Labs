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
            if (textBox1.Text != null && textBox1.Text != "")
            {
                if (edit == "Создание")
                    AddNode();
                
            }
            else MessageBox.Show("Поле не должно быть пустым.", "Error 007", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        string table;
        private void AddNode()
        {
            if (type == "Район")
                table = "area";
            else if (type == "Тип собственности")
                table = "own";
            else if (type == "Язык")
                table = "lang";
            else if (type == "Город")
                table = "city";
            else if (type == "Жанр")
                table = "style";
            else if (type == "Тип переплета")
                table = "binding";

            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                try
                {
                    connect.Open();
                    NpgsqlCommand command = new NpgsqlCommand("INSERT INTO " + table + " (name_" + table + ")  VALUES ('" + textBox1.Text + "');", connect);
                    command.ExecuteNonQuery();
                    connect.Close();
                    SQL.Success();
                    
                }
                catch(Npgsql.PostgresException ex){    SQL.SQLErrors(ex.ConstraintName);   }
                textBox1.Clear();
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
