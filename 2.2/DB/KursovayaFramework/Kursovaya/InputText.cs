using System;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class InputText : Form
    {
        public InputText()
        {
            InitializeComponent();
        }
        string Text;
        private void button1_Click(object sender, EventArgs e)
        {
            Text = textBox1.Text;
            Hide();
        }
        public string GetResult() { return Text; }
    }
}
