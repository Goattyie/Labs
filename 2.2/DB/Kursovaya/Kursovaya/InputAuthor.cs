using Npgsql;
using System;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class InputAuthor : Form
    {
        public InputAuthor()
        {
            InitializeComponent();
        }
        string Author;
        private void button1_Click(object sender, EventArgs e)
        {
            Author = comboBox1.Text;
            Hide();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT second_name, name, last_name FROM author;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                }
                connect.Close();
            }
        }
        public string GetResult() { return Author; }
    }
}
