using System;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class Searcher : Form
    {
        public Searcher(DataGridView dgv)
        {
            InitializeComponent();
            Columns = new string[dgv.Columns.Count];
            for (int i = 0; i < Columns.Length; i++)
                Columns[i] = dgv.Columns[i].HeaderText;
        }
        string[] Columns;
        string Text;
        int[] SearchIndexes; //Колонки для поиска
        private void button2_Click(object sender, EventArgs e)
        {
            SetIndexes();
            Text = textBox1.Text;
            Hide();
        }
        public int[] GetIndexes() { return SearchIndexes; }
        public string GetText() { return Text; }
        private void SetIndexes()
        {
            SearchIndexes = new int[listBox1.Items.Count];
            int n = 0;
            for (int i = 0; i < Columns.Length; i++)
            {
                foreach (string item in listBox1.Items)
                {
                    if (Columns[i] == item)
                    {
                        SearchIndexes[n] = i;
                        n++;
                    }
                }
            }
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
