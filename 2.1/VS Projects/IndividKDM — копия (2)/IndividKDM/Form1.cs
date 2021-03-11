using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace IndividKDM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int SIZE_X = 968;
        public static int SIZE_Y = 485;
        public static int CENTER_X = SIZE_X / 2;
        public static int CENTER_Y = SIZE_Y / 2;
        public static int RADIUS = 200;
        public static int N_B = 0, P_Y, P_X;
        public static Bitmap bmp = new Bitmap(SIZE_X, SIZE_Y);
        Brush brush = Brushes.Black;
        Pen pen = new Pen(Color.Black, 3);
        Font font = new Font("Calibri", 13, FontStyle.Bold);
        Point point = new Point();
        bool isDragging = false;//отвечает за то перетаскивать или нет picturebox3
        int currentX, currentY;
        Vertex[] temp = new Vertex[2];
        List<Vertex> lvert = new List<Vertex>();
        List<int> T = new List<int>();
        List<int> B = new List<int>();
        List<int> matrix = new List<int>();
        List<int> Nbr = new List<int>();
        List<int> Fst = new List<int>();
        List<int> Adj = new List<int>();
        List<int> Mark = new List<int>();
        
        int nT = 0;


        public class Vertex
        {
           Graphics graph = Graphics.FromImage(bmp);
           public int name;
           static int SIZE = 15;
           public int x, y;
           public Image circle = Image.FromFile("Circle.png");

           public Vertex(int x, int y, int name)
           {
                this.x = x;
                this.y = y;
                this.name = name;
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
        
        Vertex[] allvert;
        private void FindInDeep(Vertex[] allvert)
        {
           
        }
         
        private void Form1_Load(object sender, EventArgs e)
        {
            Canvas.Image = bmp;
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            Vertex node = new Vertex(x,y, lvert.Count+1);
            lvert.Add(node);
            Canvas.Invalidate();

        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            Vertex[] connect = new Vertex[2];
            int x = 0, y = 0;
            int SIZE = 15;
            if (e.Button == MouseButtons.Right)
            {

                for (int i = 0; i < lvert.Count; i++)
                {

                    if ((lvert[i].x >= e.X - SIZE && lvert[i].x  <= e.X + SIZE) && (lvert[i].y  >= e.Y - SIZE && lvert[i].y  <= e.Y + SIZE))
                    {
                        if (temp[0] == null)
                        {
                            temp[0] = lvert[i];
                           
                        }
                        else if (temp[1] == null)
                        {
                            x = lvert[i].GetX();
                            temp[1] = lvert[i];
                            connect[0] = temp[0];
                            connect[1] = temp[1];
                            
                            Canvas.Refresh();
                            temp[0] = null;
                            temp[1] = null;
                        }

                    }


                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                isDragging = true;
                currentX = e.X;
                currentY = e.Y;
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;
                int SIZE = 15;

                for (int i = 0; i < lvert.Count; i++)
                {
                    if ((lvert[i].x >= x - SIZE && lvert[i].x <= x + SIZE) && (lvert[i].y >= y - SIZE && lvert[i].y <= y + SIZE))
                    {
                        
                        lvert[i].SetX(x);
                        lvert[i].SetY(y);

                        Canvas.Refresh();
                        
                    }
                    
                }
               
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Vertex v;
            StreamReader sr = new StreamReader("graph1.txt");
            int count = System.IO.File.ReadAllLines("graph1.txt").Length;
            string[] masline;
            double interval = 360 / count;
            string line;
            double X = CENTER_X + RADIUS;
            double Y = CENTER_Y;
            while ((line = sr.ReadLine()) != null)
            {
                line = Regex.Replace(line, @"\t", " ");
                masline = line.Split(' ');
                double X2 = CENTER_X + (RADIUS * Math.Cos(interval * Math.PI / 180.0));
                double Y2 = CENTER_Y + (RADIUS * Math.Sin(interval * Math.PI / 180.0));
                v = new Vertex(Convert.ToInt32(X2), Convert.ToInt32(Y2), Convert.ToInt32(masline[0]));
                interval = interval + 360 / count;
                lvert.Add(v);
                Nbr.Add(Convert.ToInt32(masline[1]));
                if(Fst.Count == 0)
                {
                    Fst.Add(0);
                }
                Fst.Add(Fst[Fst.Count - 1] + Nbr[Nbr.Count-1]);
                string line2 = line.Replace(" ","").Substring(2);
                Adj.Add(Convert.ToInt32(line2));
            }
            sr.Close();
            Canvas.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           /* void Init(bool yes)
            {
                int i, j, m;
                for (i = 0; i < lvert.Count; i++)
                {
                    for (j = 0; j < Nbr[i]; j++)
                    {
                        yes = false;
                        for(m = 0; m < lvert.Count; m++)
                        {
                            if (Adj[Fst[i] + j] == lvert[m].name)
                                yes = true;
                            if (Adj[Fst[i] + j] == m)
                                break;
                        }
                        if (yes == false)
                            return;
                    }
                }
            }
            */
            void Depth(int x, int u, int count)
            {
                int i, v;
                count = count + 1;
                Mark[x] = count;
                for(i = 0; i < Nbr[x]; i++)
                {
                    v = Adj[Fst[x] + i].ToString().ToCharArray;
                    if (Mark[i] == 0)
                    {
                        nT = nT + 2;
                        T[nT - 1] = x;
                        T[nT] = v;
                        B[nT / 2] = 1;
                        Depth(v, x, count);
                    }
                    else if(Mark[v] < Mark[x] && v != u)
                    {
                        nT = nT + 2;
                        T[nT - 1] = x;
                        T[nT] = v;
                        B[nT / 2] = 0;
                    }
                }
            }

                int ve, counte;
                nT = 0;
                counte = 0;
                for( ve = 0; ve < lvert.Count; ve++)
                {
                    Mark.Add(0);
                    for(ve = 0; ve < lvert.Count; ve++)
                    {
                        if( Mark[ve] == 0)
                        {
                            Depth(ve, 0, counte);
                        }
                    }
                }

        }


        private void Canvas_Click(object sender, EventArgs e)
        {

        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            for(int i = 0; i < lvert.Count-1; i++)
            {
                string str = Adj[i].ToString();
                char[] str2 = str.ToCharArray();
                for(int j = 0; j < str2.Length; j++)
                {
                    e.Graphics.DrawLine(pen, lvert[i].GetX(), lvert[i].GetY(), lvert[Convert.ToInt32(str2[j].ToString())-1].GetX(), lvert[Convert.ToInt32(str2[j].ToString())-1].GetY());
                }
            }
           
            for (int i = 0; i < lvert.Count; i++)
            {
                e.Graphics.DrawImage(lvert[i].circle, lvert[i].x - 15, lvert[i].y - 15);
                e.Graphics.DrawString(lvert[i].name.ToString(), font, brush, lvert[i].GetX()-13, lvert[i].GetY()-10);
            }
                
        }
    }
}
