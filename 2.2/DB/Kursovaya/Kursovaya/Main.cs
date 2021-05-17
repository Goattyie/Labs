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
            RequersResult RR = new RequersResult();
            RR.Show();
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
    }
}
