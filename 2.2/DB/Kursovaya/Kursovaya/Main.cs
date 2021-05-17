using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            tabPage1.Text = "Основные таблицы";
            tabPage2.Text = "Справочники";
            tabPage3.Text = "Запросы";

            SetTagPage2();
            SetTagPage3();

            AddTables();

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            button8.Enabled = false;
            button12.Enabled = false;
        }
        private void SetTagPage2()
        {
            listBox2.Size = listBox1.Size;
            listBox2.Location = listBox1.Location;

            dataGridView2.Size = dataGridView1.Size;
            dataGridView2.Location = dataGridView1.Location;

            button3.Text = button1.Text;
            button3.Size = button1.Size;
            button3.Location = button1.Location;
            button3.Font = button1.Font;

            button4.Text = button2.Text;
            button4.Size = button2.Size;
            button4.Location = button2.Location;
            button4.Font = button2.Font;

            label3.Text = label2.Text;
            label3.Location = label2.Location;
            label3.Size = label2.Size;
            label3.Font = label2.Font;

            label7.Text = label6.Text;
            label7.Location = label6.Location;
            label7.Size = label6.Size;
            label7.Font = label6.Font;

            label9.Location = label8.Location;
            label9.Text = " ";
        }
        private void SetTagPage3()
        {

            label5.Size = label2.Size;
            label5.Font = label2.Font;
            button5.Font = button1.Font;

        }
        private void AddTables() 
        {
            listBox1.Items.Add("Магазины");
            listBox1.Items.Add("Поставки");
            listBox1.Items.Add("Книги");
            listBox1.Items.Add("Издательства");
            listBox1.Items.Add("Книги-авторы");

            listBox2.Items.Add("Район");
            listBox2.Items.Add("Город");
            listBox2.Items.Add("Язык");
            listBox2.Items.Add("Тип собственности");
            listBox2.Items.Add("Жанр");
            listBox2.Items.Add("Тип переплета");
            listBox2.Items.Add("Автор");

            listBox3.Items.Add("Вывести все магазины с заданным типом собственности.");
            listBox3.Items.Add("Вывести все района по вводимому названию маназина.");
            listBox3.Items.Add("Вывести информацию о магазинах, поставках о них по вводимому году открытия магазина.");
            listBox3.Items.Add("Вывести все книги с датой публикации издательством в период с - по");
            listBox3.Items.Add("Вывести информацию о всех магазинах и поставках в них.");
            listBox3.Items.Add("Вывести все книги и их язык оригинала.");
            listBox3.Items.Add("Вывести города и издательства в них.");
            listBox3.Items.Add("Вывести информацию о городах, в которых нет издательств.");
            listBox3.Items.Add("Выбрать все книги, но показать только книги данног автора.");
            listBox3.Items.Add("Вывести информацию о городах, в которых нет издательств за указанный год создания");

            dataGridView2.ColumnHeadersHeight = 30;
            label1.Location = label9.Location;
            label1.Font = label9.Font;
            label9.Size = label9.Size;
        }
        private void tagPage1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text == "Основные таблицы")
            {
                UpdateDatagrid(dataGridView1, listBox1, label1);
            }
            else if (e.TabPage.Text == "Справочники")
            {
                UpdateDatagrid(dataGridView2, listBox2, label9);
            }
            else if (e.TabPage.Text == "Запросы")
            {
                //Открыть форму Request
            }
        }
        private void tagPage1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            if (listBox1.SelectedItem.ToString() == "Магазины")
                new EditShop().ShowDialog(this);
            else if (listBox1.SelectedItem.ToString() == "Поставки")
                new EditDeliveries().ShowDialog(this);
            else if (listBox1.SelectedItem.ToString() == "Книги")
                new EditBook().ShowDialog(this);
            else if (listBox1.SelectedItem.ToString() == "Издательства")
                new EditPublisher().ShowDialog(this);

            UpdateDatagrid(dataGridView1, listBox1, label1);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                if (listBox2.SelectedItem.ToString() == "Автор") 
                {
                    EditAuthor AddAuthor = new EditAuthor();
                    AddAuthor.ShowDialog(this);
                }
                else
                {
                    EditSup AddSup = new EditSup(listBox2.SelectedItem.ToString());
                    AddSup.ShowDialog(this);
                }
                UpdateDatagrid(dataGridView2, listBox2, label9);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem == null)
                return;
            string Query = null;
            InputText it = new InputText();
            if (listBox3.SelectedIndex == 0)
            {
                it.ShowDialog(this);
                Query = $"SELECT s.name Магазин, a.name Район, s.address Адрес, s.date_open \"Дата открытия\" FROM shop s " +
                    $"JOIN own o ON s.id_own = o.id " +
                    $"JOIN area a ON s.id_area = a.id " +
                    $"WHERE o.name = '{it.GetResult()}' " +
                    $"ORDER BY s.name, a.name";
            }
            else if (listBox3.SelectedIndex == 1)
            {
                it.ShowDialog(this);
                Query = $"SELECT a.id id, a.name Район FROM area a " +
                    $"JOIN shop s ON a.id = s.id_area WHERE s.name = '{it.GetResult()}' " +
                    $"ORDER BY a.name";
            }
            else if (listBox3.SelectedIndex == 2)
            {
                it.ShowDialog(this);
                int Year;
                try
                {
                    Year = Convert.ToInt32(it.GetResult());
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = "SELECT d.id \"id Поставки\", s.name Магазин, a.name Район, s.address Адрес, o.name Собственность, b.name Книга, d.count_book Количество, d.date_come \"Дата поступления\"," +
                    " d.cost \"Цена для магазина\", d.def_cost \"Цена для поставщика\", l.name Язык, d.size Объем, d.pre_order Предзаказ " +
                    "FROM deliveries d " +
                    "JOIN book b ON d.id_book = b.id " +
                    "JOIN shop s ON d.id_shop = s.id " +
                    "JOIN area a ON s.id_area = a.id " +
                    "JOIN lang l ON d.id_lang = l.id " +
                    "JOIN own o ON s.id_own = o.id " +
                    $"WHERE s.date_open = {Year} " +
                    "ORDER BY s.name, a.name, d.date_come";
            }
            else if (listBox3.SelectedIndex == 3)
            {
                it.ShowDialog(this);
                int FirstYear, SecondYear;
                try
                {
                    FirstYear = Convert.ToInt32(it.GetResult());
                    it.ShowDialog(this);
                    SecondYear = Convert.ToInt32(it.GetResult());
                    if (FirstYear > SecondYear)
                        throw new Exception();
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = "SELECT b.name Книга, b.description Описание, l.name Язык, p.name Издательство, bind.name Переплет, b.date_public \"Дата публикации\", b.date_create \"Дата создания\"," +
                    " b.photo Фото FROM book b " +
                    "JOIN lang l ON b.id_lang = l.id " +
                    "JOIN publisher p ON p.id = b.id_publisher " +
                    "JOIN binding bind ON bind.id = b.id_binding " +
                    $"WHERE b.date_public >= {FirstYear} AND b.date_public <= {SecondYear} " +
                    $"ORDER BY b.name, b.date_public";
            }
            else if (listBox3.SelectedIndex == 4)
            {
                Query = "SELECT d.id \"id Поставки\", s.name Магазин, a.name Район, s.address Адрес, o.name Собственность, b.name Книга, d.count_book Количество, d.date_come \"Дата поступления\"," +
                    " d.cost \"Цена для магазина\", d.def_cost \"Цена для поставщика\", l.name Язык, d.size Объем, d.pre_order Предзаказ " +
                    "FROM deliveries d " +
                    "JOIN book b ON d.id_book = b.id " +
                    "JOIN shop s ON d.id_shop = s.id " +
                    "JOIN area a ON s.id_area = a.id " +
                    "JOIN lang l ON d.id_lang = l.id " +
                    "JOIN own o ON s.id_own = o.id " +
                    "ORDER BY s.name, a.name, d.date_come";
            }
            else if (listBox3.SelectedIndex == 5)
                Query = "SELECT b.id id, b.name Название, b.photo Фото, p.name Издательство, l.name Язык FROM book b JOIN publisher p ON b.id_publisher = p.id JOIN lang l ON b.id_lang = l.id ORDER BY b.name, l.name";
            else if (listBox3.SelectedIndex == 6)
                Query = "SELECT c.name Город, p.name Издательство FROM publisher p JOIN city c ON c.id = p.id_city ORDER BY c.name, p.name";
            else if (listBox3.SelectedIndex == 7)
                Query = "SELECT c.id id, c.name Город FROM city c LEFT JOIN publisher p ON p.id_city = c.id WHERE p.name IS NULL ORDER BY c.name";
            else if (listBox3.SelectedIndex == 8)
            {
                InputAuthor ia = new InputAuthor();
                ia.ShowDialog(this);
                if (ia.GetResult() == null)
                    return;
                string[] Author = ia.GetResult().Split(' ');
                Query = "SELECT b.name Книга, b.photo Фото " +
                    "FROM book_author ba " +
                    "RIGHT JOIN book b ON b.id = ba.id_book WHERE ba.id_author = " +
                    $"(SELECT id FROM author WHERE second_name = '{Author[0]}' AND name = '{Author[1]}' AND last_name = '{Author[2]}') " +
                    "ORDER BY b.name";
            }
            else if (listBox3.SelectedIndex == 9) 
            {
                it.ShowDialog(this);
                int Year;
                try
                {
                    Year = Convert.ToInt32(it.GetResult());
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = $"SELECT DISTINCT c.id id, c.name Город FROM city c LEFT JOIN publisher p ON p.id_city = c.id WHERE (SELECT Count(p.name) FROM publisher p WHERE p.date_create = {Year}) = 0 ORDER BY c.name";
            }
            if (Query == null)
                return;
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                connect.Open();
                NpgsqlDataAdapter nda = new NpgsqlDataAdapter(Query, SQL.GetConnection());
                DataTable dt = new DataTable();
                nda.Fill(dt);
                dataGridView3.DataSource = dt;
                connect.Close();
            }
            label4.Text = "Количество записей: " + dataGridView3.Rows.Count.ToString();

        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            else
            {
                List<int> Id = new List<int>();
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    Id.Add(Convert.ToInt32(dataGridView1[0, row.Index].Value.ToString()));
                }
                Table.ReturnTable(listBox1.SelectedItem.ToString()).Delete(Id.ToArray());
                UpdateDatagrid(dataGridView1, listBox1, label1);
            }
        }
        void UpdateDatagrid(DataGridView dgv, ListBox lb, Label count_nodes)
        {
            if (lb.SelectedItem == null) return;
            else
            {
                DataTable dt = new DataTable();
                Table.ReturnTable(lb.SelectedItem.ToString()).Select().Fill(dt);
                dgv.DataSource = dt;
                count_nodes.Text = "Количество записей: " + dgv.Rows.Count;
            }
        }
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem.ToString() == "Книги-авторы")
            {
                button2.Enabled = false;
                button9.Enabled = false;
            }
            else { button2.Enabled = true; button9.Enabled = true; }
                if (listBox1.SelectedItem.ToString() == "Книги")
                button8.Enabled = true;
            else button8.Enabled = false;

            if (listBox1.SelectedItem.ToString() == "Магазины" || listBox1.SelectedItem.ToString() == "Издательства")
                button12.Enabled = true;
            else button12.Enabled = false;
            UpdateDatagrid(dataGridView1, listBox1, label1);
        }
        private void AreaCreateColumns()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Адресс");
            dataGridView1.DataSource = dt;
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void listBox2_Click(object sender, EventArgs e)
        {
            UpdateDatagrid(dataGridView2, listBox2, label9);
            
        }
        private void tabPage2_MouseClick(object sender, MouseEventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            if (Message.DeleteWarning() == DialogResult.OK)
            {
                    List<int> Id = new List<int>();
                    foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                    {
                        Id.Add(Convert.ToInt32(dataGridView2[0, row.Index].Value.ToString()));
                    }
                    Table.ReturnTable(listBox2.SelectedItem.ToString()).Delete(Id.ToArray());
                    UpdateDatagrid(dataGridView2, listBox2, label9);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Table.Generate();
               
            }).Start();
            UpdateDatagrid(dataGridView2, listBox2, label9);
            UpdateDatagrid(dataGridView1, listBox1, label1);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Table.TruncateAll();
            UpdateDatagrid(dataGridView2, listBox2, label9);
            UpdateDatagrid(dataGridView1, listBox1, label1);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            string image = "images/" + dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString();
            if (!File.Exists(image))
            {
                Message.ErrorShow("Картинка была удалена из каталога");
                return;
            }
            new PictureViewer(image).Show();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || dataGridView1.SelectedRows.Count == 0)
                return;


            if (listBox1.SelectedItem.ToString() == "Магазины")
                new EditShop(Convert.ToInt32(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString()), dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value.ToString(), Convert.ToInt32(dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString()), dataGridView1[4, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[3, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[5, dataGridView1.SelectedRows[0].Index].Value.ToString()).ShowDialog(this);
            else if (listBox1.SelectedItem.ToString() == "Поставки")
                new EditDeliveries(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[3, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[4, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[5, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[7, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[8, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[9, dataGridView1.SelectedRows[0].Index].Value.ToString()).ShowDialog(this);
            else if (listBox1.SelectedItem.ToString() == "Книги")
                new EditBook(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[3, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[4, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[5, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[6, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[7, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[8, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[9, dataGridView1.SelectedRows[0].Index].Value.ToString()).ShowDialog(this);
            else if (listBox1.SelectedItem.ToString() == "Издательства")
                new EditPublisher(Convert.ToInt32(dataGridView1[0, dataGridView1.SelectedRows[0].Index].Value.ToString()), dataGridView1[1, dataGridView1.SelectedRows[0].Index].Value.ToString(), dataGridView1[2, dataGridView1.SelectedRows[0].Index].Value.ToString(), Convert.ToInt32(dataGridView1[4, dataGridView1.SelectedRows[0].Index].Value.ToString()), dataGridView1[3, dataGridView1.SelectedRows[0].Index].Value.ToString()).ShowDialog(this);

            UpdateDatagrid(dataGridView1, listBox1, label1);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            Searcher search = new Searcher(dataGridView1);
            search.ShowDialog(this);
            int[] indexes = search.GetIndexes();
            string text = search.GetText();
            
            if (indexes == null)
                return;

            UpdateDatagrid(dataGridView1, listBox1, label1);
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                dt.Columns.Add(column.HeaderText);
            foreach (DataGridViewRow node in dataGridView1.Rows)
            {
                bool find = true;
                foreach (int index in indexes)
                {
                    if (!node.Cells[index].Value.ToString().Contains(text))
                        find = false;
                }
                if (find)
                    dt.Rows.Add(CreateNode(node));
            }
            dataGridView1.DataSource = dt;
            label1.Text = "Количество записей: " + dataGridView1.Rows.Count.ToString();
            string[] CreateNode(DataGridViewRow node)
            {
                string[] n = new string[dataGridView1.ColumnCount];
                for (int i = 0; i < n.Length; i++)
                    n[i] = node.Cells[i].Value.ToString();
                return n;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            Deleter delete = new Deleter(dataGridView1);
            delete.ShowDialog(this);

            int index = delete.GetIndex();
            string text = delete.GetText();

            if(index != -1 && text != "")
                 MainTable.ReturnMainTable(listBox1.SelectedItem.ToString()).ColumnDelete(dataGridView1.Columns[index].HeaderText, text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string Column = null;
            foreach (DataGridViewColumn DataCol in dataGridView1.Columns)
            {
                Column = MainTable.ReturnMainTable(listBox1.SelectedItem.ToString()).ConvertColumnName(DataCol.HeaderText);
                if (Column != null)
                    break;
            }
            if (Column == null)
                return;
            new Viewer(Column, MainTable.ReturnMainTable(listBox1.SelectedItem.ToString()).ClassName).Show();
        }
    }
}
