using System;
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
