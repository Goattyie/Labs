using System;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class InputPeriod : Form
    {
        public InputPeriod()
        {
            InitializeComponent();
        }
        public InputPeriod(string lab1, string lab2)
        {
            InitializeComponent();
            label1.Text = lab1;
            label2.Text = lab2;
        }
        string[] TextT;
        private void button1_Click(object sender, EventArgs e)
        {
            TextT = new string[2];
            TextT[0] = textBox1.Text;
            TextT[1] = textBox2.Text;
            Hide();
        }
        public string[] GetResult() { return TextT; }
    }
}
