using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _4lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string path;
        Catalog catalog;
        private void button1_Click(object sender, EventArgs e)
        {

            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) 
            {
                label1.Text = "Время указания каталога: ";
                this.path = fbd.SelectedPath;
                catalog = new Catalog();
                label1.Text += catalog.GetTime();
                button1.Enabled = true;
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            catalog.FindFolders(this.path);
            button2.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }
    }
}
