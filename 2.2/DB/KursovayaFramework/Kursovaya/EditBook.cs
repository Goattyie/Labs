using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    public partial class EditBook : Form
    {
        public EditBook()
        {
            InitializeComponent();
        }
        public EditBook(string id, string name, string photo, string description, string lang, string publisher, string style, string binding, string date, string publishDate)
        {
            InitializeComponent();
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand($"SELECT p.id FROM book b JOIN publisher p ON p.id = b.id_publisher WHERE b.id = {id}", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    publisher = reader.GetValue(0) + " " + publisher;
                }
                connect.Close();
            }
            this.id = Convert.ToInt32(id);
            textBox1.Text = name;
            textBox2.Text = description;
            textBox3.Text = publishDate.ToString();
            textBox4.Text = date.ToString();
            comboBox1.Text = publisher;
            comboBox2.Text = binding;
            comboBox4.Text = lang;
            comboBox5.Text = style;
            button1.Text = photo;
            this.photo = button1.Text;
            State = false;
            comboBox3.Visible = false;
            label4.Visible = false;
            button8.Visible = false;
            listBox1.Visible = false;
        }
        private bool State = true;
        private int id;
        private string name;
        private string photo;
        private string description;
        private string lang;
        private int date;
        private string style;
        private int publisher;
        private string binding;
        private int publish_date;

        //Обложка
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            var state = ofd.ShowDialog();

            if (state == DialogResult.Cancel)
                return;

            if (state == DialogResult.OK)
            {
                button1.Text = ofd.FileName;
            }
        }
        //Удалить автора из списка
        private void button8_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[i]);
            }
        }
        //Добавление записи
        private void button7_Click(object sender, EventArgs e)
        {
            photo = button1.Text;
            name = InputData.CheckString(textBox1.Text);
            if (InputData.CheckInt(comboBox1.Text.Split(' ')[0], "Издательство"))
                publisher = Convert.ToInt32(comboBox1.Text.Split(' ')[0]);
            binding = InputData.CheckString(comboBox2.Text);
            lang = InputData.CheckString(comboBox4.Text);
            style = InputData.CheckString(comboBox5.Text);
            InputData.CheckString(photo);
            description = InputData.CheckString(textBox2.Text);
            if (!InputData.CheckInt(textBox3.Text, "\"Год создания(автором)\""))
                return;
            if (!InputData.CheckInt(textBox4.Text, "\"Дата публикации(издательством)\""))
                return;
           
            date = Convert.ToInt32(textBox4.Text);
            publish_date = Convert.ToInt32(textBox3.Text);
            if (date > publish_date)
            {
                Message.ErrorShow("Дата публикации не может быть больше даты созданияю.");
                return;
            }

            if (State)
            {
                if (listBox1.Items.Count == 0)
                {
                    Message.ErrorShow("У книги должны быть авторы");
                    return;
                }
                bool success_authors = false;
                bool success_book = new Book(name, photo, description, lang, date, publisher, style, binding, publish_date).Insert();
                if (!success_book)
                    return;

                new BookAuthor(name, listBox1.Items.Cast<String>().ToArray()).InsertAllAuthors();

                Message.Success();
                listBox1.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";//14635
                comboBox1.Text = null;
                comboBox2.Text = null;
                comboBox3.Text = null;
                comboBox4.Text = null;
                comboBox5.Text = null;
                button1.Text = "Выбрать";
                photo = "0";
                lang = null;
                style = null;
                binding = null;
            }
            else
            {
                new Book(id, name, photo, description, lang, date, publisher, style, binding, publish_date).Update();
                //new BookAuthor(id, listBox1.Items.Cast<String>().ToArray()).Update();
            }
        }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, name FROM publisher;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add($"{reader.GetValue(0)} {reader.GetValue(1)}");
                }
                connect.Close();
            }
        }
        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name FROM binding;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                }
                connect.Close();
            }
        }
        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox3.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT second_name, name, last_name FROM author;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                }
                connect.Close();
            }
        }
        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox4.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name FROM lang;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox4.Items.Add(reader.GetString(0));
                }
                connect.Close();
            }
        }
        private void comboBox5_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox5.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name FROM style;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox5.Items.Add(reader.GetString(0));
                }
                connect.Close();
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (string item in listBox1.Items)
            {
                if (item == comboBox3.Text)
                {
                    MessageBox.Show("Данный автор уже добавлен в список!", "Ошибка013", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if(comboBox3.Text != null || comboBox3.Text != "")
                listBox1.Items.Add(comboBox3.Text);
        }
    }
}
