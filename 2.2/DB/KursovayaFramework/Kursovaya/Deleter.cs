using System;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class Deleter : Form
    {
        public Deleter(DataGridView dgv)
        {
            InitializeComponent();
            for (int i = 0; i < dgv.ColumnCount; i++)
                listBox1.Items.Add(dgv.Columns[i].HeaderText);
        }
        string Text;
        int Index; //Колонки для поиска

        private void button1_Click(object sender, EventArgs e)
        {
            Index = listBox1.SelectedIndex;
            Text = textBox1.Text;
            Hide();
        }
        public int GetIndex() { return Index; }
        public string GetText() { return Text; }
    }
}
