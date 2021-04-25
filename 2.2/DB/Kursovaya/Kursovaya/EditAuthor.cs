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
    public partial class EditAuthor : Form
    {
        public EditAuthor()
        {
            InitializeComponent();
        }
        string Name, SecondName, LastName;
        private void button1_Click(object sender, EventArgs e)
        {
            Name = InputData.CheckString(textBox1.Text);
            SecondName = InputData.CheckString(textBox2.Text);
            LastName = InputData.CheckString(textBox3.Text);

            if (new Author(Name, SecondName, LastName).Insert())
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
                
        }
    }
}
