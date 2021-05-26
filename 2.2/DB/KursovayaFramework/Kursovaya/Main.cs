﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
            tabPage4.Text = "Диаграммы";

            SetTagPage2();
            SetTagPage3();

            AddTables();

            dataGridView1.ReadOnly = true;
            dataGridView2.ReadOnly = true;

            button8.Enabled = false;
            button12.Enabled = false;

            chart1.Series.Clear();
            chart1.Series.Add("Диаграммы");
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
            listBox3.Items.Add("Вывести все районы по вводимому названию магазина.");
            listBox3.Items.Add("Вывести информацию о магазинах по вводимому году открытия магазина.");
            listBox3.Items.Add("Вывести все книги с датой публикации издательством в период с - по");
            listBox3.Items.Add("Вывести информацию о всех магазинах и поставках в них.");
            listBox3.Items.Add("Вывести все книги и их язык оригинала.");
            listBox3.Items.Add("Вывести города и издательства в них.");
            listBox3.Items.Add("Вывести информацию о городах, в которых нет издательств.");
            listBox3.Items.Add("Выбрать все книги, но показать только книги данного автора.");
            listBox3.Items.Add("Вывести информацию о городах, в которых нет издательств за указанный год создания");

            listBox3.Items.Add("Выдать количество издательств в каждом городе");
            listBox3.Items.Add("Вывести название и тираж книг с датой публикации издательством в период с-по.");
            listBox3.Items.Add("Выбрать названия магазинов с общей стоимостью закупок, превышающей заданное значение и определить количество поставленных партий.");
            listBox3.Items.Add("Выбрать названия магазинов с общей стоимостью закупок, превышающей заданное значение и определить количество поставленных партий в период поставок с (дата) – по (дата)");
            listBox3.Items.Add("Определить количество магазинов каждой магазинной сети и количество поставок для каждой сети.");
            listBox3.Items.Add("Выдать информацию об издательствах у которых год создания больше среднего года создания всех издательств");

            listBox3.Items.Add("Определить процент изданий поступивших в магазины после 2019 года по каждому магазину района");
            listBox3.Items.Add("Определить процент изданий поступивших в магазины после 2019 года по каждому району в целом");
            listBox3.Items.Add("Определить среднее количество изданий каждого издательства");
            listBox3.Items.Add("Определить среднее количество изданий по всем издательствам в целом");
            listBox3.Items.Add("Определить стоимость и количество книг каждого магазина за указанный период (по месяцам)");

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
            dataGridView3.DataSource = new DataTable();
            if (listBox3.SelectedIndex == 0)
            {
                it.ShowDialog(this);
                if (it.GetResult() == null)
                    return;
                Query = $"SELECT a.name Район, s.name Магазин, s.address Адрес, s.date_open \"Год открытия\" FROM shop s " +
                    $"JOIN own o ON s.id_own = o.id " +
                    $"JOIN area a ON s.id_area = a.id " +
                    $"WHERE o.name = '{it.GetResult()}' " +
                    $"ORDER BY a.name, s.name";
                label10.Text = "Собственность: " + it.GetResult();
                new Thread(() =>
                {
                    new HtmlReport("1.html", new string[] { "Район", "Магазин", "Адрес", "Год открытия" }).Parse(Query);
                    new ExcelReport("1", new string[] { "Район", "Магазин", "Адрес", "Год открытия" }).Parse(Query);
                }).Start();
            }
            else if (listBox3.SelectedIndex == 1)
            {
                it.ShowDialog(this);
                if (it.GetResult() == null)
                    return;
                Query = $"SELECT a.id id, a.name Район FROM area a " +
                    $"JOIN shop s ON a.id = s.id_area WHERE s.name = '{it.GetResult()}' " +
                    $"ORDER BY a.name";
                label10.Text = "Магазин: " + it.GetResult();
                new Thread(() =>
                {
                    new HtmlReport("2.html", new string[] { "id", "Район" }).Parse(Query);
                    new ExcelReport("2", new string[] { "id", "Район" }).Parse(Query);
                }).Start();
            }
            else if (listBox3.SelectedIndex == 2)
            {
                it.ShowDialog(this);
                int Year;
                if (it.GetResult() == null)
                    return;
                try
                {
                    Year = Convert.ToInt32(it.GetResult());
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = "SELECT a.name Район, s.name Магазин, s.address Адрес, o.name Собственность " +
                    "FROM shop s " +
                    "JOIN area a ON s.id_area = a.id " +
                    "JOIN own o ON s.id_own = o.id " +
                    $"WHERE s.date_open = {Year} " +
                    "ORDER BY  a.name, s.name";
                label10.Text = "Год открытия: " + it.GetResult();
                new Thread(() =>
                {
                    new HtmlReport("3.html", new string[] { "Район", "Магазин", "Адрес", "Собственность" }).Parse(Query);
                    new ExcelReport("3", new string[] { "Район", "Магазин", "Адрес", "Собственность" }).Parse(Query);
                }).Start();
            }
            else if (listBox3.SelectedIndex == 3)
            {
                InputPeriod ip = new InputPeriod();
                ip.ShowDialog(this);
                if (ip.GetResult() == null)
                    return;
                int FirstYear, SecondYear;
                try
                {
                    FirstYear = Convert.ToInt32(ip.GetResult()[0]);
                    SecondYear = Convert.ToInt32(ip.GetResult()[1]);
                    if (FirstYear > SecondYear)
                        throw new Exception();
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = "SELECT b.date_public \"Дата публикации\" , p.name Издательство ,b.name Книга, b.description Описание, l.name Язык, bind.name Переплет, b.date_create \"Дата создания\"," +
                    " b.photo Фото FROM book b " +
                    "JOIN lang l ON b.id_lang = l.id " +
                    "JOIN publisher p ON p.id = b.id_publisher " +
                    "JOIN binding bind ON bind.id = b.id_binding " +
                    $"WHERE b.date_public >= {FirstYear} AND b.date_public <= {SecondYear} " +
                    $"ORDER BY  b.date_public, p.name, b.name";
                label10.Text = "Период: " + FirstYear.ToString() + "-" + SecondYear.ToString();
                //new HtmlReport("4.html", new string[] { "Дата публикации", "Издательство", "Книга", "Описание", "Язык", "Переплет", "Дата создания", "Фото" }).Parse(Query);
                //new ExcelReport("4", new string[] { "Дата публикации", "Издательство", "Книга", "Описание", "Язык", "Переплет", "Дата создания", "Фото" }).Parse(Query);
            }
            else if (listBox3.SelectedIndex == 4)
            {
                Query = "SELECT d.id \"id Поставки\", a.name Район, s.name Магазин, s.address Адрес, o.name Собственность, b.name Книга, d.count_book Количество, d.date_come \"Дата поступления\"," +
                    " d.cost \"Цена для магазина\", d.def_cost \"Цена для поставщика\", l.name Язык, d.size Объем, d.pre_order Предзаказ " +
                    "FROM deliveries d " +
                    "JOIN book b ON d.id_book = b.id " +
                    "JOIN shop s ON d.id_shop = s.id " +
                    "JOIN area a ON s.id_area = a.id " +
                    "JOIN lang l ON l.id = b.id_lang " +
                    "JOIN own o ON s.id_own = o.id " +
                    "ORDER BY a.name, s.name, d.date_come";
                label10.Text = null;
                //new HtmlReport("5.html", new string[] { "id", "Магазин", "Район", "Адрес", "Собственность", "Книга", "Тираж", "Дата поступления", "Цена для магазина", "Цена для поставщика", "Язык", "Объем", "Предзаказ" }).Parse(Query);
            }
            else if (listBox3.SelectedIndex == 5)
            {
                label10.Text = null;
                Query = "SELECT b.id id, p.name Издательство, b.name Название, b.photo Фото, l.name \"Язык оригинала\" FROM book b JOIN publisher p ON b.id_publisher = p.id JOIN lang l ON b.id_lang = l.id ORDER BY p.name, b.name, l.name";
                //new HtmlReport("6.html", new string[] { "id", "Издательство", "Название", "Фото", "Язык оригинала" }).Parse(Query);
            }
            else if (listBox3.SelectedIndex == 6)
            {
                label10.Text = null;
                Query = "SELECT c.name Город, p.name Издательство FROM publisher p JOIN city c ON c.id = p.id_city ORDER BY c.name, p.name";
                //new HtmlReport("7.html", new string[] { "Город", "Издательство" }).Parse(Query);
            }
            else if (listBox3.SelectedIndex == 7)
            {
                label10.Text = null;
                Query = "SELECT c.id id, c.name Город FROM city c LEFT JOIN publisher p ON p.id_city = c.id ORDER BY c.name";
                //new HtmlReport("8.html", new string[] { "id", "Город" }).Parse(Query);
            }
            else if (listBox3.SelectedIndex == 8)
            {
                InputAuthor ia = new InputAuthor();
                ia.ShowDialog(this);
                if (ia.GetResult() == null)
                    return;

                string[] Author = ia.GetResult().Split(' ');
                Query = "SELECT b.name Книга, b.photo Фото "+
                        "FROM book_author ba "+
                        "RIGHT JOIN book b ON b.id = ba.id_book "+
                        "JOIN author a ON a.id = ba.id_author " +
                        $"WHERE a.name = '{Author[1]}' AND a.second_name = '{Author[0]}' AND a.last_name = '{Author[2]}'ORDER BY b.name";
                label10.Text = "Автор: " + Author[0] + " " + Author[1] + " " + Author[2];
                //new HtmlReport("9.html", new string[] { "Книга", "Фото" }).Parse(Query);
            }
            else if (listBox3.SelectedIndex == 9)
            {
                InputPeriod ip = new InputPeriod();
                ip.ShowDialog(this);
                if (ip.GetResult() == null)
                    return;
                int FirstYear, SecondYear;
                try
                {
                    FirstYear = Convert.ToInt32(ip.GetResult()[0]);
                    SecondYear = Convert.ToInt32(ip.GetResult()[1]);
                    if (FirstYear > SecondYear)
                        throw new Exception();
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = $"SELECT DISTINCT c.id id, c.name Город FROM city c " +
                    $"LEFT JOIN publisher p ON p.id_city = c.id " +
                    $"WHERE c.id NOT IN (SELECT c.id FROM publisher p " +
                    $"JOIN city c ON p.id_city = c.id WHERE p.date_create >= {FirstYear} AND p.date_create <= {SecondYear}) ORDER BY c.name; ";
            }
            else if (listBox3.SelectedIndex == 10)
            {
                Query = $"SELECT c.name Город, COUNT(p.id) \"Количество издательств\" FROM city c JOIN publisher p ON p.id_city = c.id GROUP BY (c.name) ORDER BY c.name";
            }
            else if (listBox3.SelectedIndex == 11)
            {
                InputPeriod ip = new InputPeriod();
                ip.ShowDialog(this);
                if (ip.GetResult() == null)
                    return;
                int FirstYear, SecondYear;
                try
                {
                    FirstYear = Convert.ToInt32(ip.GetResult()[0]);
                    SecondYear = Convert.ToInt32(ip.GetResult()[1]);
                    if (FirstYear > SecondYear)
                        throw new Exception();
                }
                catch { Message.ErrorShow("Значение указано неверно."); return; }
                Query = $"SELECT b.name Книга, COUNT(b.id) Тираж FROM book b WHERE b.date_public >= {FirstYear} AND b.date_public <= {SecondYear} GROUP BY (b.name) ORDER BY b.name";
                label10.Text = "Период: " + FirstYear.ToString() + "-" + SecondYear.ToString();
            }
            else if (listBox3.SelectedIndex == 12)
            {
                it.ShowDialog(this);
                if (it.GetResult() == null)
                    return;
                int Cost;
                try
                {
                    Cost = Convert.ToInt32(it.GetResult());
                }
                catch { return; }

                Query = $"SELECT s.id, s.name Магазин, SUM(d.cost*d.count_book) \"Стоимость закупок\", COUNT(d.id) \"Количество поставок\" FROM deliveries d " +
                        "JOIN shop s ON s.id = d.id_shop " +
                        "GROUP BY(s.id) " +
                        $"HAVING SUM(d.cost* d.count_book) > {Cost} " +
                        "ORDER BY(s.name)";
                label10.Text = "Общая стоимость превышает: " + Cost.ToString();
            }
            else if (listBox3.SelectedIndex == 13)
            {
                it.ShowDialog(this);
                if (it.GetResult() == null)
                    return;
                int Cost;
                try
                {
                    Cost = Convert.ToInt32(it.GetResult());
                }
                catch { return; }
                InputDates id = new InputDates();
                id.ShowDialog(this);
                if (id.GetResult() == null)
                    return;
                string[] date = id.GetResult();
                Query = $"SELECT s.id, s.name Магазин, SUM(d.cost*d.count_book) \"Стоимость закупок\", COUNT(d.id) \"Количество поставок\" FROM deliveries d " +
                        "JOIN shop s ON s.id = d.id_shop " +
                        $"WHERE d.date_come >= '{date[0]}' AND d.date_come <= '{date[1]}' " +
                        "GROUP BY(s.id) " +
                        $"HAVING SUM(d.cost* d.count_book) > {Cost} " +
                        "ORDER BY(s.name)";
                label10.Text = "Общая стоимость превышает: " + Cost.ToString();
            }
            else if (listBox3.SelectedIndex == 14)
            {
                Query = "SELECT s.name, COUNT(s.id) \"Количество магазинов\", MAX(dc.count) \"Количество поставок\" FROM shop s "+
                        "JOIN(SELECT s.name, COUNT(d.id) FROM deliveries d JOIN shop s ON s.id = " +
                        "d.id_shop GROUP BY(s.name)) dc ON s.name = dc.name GROUP BY (s.name)";
            }
            else if (listBox3.SelectedIndex == 15)
            {
                Query = "SELECT  p.name Название, c.name Город, p.phone Телефон, p.date_create \"Дата создания\" FROM publisher p JOIN city c ON p.id_city = c.id WHERE p.date_create > (SELECT AVG(p.date_create) FROM publisher p)";
            }
            else if (listBox3.SelectedIndex == 16)
            {
                it.ShowDialog(this);
                if (it.GetResult() == null)
                    return;
                string Value;
                try
                {
                    Value = InputData.CheckString(it.GetResult());
                }
                catch { return; }
                Query = "SELECT d.id_shop, s.name Магазин, ROUND(COUNT(DISTINCT d.id_book)/ "+
                    "(SELECT COUNT(DISTINCT d.id_book) FROM deliveries d "+
                    "JOIN shop s ON s.id = d.id_shop "+
                    "JOIN area a ON a.id = s.id_area "+
                    $"WHERE EXTRACT(YEAR FROM d.date_come) >= 2019 AND a.name = {Value})::numeric*100, 2) Процент " +
                    "FROM deliveries d "+
                    "JOIN shop s ON s.id = d.id_shop "+
                    "JOIN area a ON a.id = s.id_area "+
                    $"WHERE EXTRACT(YEAR FROM d.date_come) >= 2019 AND a.name = {Value} "+
                    "GROUP BY(d.id_shop, s.name, a.id)";

                label10.Text = "Район: " + Value;
            }
            else if(listBox3.SelectedIndex == 17)
            {
                Query = "SELECT a.name Район, ROUND(COUNT(DISTINCT d.id_book)/" +
                "(SELECT COUNT (DISTINCT d.id_book) FROM deliveries d WHERE EXTRACT(YEAR FROM d.date_come) >= 2019)::numeric*100, 2) Процент " +
                "FROM deliveries d " +
                "JOIN shop s ON s.id=d.id_shop " +
                "JOIN area a ON a.id=s.id_area " +
                "WHERE EXTRACT(YEAR FROM d.date_come) >= 2019 " +
                "GROUP BY(a.name)";
            }
            else if(listBox3.SelectedIndex == 18)
            {
                Query = "SELECT p.name Издательство, ROUND(COUNT(DISTINCT b.id)/COUNT(p.id)::numeric, 2) Количество " +
                    "FROM deliveries d " +
                    "JOIN book b ON b.id = d.id_book " +
                    "JOIN publisher p ON p.id = b.id_publisher " +
                    "GROUP BY(p.name) " +
                    "ORDER BY(p.name)";
            }
            else if(listBox3.SelectedIndex == 19)
            {
                Query = "SELECT COUNT(DISTINCT b.id)/COUNT(DISTINCT p.id) Количество "+
                "FROM deliveries d "+
                "JOIN book b ON b.id = d.id_book "+
                "JOIN publisher p ON p.id = b.id_publisher";
            }
            else if(listBox3.SelectedIndex == 20)
            {
                Query = "SELECT s.id, s.name Магазин, EXTRACT(YEAR from d.date_come) Год, EXTRACT(MONTH from d.date_come) Месяц, SUM(d.count_book) Количество, SUM(d.count_book*d.cost) Стоимость FROM deliveries d "+
                    "JOIN shop s ON s.id = d.id_shop "+
                    "WHERE EXTRACT(YEAR from d.date_come) >= 2018 AND EXTRACT(YEAR from d.date_come) <= 2018 "+
                    "GROUP BY(s.id, EXTRACT(YEAR from d.date_come), EXTRACT(MONTH from d.date_come)) "+
                    "ORDER BY(s.id, EXTRACT(YEAR from d.date_come), EXTRACT(MONTH from d.date_come))";
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
                Action action = () => { UpdateDatagrid(dataGridView2, listBox2, label9); UpdateDatagrid(dataGridView1, listBox1, label1); };
                Invoke(action);
                
            }).Start();
            
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
                    if (!(node.Cells[index].Value.ToString() == text))
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

        private void button13_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("Диаграмма");
            if (listBox4.SelectedItem == null)
                return;
            if (listBox4.SelectedIndex == 0)
                PieDiagramm();
            else if (listBox4.SelectedIndex == 1)
                ColumnDiagramm();
            else if (listBox4.SelectedIndex == 2)
                ThreeColumnDiagramm();

        }
        private void ThreeColumnDiagramm()
        {
            listBox5.Visible = false;
            var count = chart1.Series.Add("Стоимость закупок для магазинов");
            var shopcost = chart1.Series.Add("Стоимость закупок для поставщиков");
            var defcost = chart1.Series.Add("Разница в цене");

            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                string Query = "SELECT EXTRACT(YEAR FROM d.date_come),SUM(d.cost), SUM(d.def_cost), SUM(d.cost) - SUM(d.def_cost) FROM deliveries d GROUP BY (EXTRACT(YEAR FROM d.date_come)) ORDER BY (EXTRACT(YEAR FROM d.date_come));";
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(Query, connect);
                    command.ExecuteNonQuery();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count.Points.AddXY(reader.GetDouble(0), reader.GetDouble(1));
                        shopcost.Points.AddXY(reader.GetDouble(0), reader.GetDouble(2));
                        defcost.Points.AddXY(reader.GetDouble(0), reader.GetDouble(3));
                    }
                }
                catch (Npgsql.PostgresException ex)
                {
                }
                connect.Close();
            }
        }
        private void PieDiagramm()
        {
            listBox5.Visible = false;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart1.Series[0].Points.Clear();
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                string Query = "SELECT o.name, (COUNT(o.id)/(SELECT COUNT(*) FROM shop s)::float) FROM shop s JOIN own o ON o.id = s.id_own GROUP BY(o.name)";
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(Query, connect);
                    command.ExecuteNonQuery();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chart1.Series[0].Points.AddY(reader.GetDouble(1));
                        chart1.Series[0].Points[chart1.Series[0].Points.Count - 1].LegendText = reader.GetString(0) + ": " + Math.Round(reader.GetDouble(1)*100, 2).ToString() + "%";
                    }
                }
                catch (Npgsql.PostgresException ex)
                {
                }
                connect.Close();
            }
        }
        private void ColumnDiagramm()
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[0].Points.Clear();
            
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                string Query = "SELECT c.name, COUNT(p.id) FROM city c JOIN publisher p ON p.id_city = c.id GROUP BY(c.name) ORDER BY c.name";
                connect.Open();
                try
                {
                    NpgsqlCommand command = new NpgsqlCommand(Query, connect);
                    command.ExecuteNonQuery();
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        chart1.Series[0].Points.AddY(reader.GetInt32(1));
                        chart1.Series[0].Points[chart1.Series[0].Points.Count - 1].LegendText = reader.GetString(0);
                    }
                    foreach (DataPoint dp in chart1.Series[0].Points)
                    {
                        dp.Color = System.Drawing.Color.SkyBlue;
                    }
                }
                catch (Npgsql.PostgresException ex)
                {
                }
                connect.Close();
            }
            listBox5.Visible = true;
            using (NpgsqlConnection connect = SQL.GetConnection())
            {
                listBox5.Items.Clear();
                connect.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT c.name FROM city c JOIN publisher p ON p.id_city = c.id GROUP BY(c.name) ORDER BY c.name", connect);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listBox5.Items.Add(reader.GetString(0));
                }
                connect.Close();
            }
        }

        private void listBox5_MouseClick(object sender, MouseEventArgs e)
        {
            foreach(DataPoint dp in chart1.Series[0].Points)
            {
                dp.Color = System.Drawing.Color.SkyBlue;
            }
            chart1.Series[0].Points[listBox5.SelectedIndex].Color = System.Drawing.Color.Red;
        }
    }
}
