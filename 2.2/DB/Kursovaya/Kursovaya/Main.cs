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

            listBox2.Items.Add("Районы");
            listBox2.Items.Add("Города");
            listBox2.Items.Add("Языки");
            listBox2.Items.Add("Типы собственности");
            listBox2.Items.Add("Жанры");
            listBox2.Items.Add("Книги-авторы");
            listBox2.Items.Add("Типы переплета");
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
            EditShop Shop = new EditShop();
            Shop.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            EditSup AddCountry = new EditSup(listBox2.SelectedItem.ToString());
            AddCountry.Show();
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
            EditCountry EditContry = new EditCountry();
            EditContry.Show();
        }

        string list1Item, list2Item;
        
        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem == null) return;
            else if (list1Item !=listBox1.SelectedItem.ToString() && listBox1.SelectedItem.ToString() == "Магазины")
            {
                list1Item = listBox1.SelectedItem.ToString();
                DataTable dt = new DataTable();

                using(NpgsqlConnection connect = new SQL().GetConnection())
                {
                    connect.Open();

                    //NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM shop;", connect);
                    //NpgsqlDataReader reader = command.ExecuteReader();
                    NpgsqlDataAdapter sda = new NpgsqlDataAdapter("SELECT shop.shop_id as ID," +
                        "shop.shop_name AS Название," +
                        "shop.date_open AS \"Дата открытия\"," +
                        "area.name_area AS Район," +
                        "shop.address AS Адресс," +
                        "own.name_own AS \"Тип собственности\"" +
                        "FROM shop," +
                        "area, own WHERE shop.id_area = area.id_area AND shop.id_own = own.id_own", connect);
                    sda.Fill(dt);
                    dataGridView1.DataSource = dt;
                    connect.Close();
                }
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

        private void tabPage2_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox2.SelectedItem == null) return;
            else if (listBox2.SelectedItem.ToString() == "Район")
            {
                AreaCreateColumns();
            }
        }
    }
}
