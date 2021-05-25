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
    public partial class InputDates : Form
    {
        public InputDates()
        {
            InitializeComponent();
        }
        string Date1, Date2;
        private void button1_Click(object sender, EventArgs e)
        {
            Date1 = dateTimePicker1.Value.ToShortDateString();
            Date2 = dateTimePicker2.Value.ToShortDateString();
            Hide();
        }
        public string[] GetResult() { return new string[] {Date1, Date2 }; }
    }
}
