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
    public partial class Searcher : Form
    {
        public Searcher(DataGridView dgv, string name)
        {
            InitializeComponent();

            ds.Tables.Add("Таблица");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.ReadOnly = true;
            Columns = new string[dgv.Columns.Count];
            for (int i = 0; i < Columns.Length; i++)
            {
                Columns[i] = dgv.Columns[i].HeaderText;
                ds.Tables[0].Columns.Add(dgv.Columns[i].HeaderText);
            }
            Nodes = dgv.Rows;
        }
        DataGridViewRowCollection Nodes;
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        string[] Columns;
        int[] SearchIndexes; //Колонки для поиска
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;

            ds.Tables[0].Clear();
            SetIndexes(); //Ищем индексы колонок для поиска
            foreach(DataGridViewRow node in Nodes)
            {
                bool find = true;
                foreach(int index in SearchIndexes)
                {
                    if (!node.Cells[index].Value.ToString().Contains(textBox1.Text))
                        find = false;
                }
                if(find)
                    ds.Tables[0].Rows.Add(CreateNode(node));
            }
        }
        private void SetIndexes()
        {
            SearchIndexes = new int[listBox1.Items.Count];
            int n = 0;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                foreach (string item in listBox1.Items)
                {
                    if (dataGridView1.Columns[i].HeaderText == item)
                    {
                        SearchIndexes[n] = i;
                        n++;
                    }
                }
            }
        }
        private string[] CreateNode(DataGridViewRow node)
        {
            string[] n = new string[Columns.Length];
            for (int i = 0; i < n.Length; i++)
                n[i] = node.Cells[i].Value.ToString();
            return n;
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (string header in Columns)
                comboBox1.Items.Add(header);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (string item in listBox1.Items)
            {
                if (item == comboBox1.Text)
                {
                    MessageBox.Show("Данное поле уже добавлен в список.", "Ошибка013", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            listBox1.Items.Add(comboBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                listBox1.Items.Remove(listBox1.SelectedItems[i]);
            }
        }
    }
}
