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
        private string name;
        private byte photo;
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
                photo = 0;
                button1.Text = ofd.FileName;
            }
        }

        //Издательство
        private void button4_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip4.Items.Clear();
                contextMenuStrip4.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT publisher_name FROM publisher;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip4.Items.Add(reader.GetString(0));
                }
                contextMenuStrip4.Show(this, new Point(button4.Location.X + button4.Size.Width / 1, button4.Location.Y));

                connect.Close();
            }
        }

        //Тип переплета
        private void button6_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip6.Items.Clear();
                contextMenuStrip6.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_binding FROM binding;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip6.Items.Add(reader.GetString(0));
                }
                contextMenuStrip6.Show(this, new Point(button6.Location.X + button6.Size.Width / 1, button6.Location.Y));

                connect.Close();
            }
        }

        //Авторы
        private void button2_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip2.Items.Clear();
                contextMenuStrip2.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT second_name_author, name_author, last_name_author FROM author;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip2.Items.Add(reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                }
                contextMenuStrip2.Show(this, new Point(button2.Location.X + button2.Size.Width / 1, button2.Location.Y));

                connect.Close();
            }
        }

        //Язык
        private void button3_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip3.Items.Clear();
                contextMenuStrip3.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_lang FROM lang;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip3.Items.Add(reader.GetString(0));
                }
                contextMenuStrip3.Show(this, new Point(button3.Location.X + button3.Size.Width / 1, button3.Location.Y));

                connect.Close();
            }
        }

        //Жанр
        private void button5_Click(object sender, EventArgs e)
        {
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                contextMenuStrip5.Items.Clear();
                contextMenuStrip5.Items.Add("Добавить");
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT name_style FROM style;", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contextMenuStrip5.Items.Add(reader.GetString(0));
                }
                contextMenuStrip5.Show(this, new Point(button5.Location.X + button5.Size.Width / 1, button5.Location.Y));

                connect.Close();
            }
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditAuthor editauthor = new EditAuthor();
                editauthor.ShowDialog(this);
                /*area = editauthor.GetResult();
                if (area == "")
                    area = null;
                */
            }
            else
            {
                foreach(string item in listBox1.Items)
                {
                    if (item == e.ClickedItem.Text)
                    {
                        MessageBox.Show("Данный автор уже добавлен в список!", "Ошибка013", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                listBox1.Items.Add(e.ClickedItem.Text);
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
            name = InputData.CheckString(textBox1.Text);
            photo = 0;
            description = InputData.CheckString(textBox2.Text);
            if (!InputData.CheckInt(textBox4.Text, "Дата создания"))
                return;
            if (!InputData.CheckInt(textBox3.Text, "Дата публикации"))
                return;
            else if (listBox1.Items.Count == 0)
                SQL.ErrorShow("У книги должны быть авторы");

            date = Convert.ToInt32(textBox4.Text);
            publish_date = Convert.ToInt32(textBox3.Text);

            //Добавление в таблицу книги-авторы
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                try
                {
                    //Добавление записи в книгу
                    NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO book (book_name, book_photo, book_description, book_lang_id, book_date, book_publisher_id, book_style_id, book_binding_id, book_date_public) VALUES " +
                        $"('{name}', '{photo}', '{description}', " +
                        $"(SELECT id_lang FROM lang WHERE name_lang = '{lang}'), {date}, " +
                        $"(SELECT publisher_id FROM publisher WHERE publisher_name = '{publisher}')," +
                        $"(SELECT id_style FROM style WHERE name_style = '{style}')," +
                        $"(SELECT id_binding FROM binding WHERE name_binding = '{binding}'), {publish_date})", connect);
                    command.ExecuteNonQuery();
                    //Добавление авторов в книгу
                    foreach(string author in listBox1.Items)
                    {
                        string[] FIO = author.Split(" ");
                        command = new NpgsqlCommand($"INSERT INTO book_author (id_book, id_author) VALUES " +
                            $"( (SELECT book_id FROM book WHERE book_name = '{name}')," +
                            $" ( SELECT id_author FROM author WHERE name_author = '{FIO[1]}' AND second_name_author = '{FIO[0]}' AND last_name_author = '{FIO[2]}'))", connect);
                        command.ExecuteNonQuery();
                    }
                    SQL.Success();
                }catch (Npgsql.PostgresException ex) {  SQL.SQLErrors(ex);   }
                connect.Close();
            }
        }


        private void contextMenuStrip5_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditSup editsup = new EditSup("Жанр");
                editsup.ShowDialog(this);
                style = editsup.GetResult();
                if (style == "")
                    style = null;
            }
            else
            {
                style = e.ClickedItem.Text;
                button5.Text = style;
            }
        }

        private void contextMenuStrip3_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditSup editsup = new EditSup("Язык");
                editsup.ShowDialog(this);
                lang = editsup.GetResult();
                if (lang == "")
                    lang = null;
            }
            else
            {
                lang = e.ClickedItem.Text;
                button3.Text = lang;
            }
        }

        private void contextMenuStrip4_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditPublisher editPublisher = new EditPublisher();
                editPublisher.ShowDialog(this);
                publisher = editPublisher.GetResult();
                if (publisher == "")
                    publisher = null;
            }
            else
            {
                publisher = e.ClickedItem.Text;
                button4.Text = publisher;
            }
        }

        private void contextMenuStrip6_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Добавить")
            {
                EditSup editsup = new EditSup("Тип переплета");
                editsup.ShowDialog(this);
                binding = editsup.GetResult();
                if (binding == "")
                    binding = null;
            }
            else
            {
                binding = e.ClickedItem.Text;
                button6.Text = binding;
            }
        }
    }
}
