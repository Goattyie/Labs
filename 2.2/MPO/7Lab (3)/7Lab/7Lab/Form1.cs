﻿using System;
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
            if (numericUpDown1.Value < 3)
            {
                MessageBox.Show("Количество граней не может быть меньше 3", "Ошибка");
                numericUpDown1.Value = 3;
                return;
            }
        }
    }
}
