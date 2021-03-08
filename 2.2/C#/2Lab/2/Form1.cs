using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private Bitmap AdjustGamma(Image image, float gamma)
        {
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetGamma(gamma);

            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            return bm;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            var state = openFileDialog1.ShowDialog();

            if (state == DialogResult.Cancel)
                return;

            if (state == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                pictureBox2.Image = (Image)AdjustGamma(pictureBox1.Image, 1F);
            }

            button2.Enabled = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "*.jpeg | *.jpeg";

            var state = saveFileDialog1.ShowDialog();

            if (state == DialogResult.Cancel)
                return;

            pictureBox2.Image.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
        }

        public struct YCbCrColor
        {
            public byte Y { set; get; }
            public byte Cb { set; get; }
            public byte Cr { set; get; }

            public YCbCrColor(byte y, byte cb, byte cr)
                : this()
            {
                Y = y;
                Cb = cb;
                Cr = cr;
            }

            public Color ToRgbColor()
            {
                int r = Convert.ToInt32((double)this.Y +
                    1.402 * (double)(this.Cr - 128));
                int g = Convert.ToInt32((double)this.Y -
                    0.34414 * (double)(this.Cb - 128) -
                    0.71414 * (double)(this.Cr - 128));
                int b = Convert.ToInt32((double)this.Y +
                    1.772 * (double)(this.Cb - 128));
                /*if (r < 0)
                    r = 0;
                if (g < 0)
                    g = 0;
                if (b < 0)
                    b = 0;
                if (r > 255)
                    r = 255;
                if (g > 255)
                    g = 255;
                if (b > 255)
                    b = 255;
                */
                return Color.FromArgb(r, g, b);
            }

            public static YCbCrColor FromRgbColor(Color color)
            {

                byte y = Convert.ToByte(0.299 * (double)color.R +0.587 * (double)color.G +0.114 * (double)color.B);
                byte cb = Convert.ToByte(128 - 0.168736 * (double)color.R - 0.331264 * (double)color.G + 0.5 * (double)color.B);
                byte cr = Convert.ToByte(128 + 0.5 * (double)color.R - 0.418688 * (double)color.G - 0.081312 * (double)color.B);

                return new YCbCrColor(y, cb, cr);
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null)
            {
                YCbCrColor ybc;

                Bitmap bmp = new Bitmap(pictureBox2.Image);
                
                    for (int i = 0; i < pictureBox2.Image.Size.Width; i++)
                    {
                        for (int j = 0; j < pictureBox2.Image.Size.Height; j++)
                        {

                            Color clr = bmp.GetPixel(i, j); // Получить цвет пикселя в точке (5, 5)
                            ybc = YCbCrColor.FromRgbColor(clr);
                            bmp.SetPixel(i, j, ybc.ToRgbColor());

                        }
                        
                    }
                    
                pictureBox2.Image = (Image)bmp;
                
            }
        }
    }
}
