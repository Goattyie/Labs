using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5Lab
{
    public partial class Form1 : Form
    {
        public Form1(Bitmap img)
        {
            InitializeComponent();
            this.img = (Image)img;

        }
        Image img;
        PictureBox picbox = new PictureBox();
        const int SIZE_W = 800, SIZE_H = 400;
        private void Form1_Load(object sender, EventArgs e)
        {
            picbox.Location = new Point(0, 0);
            picbox.Name = "Picbox";
            picbox.TabIndex = 0;
            picbox.TabStop = false;
            picbox.Parent = this;
            if (img.Width < SIZE_W && img.Height < SIZE_H)
            {
                picbox.Size = img.Size;
                picbox.Image = img;
                this.Size = picbox.Size;
            }
            else
            {
                int w = img.Width / SIZE_W, h = img.Height / SIZE_H;
                if(w >= h)
                {
                    Bitmap bitmap = new Bitmap(img);
                    Size size = new Size(SIZE_W, img.Height / w);
                    Bitmap newBitmap = new Bitmap(bitmap, size);
                    Image img2 = newBitmap;
                    picbox.Size = img2.Size;
                    picbox.Image = img2;
                    this.Size = picbox.Size;
                }
                else
                {
                    Bitmap bitmap = new Bitmap(img);
                    Size size = new Size(img.Width / h, SIZE_H);
                    Bitmap newBitmap = new Bitmap(bitmap, size);
                    Image img2 = newBitmap;
                    picbox.Size = img2.Size;
                    picbox.Image = img2;
                    this.Size = picbox.Size;
                }
            }
        }
    }
}
