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
            listBox3.Size = listBox1.Size;
            listBox3.Location = listBox1.Location;

            label4.Text = label2.Text;
            label4.Location = label2.Location;
            label4.Size = label2.Size;
            label4.Font = label2.Font;

            label5.Location = label6.Location;
            label5.Size = label2.Size;
            label5.Font = label2.Font;

            listBox4.Size = dataGridView1.Size;
            listBox4.Location = dataGridView1.Location;

            button5.Location = button1.Location;
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

            dataGridView2.ColumnHeadersHeight = 30;
        }
        private void tagPage1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text == "Основные таблицы")
            {
                //Открыть форму Request
            }
            else if (e.TabPage.Text == "Справочники")
            {
                //Открыть форму Request
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
                new EditShop().Show();
            else if (listBox1.SelectedItem.ToString() == "Поставки")
                new EditDeliveries().Show();
            else if (listBox1.SelectedItem.ToString() == "Книги")
                new EditBook().Show();
            else if (listBox1.SelectedItem.ToString() == "Издательства")
                new EditPublisher().Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                EditSup AddSup = new EditSup(listBox2.SelectedItem.ToString());
                AddSup.Show();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            RequersResult RR = new RequersResult();
            RR.Show();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            else
            {
                string table = null;
                DataTable dt = new DataTable();
                if (listBox2.SelectedItem.ToString() == "Магазины")
                    table = "shop";
                else if (listBox2.SelectedItem.ToString() == "Поставки")
                    table = "deliveries";
                else if (listBox2.SelectedItem.ToString() == "Книги")
                    table = "book";
                else if (listBox2.SelectedItem.ToString() == "Издательства")
                    table = "publisher";

                /*
                using (NpgsqlConnection connect = SQL.GetConnection())
                {
                    if (SQL.DeleteWarning() == DialogResult.OK)
                    {
                        connect.Open();
                        for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
                            SQL.DeleteSup(table, dataGridView2[1, dataGridView2.SelectedRows[i].Index].Value.ToString(), connect);
                        connect.Close();
                    }
                }
                */
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            else
            {
                DataTable dt = new DataTable();
                Table.ReturnTable(listBox1.SelectedItem.ToString()).Select().Fill(dt);
                dataGridView1.DataSource = dt; 
            }
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
            if (listBox2.SelectedItem == null) return;
            else
            {
                DataTable dt = new DataTable();
                Table.ReturnTable(listBox2.SelectedItem.ToString()).Select().Fill(dt);
                dataGridView2.DataSource = dt;
                
            }
        }

        private void tabPage2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            else if (listBox2.SelectedItem.ToString() == "Район")
            {
                AreaCreateColumns();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            else
            {
                string table = null;
                DataTable dt = new DataTable();
                if (listBox2.SelectedItem.ToString() == "Район")
                    table = "area";
                else if (listBox2.SelectedItem.ToString() == "Тип собственности")
                    table = "own";
                else if (listBox2.SelectedItem.ToString() == "Город")
                    table = "city";
                else if (listBox2.SelectedItem.ToString() == "Язык")
                    table = "lang";
                else if (listBox2.SelectedItem.ToString() == "Жанр")
                    table = "style";
                else if (listBox2.SelectedItem.ToString() == "Тип переплета")
                    table = "binding";


                using (NpgsqlConnection connect = SQL.GetConnection())
                {
                    if (SQL.DeleteWarning() == DialogResult.OK) 
                    {
                        connect.Open();
                        for (int i = 0; i < dataGridView2.SelectedRows.Count; i++)
                            SQL.DeleteSup(table, dataGridView2[1, dataGridView2.SelectedRows[i].Index].Value.ToString(), connect);
                        connect.Close();
                    }
                }
            }
        }
    }
}
