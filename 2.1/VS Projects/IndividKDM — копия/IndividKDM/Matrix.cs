using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndividKDM
{
    public partial class Matrix : Form
    {
        int size;
        public Matrix(int text)
        {
            this.size = text;
            InitializeComponent();
        }

        private void Matrix_Load(object sender, EventArgs e)
        {
            label1.Text = label1.Text + size.ToString();
        }
    }
}
