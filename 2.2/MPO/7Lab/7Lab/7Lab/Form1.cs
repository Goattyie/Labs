using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7Lab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.piramida;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 3 || listBox2.SelectedItems.Count != 1)
                return;
            foreach (string item in listBox1.SelectedItems) 
            {
                if (item == listBox2.SelectedItem.ToString())
                    return;
            }

            string[] inputData = new string[listBox1.SelectedItems.Count];
            string[] outputData = new string[listBox2.SelectedItems.Count];
            for (int i = 0; i < inputData.Length; i++)
                inputData[i] = listBox1.SelectedItems[i].ToString();
            for (int i = 0; i < outputData.Length; i++)
                outputData[i] = listBox2.SelectedItems[i].ToString();
            new Synthesizer(inputData, outputData);
        }
    }
}
