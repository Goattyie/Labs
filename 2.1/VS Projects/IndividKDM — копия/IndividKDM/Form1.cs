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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int SIZE_X = 776;
        public static int SIZE_Y = 426;
        public static int N_B = 0, P_Y, P_X;
        public static Bitmap bmp = new Bitmap(SIZE_X, SIZE_Y);
        Brush brush = Brushes.Black;
        Pen pen = new Pen(Color.Black, 3);
        Font font = new Font("Calibri", 10, FontStyle.Bold);
        bool isDragging = false;//отвечает за то перетаскивать или нет picturebox3
        int currentX, currentY;


        public class Vertex
        {
           Graphics graph = Graphics.FromImage(bmp);
           static int SIZE = 15;
           public int x, y;
           public Image circle = Image.FromFile("Circle.png");

           public Vertex(int x, int y)
           {
                this.x = x;
                this.y = y;
           }
            public int GetX()
            {
                return this.x;
            }
            public int GetY()
            {
                return this.y;
            }
            
           public void Draw()
            {
                graph.DrawImage(circle, this.x, this.y);
            }
            
            public void SetX(int x)
            {
                this.x = x;
            }

            public void SetY(int y)
            {
                this.y = y;
            }
            

        }
        List<Vertex> lvert = new List<Vertex>();
        private void Form1_Load(object sender, EventArgs e)
        {
            Canvas.Image = bmp;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

           if (e.Button == MouseButtons.Middle)
            {
                isDragging = true;
                currentX = e.X;
                currentY = e.Y;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
 
            if (isDragging)
            {
                Canvas.Top = Canvas.Top + (e.Y - currentY);
                Canvas.Left = Canvas.Left + (e.X - currentX);
            }


        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Middle)
            {
                isDragging = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Matrix matrix = new Matrix(Convert.ToInt32(textBox1.Text));
            matrix.Visible = true;
        }

        private void Canvas_Click(object sender, EventArgs e)
        {

        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {



        }
    }
}
