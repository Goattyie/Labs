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
    public partial class InputPeriod : Form
    {
        public InputPeriod()
        {
            InitializeComponent();
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
