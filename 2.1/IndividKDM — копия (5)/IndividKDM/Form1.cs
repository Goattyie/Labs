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
        public static int SIZE_Y = 700;
        public static int CENTER_X = SIZE_X / 2;
        public static int CENTER_Y = SIZE_Y / 2;
        public static int RADIUS = 320;
        public static int N_B = 0, P_Y, P_X;
        public static Bitmap bmp = new Bitmap(SIZE_X, SIZE_Y);
        Brush brush = Brushes.Black;
        Pen pen = new Pen(Color.Black, 2);
        Pen pen2 = new Pen(Color.Red, 2);
        Font font = new Font("Calibri", 14, FontStyle.Bold);
        Point point = new Point();
        
        bool isDragging = false;//отвечает за то перетаскивать или нет picturebox3
        int currentX, currentY;
        Vertex[] temp = new Vertex[2];
        List<int> size = new List<int>();
        List<int> position = new List<int>();
        List<Vertex[]> forward = new List<Vertex[]>();
        List<Vertex[]> back = new List<Vertex[]>();
        List<Vertex[]> matrix = new List<Vertex[]>();
        List<Vertex> lvert = new List<Vertex>();
        List<Vertex> comp = new List<Vertex>();

        public void ClearAllLists()
        {
            lvert.Clear();
            matrix.Clear();
            size.Clear();
            position.Clear();
            forward.Clear();
            back.Clear();
            comp.Clear();
        }
        public void ClearDeepList()
        {
            forward.Clear();
            back.Clear();
            comp.Clear();
        }

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
            void Init(bool yes)
            {
                int i, j, m;
                for(i = 0; i < lvert.Count; i++)
                {
                    for(j = 0; j < matrix.Count; j++)
                    {
                        yes = false;
                        for(m = 0; m < lvert.Count; m++)
                        {
                            if(matrix[i][j] == lvert[m])
                            {
                                yes = true;
                                matrix[i][j].name = m;
                                break;
                            }
                            if(yes == false)
                                return;
                            
                        }
                    }
                }
            }
            void Deep(int x, int u, int count)
            {
                int i, v;
                count++;
                for(i = 0; i < lvert.Count; i++)
                {

                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Canvas.Image = bmp;
            button2.Enabled = false;
            button4.Enabled = false;

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
                            matrix.Add(connect);
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

            ReadFile("50.txt");
        }

        public void ReadFile(string name)
        {
            ClearAllLists();
            Vertex v;
            try
            {
                StreamReader sr = new StreamReader(name);
                int count = System.IO.File.ReadAllLines(name).Length;
                string[] masline = new string[count];

                double interval = 360 / Convert.ToDouble(count), pos = -90;
                string line;
                double X2;
                double Y2;

                while ((line = sr.ReadLine()) != null)
                {
                    line = Regex.Replace(line, @"\t", " ");
                    masline = line.Split(' ');
                    X2 = CENTER_X + (RADIUS * 1.2 * Math.Cos(pos * Math.PI / 180.0));
                    Y2 = CENTER_Y + (RADIUS * Math.Sin(pos * Math.PI / 180.0));
                    v = new Vertex(Convert.ToInt32(X2), Convert.ToInt32(Y2), Convert.ToInt32(masline[0]));
                    lvert.Add(v);
                    pos = pos + interval;


                }
                sr.Close();
                sr = new StreamReader(name);
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    line = Regex.Replace(line, @"\t", " ");
                    masline = line.Split(' ');
                    size.Add(Convert.ToInt32(masline[1]));
                    if (position.Count == 0)
                        position.Add(0);
                    else position.Add(position[position.Count - 1] + size[size.Count - 2]);
                    for (int j = 2; j < masline.Length; j++)
                    {
                        Vertex[] mat = new Vertex[2];
                        mat[0] = lvert[i];
                        mat[1] = lvert[Convert.ToInt32(masline[j]) - 1];
                        matrix.Add(mat);
                    }
                    i++;

                }
                position.Add(position[position.Count - 1] + size[size.Count - 2]);
                Canvas.Refresh();
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
            }
        }
        public void WriteForwComp(StreamWriter fw)
        {
            fw.Write("comp = [ ");
            for (int a = 0; a < comp.Count; a++)
                fw.Write(comp[a].name + " ");
            fw.Write("]\n");
            fw.Write("forward = [");
            for (int a = 0; a < forward.Count; a++)
            {
                fw.Write("[ ");
                for (int b = 0; b < 2; b++)
                {
                    fw.Write(forward[a][b].name + " ");
                }
                fw.Write("]");
            }
            fw.Write("]\n");
        }
        StreamWriter fw;
        public void WriteBack(StreamWriter fw)
        {
            fw.Write("back = [");
            for (int a = 0; a < back.Count; a++)
            {
                fw.Write("[ ");
                for (int b = 0; b < 2; b++)
                {
                    fw.Write(back[a][b].name + " ");
                }
                fw.Write("]");
            }
            fw.Write("]\n");
        }
        public void WriteAll(StreamWriter fw)
        {
            fw.WriteLine("Выходные данные:");
            WriteForwComp(fw);
            WriteBack(fw);
        }
        public void WriteIntro(StreamWriter fw)
        {
            fw.Write("Исходные данные:\n" +
                "Количество вершин: " + lvert.Count() + "\n" +
                "comp - список посвященных вершин\n" +
                "forward - список прямых рёбер\n" +
                "back - список обратных рёбер\n" +
                "matrix - матрица смежности или же список рёбер\n" +
                "position - список начальных позиций всех вершин в списке matrix\n" +
                "size - список степенй вершин\n");
            fw.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            void Depth(int p, int sz, int pos, int backcount)
            {
                bool find = false;
                fw.Write("Ищем среди вершин: [");
                for (int j = pos; j < pos + sz; j++)
                {
                    fw.Write("[ " + matrix[j][0].name + " " + matrix[j][1].name + " ]");
                }
                fw.Write("] новые вершины, не входящие в comp\n");
                for (int j = pos; j < pos + sz; j++)
                {
                    if (comp.Find(x => x == matrix[j][1]) == null)
                    {
                        comp.Add(matrix[j][1]);
                        forward.Add(matrix[j]);
                        find = true;
                        p = comp[comp.Count - 1].name - 1;
                        sz = size[p];
                        pos = position[p];
                        fw.WriteLine("Нашли в какую вершину идти.\n" +
                                "Добавляем её в comp и ребро в forward");
                        WriteForwComp(fw);
                        Depth(p, sz, pos, 1);
                        break;
                    }


                }
                if (find == false && comp.Count != lvert.Count)
                {
                    if (backcount > forward.Count || backcount+1 > comp.Count)
                        return;
                    if (back.Find(x => x == forward[forward.Count - backcount]) == null)
                    {
                        fw.WriteLine("Из этой вершины идти некуда,\n" +
                                "Добавляем в back ребро, из которого пришли");
                        back.Add(forward[forward.Count - backcount]);
                    }
                    backcount++;
                    p = comp[comp.Count - backcount].name - 1;
                    sz = size[p];
                    pos = position[p];
                    Depth(p, sz, pos, backcount);

                }

            }
            ClearDeepList();
            comp.Add(lvert[0]);
            fw = new StreamWriter("logfile.txt");
            WriteIntro(fw);
            fw = new StreamWriter("logfile.txt", true);
            fw.WriteLine("Добавляем в comp первую вершину из списка lvert. comp = [" + comp[0].name + "]");
            fw.WriteLine("Начинаем поиск в первой вершины");
            Depth(0, size[0], position[0], 1);
            WriteAll(fw);
            fw.Close();
            Canvas.Refresh();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox1.Text.ToString()) < 1)
                    throw new Exception();   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неверное значение размера матрицы. Введите значение корректно.");
                return;
            }
            button2.Enabled = false;
            DataSet ds = new DataSet();
            ds.Clear();
            ds.Tables.Add("matrix");
            dataGridView1.DataSource = ds.Tables[0];
            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                ds.Tables[0].Columns.Add((i+1).ToString());
                ds.Tables[0].Rows.Add();
                dataGridView1.Columns[i].Width = 30;
                
            }
            if(dataGridView1.Columns.Count*30 < dataGridView1.Width-40)
                for(int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].Width = (dataGridView1.Width-40)/ dataGridView1.Columns.Count;
            if (dataGridView1.Columns.Count * 30 < dataGridView1.Height - 20)
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Rows[i].Height = (dataGridView1.Height-40) / dataGridView1.RowCount;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i+1).ToString();
                for(int j = 0; j < dataGridView1.Columns.Count; j++)
                    dataGridView1.Rows[i].Cells[j].Value = 0;
            }
            button4.Enabled = true;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == '\b'))
                e.Handled = true;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.CurrentCell.Value.ToString().Length > 1)
            {
                MessageBox.Show("Значения могут быть только 0 и 1", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dataGridView1.CurrentCell.Value = null;
            }
            else
            {
                dataGridView1.Rows[dataGridView1.CurrentCell.ColumnIndex].Cells[dataGridView1.CurrentCell.RowIndex].Value = dataGridView1.CurrentCell.Value;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_Cellalidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dataGridView1.EditingControl.KeyPress -= EditingControl_KeyPress;
            dataGridView1.EditingControl.KeyPress += EditingControl_KeyPress;
        }
        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar))
            {
                Control editingControl = (Control)sender;
                if (!(e.KeyChar == '0' || e.KeyChar == '1' || e.KeyChar == '\b'))
                    e.Handled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllLists();
                double X2 = CENTER_X + RADIUS;
                double Y2 = CENTER_Y;
                int value;
                int count = dataGridView1.Rows.Count;
                double interval = 360 / Convert.ToDouble(count), pos = -90;
                for (int i = 0; i < count; i++)
                {

                    X2 = CENTER_X + (RADIUS * Math.Cos(pos * Math.PI / 180.0));
                    Y2 = CENTER_Y + (RADIUS * Math.Sin(pos * Math.PI / 180.0));
                    Vertex v = new Vertex(Convert.ToInt32(X2), Convert.ToInt32(Y2), i + 1);
                    lvert.Add(v);
                    pos = interval + pos;
                }
                for (int i = 0; i < count; i++)
                {
                    for (int j = 0; j < count; j++)
                    {
                        if ((value = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value)) == 1)
                        {
                            Vertex[] matr = new Vertex[2];
                            matr[0] = lvert[i];
                            matr[1] = lvert[j];
                            matrix.Add(matr);
                        }
                    }
                }
                int kol = 0;
                position.Add(0);
                for (int i = 0; i < count; i++)
                {
                    for (int j = position[i]; j < matrix.Count; j++)
                    {
                        if (matrix[j][0].name == i + 1)
                        {
                            kol++;
                        }
                    }
                    size.Add(kol);
                    position.Add(position[i] + kol);
                    kol = 0;
                }
                Canvas.Refresh();
                button2.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ячейки матрицы заполнены неверно. Ошибка!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReadFile("Book.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReadFile("graph1.txt");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ReadFile("graph2.txt");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ReadFile("graph3.txt");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ReadFile("graph4.txt");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ReadFile("graph5.txt");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ReadFile("graph6.txt");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ReadFile("graph7.txt");
        }

        private void Canvas_Click(object sender, EventArgs e)
        {

        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {

            for(int i = 0; i < matrix.Count; i++)
            {
                e.Graphics.DrawLine(pen, matrix[i][0].GetX(), matrix[i][0].GetY(), matrix[i][1].GetX(), matrix[i][1].GetY());
            }
            for(int i = 0; i < forward.Count; i++)
            {
                
                e.Graphics.DrawLine(pen2, forward[i][0].GetX(), forward[i][0].GetY(), forward[i][1].GetX(), forward[i][1].GetY());
                //e.Graphics.DrawString((i+1).ToString(), font, brush, (forward[i][0].GetX() + forward[i][1].GetX())/2, (forward[i][0].GetY() + forward[i][1].GetY())/2);
            }
            for (int i = 0; i < lvert.Count; i++)
            {
                e.Graphics.DrawImage(lvert[i].circle, lvert[i].x - 15, lvert[i].y - 15);
                e.Graphics.DrawString(lvert[i].name.ToString(), font, brush, lvert[i].GetX()-13, lvert[i].GetY()-10);
            }
                
        }
    }
}
