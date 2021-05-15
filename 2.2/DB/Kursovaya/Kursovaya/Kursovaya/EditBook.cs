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
    public partial class EditBook : Form
    {
        public EditBook()
        {
            InitializeComponent();
        }
        public EditBook(string id, string name, string photo, string description, string lang, string publisher, string style, string binding, string date, string publishDate)
        {
            InitializeComponent();
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
            label12.Text = "D:\\Labs\\2.2\\DB\\Kursovaya\\Kursovaya\\Kursovaya\bin\\Debug\\net5.0-windows\\images\\" + photo;
            this.photo = button1.Text;
            State = false;
        }
        private bool State = true;
        private int id;
        private string name;
        private string photo;
        private string description;
        private string lang;
        private int date;
        private string style;
        private string publisher;
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
<<<<<<< HEAD:2.2/DB/Kursovaya/Kursovaya/EditBook.cs
                button1.Text = ofd.FileName;
=======
                label12.Text = ofd.FileName;
>>>>>>> cd12392bba822688b80fb07e2117a381aa004caa:2.2/DB/Kursovaya/Kursovaya/Kursovaya/EditBook.cs
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
<<<<<<< HEAD:2.2/DB/Kursovaya/Kursovaya/EditBook.cs
            photo = InputData.CheckString(button1.Text);
=======
            photo = label12.Text;
>>>>>>> cd12392bba822688b80fb07e2117a381aa004caa:2.2/DB/Kursovaya/Kursovaya/Kursovaya/EditBook.cs
            name = InputData.CheckString(textBox1.Text);
            publisher = InputData.CheckString(comboBox1.Text);
            binding = InputData.CheckString(comboBox2.Text);
            lang = InputData.CheckString(comboBox4.Text);
            style = InputData.CheckString(comboBox5.Text);
            InputData.CheckString(photo);
            description = InputData.CheckString(textBox2.Text);
            if (!InputData.CheckInt(textBox3.Text, "\"Год создания(автором)\""))
                return;
            if (!InputData.CheckInt(textBox4.Text, "\"Дата публикации(издательством)\""))
                return;
            else if (listBox1.Items.Count == 0)
            {
                Message.ErrorShow("У книги должны быть авторы");
                return;
            }
            

            date = Convert.ToInt32(textBox4.Text);
            publish_date = Convert.ToInt32(textBox3.Text);
            if (date > publish_date)
            {
                Message.ErrorShow("Дата публикации не может быть больше даты созданияю.");
                return;
            }

            if (State)
            {
                bool success_authors = false;
                bool success_book = new Book(name, photo, description, lang, date, publisher, style, binding, publish_date).Insert();
                if (!success_book)
                    return;

                success_authors = new BookAuthor(name, listBox1.Items.Cast<String>().ToArray()).Insert();

                if (!success_authors) Message.ErrorShow("Не все авторы были добавлены в базу данных.");

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
                publisher = null;
            }
            else
            {
                new Book(id, name, photo, description, lang, date, publisher, style, binding, publish_date).Update();
                new BookAuthor(id, listBox1.Items.Cast<String>().ToArray()).Update();
            }
        }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT publisher_name FROM publisher;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString(0));
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
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_binding FROM binding;", connect);
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
                comboBox3.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT second_name_author, name_author, last_name_author FROM author;", connect);
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
                comboBox4.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_lang FROM lang;", connect);
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
                comboBox5.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_style FROM style;", connect);
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
            listBox1.Items.Add(comboBox3.Text);
        }
    }
}
