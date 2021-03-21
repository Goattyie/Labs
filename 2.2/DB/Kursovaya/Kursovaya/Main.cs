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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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

        }
        private void Main_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "Основные таблицы";
            tabPage2.Text = "Справочники";
            tabPage3.Text = "Запросы";

            SetTagPage2();
            SetTagPage3();
        }

        private void tagPage1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage.Text == "Основные таблицы")
            {
                //Открыть форму Request
            }
            else if (e.TabPage.Text == "Справочники")
            {
                //Открыть форму Request
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
            AddNoteCountry AddCountry = new AddNoteCountry();
            AddCountry.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddNoteCountry AddCountry = new AddNoteCountry();
            AddCountry.Show();
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
    }
}
