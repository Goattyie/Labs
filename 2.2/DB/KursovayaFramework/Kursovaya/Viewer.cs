using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class Viewer : Form
    {
        public Viewer(string columnName, string table)
        {
            InitializeComponent();
            Column = columnName;
            Table = table;
        }
        string Column;
        string Table;
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == null)
                return;
            string Query = null;
            if (Column == "area")
                Query = $"SELECT s.id ID, s.name Название, s.date_open \"Дата открытия\", s.address Адресс, o.name Собственность FROM shop s JOIN own o ON s.id_own = o.id WHERE s.id_area = (SELECT id FROM area WHERE name = \'{comboBox1.Text}\') ";
            else if (Column == "city")
                Query = $"SELECT p.id ID, p.name Название, p.date_create \"Дата создания\", p.phone Телефон FROM publisher p WHERE p.id_city = (SELECT id FROM city WHERE name = \'{comboBox1.Text}\')";
            if (Query == null)
                return;
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlDataAdapter nda = new NpgsqlDataAdapter(Query, SQL.GetConnection());
                DataTable dt = new DataTable();
                nda.Fill(dt);
                dataGridView1.DataSource = dt;
                connect.Close();
            }
            label1.Text = "Количество записей: " + dataGridView1.Rows.Count.ToString();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT name FROM {Column};", connect);
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
