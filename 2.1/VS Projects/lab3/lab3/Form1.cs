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


namespace lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        class Trees
        {
            public Trees left, right;
            public int  x, y, deep;
            public double value;
            public Trees(double value)
            {
                this.value = value;   


            }

            
            public void Add(double value, Graphics graph)
            {
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                

                if (value.CompareTo(this.value) < 0)
                {
                    if (this.left == null)
                    {

                        this.left = new Trees(value);
                        this.left.deep = this.deep+1;
                        this.left.x = this.x - 130 + this.left.deep * 22;
                        this.left.y = this.y + 60;
                        
                    }
                    else if (this.left != null)
                    {
                        this.left.Add(value, graph);
                    }
                }
                else if (value.CompareTo(this.value) > 0)
                {
                    if (this.right == null)
                    {
                        
                        this.right = new Trees(value);
                        this.right.deep = this.deep+1;
                        this.right.x = this.x + 130 - this.right.deep * 22;
                        this.right.y = this.y + 60;
                        
                    }
                    else if (this.right != null)
                    {
                        this.right.Add(value, graph);
                    }
                }

            }
            public int Deep(double value, int deep)
            {
                if (value < this.value)
                {
                    if (this.left != null)
                    {
                        deep++;
                        deep = this.left.Deep(value, deep);
                    }
                    else
                    {

                        return deep;
                    }

                }
                else if (value > this.value)
                {
                    if (this.right != null)
                    {
                        deep++;
                        deep = this.right.Deep(value, deep);
                    }
                    else
                    {
                        return deep;
                    }

                }
                return deep;


            }
            public Trees delete(Trees node, double data)
            {
                if (node == null)
                    return null;
                 if (data < node.value)
                {
                    node.left = delete(node.left, data);
                }
                else if (data > node.value)
                {
                    node.right = delete(node.right, data);
                }
                else 
                {
                    if (node.right == null && node.left == null)
                    {
                        node = null;
                    }
                    else if (node.right == null && node.left != null)
                    {

                        node.left = null;
                        node = null;
                    }
                    else if (node.right != null && node.left == null)
                    {
                        node.right = null;
                        node = null;
                    }
                    else if (node.right != null && node.left != null)
                    {
                        node.right = null;
                        node.left = null;
                        node = null;

                    }
                }
                return node;
            }
            public int View(Trees node, Graphics graph)
            {
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                if(node.value != 0)
                    graph.DrawString(value.ToString(), font, brush, this.x, this.y);
                else return 0;
                if (node.left != null)
                {
              
                    graph.DrawLine(pen, this.x, this.y, this.left.x, this.left.y);
                    
                    this.left.View(this.left, graph);
                }
                if(node.right != null)
                {
                    graph.DrawLine(pen, this.x, this.y, this.right.x, this.right.y);
                    
                    this.right.View(this.right, graph);
                }
                return 1;
            }
            
            public void NewCordY(Trees node, int value)
            {
                node.y += value;
                if (node.left != null)
                    NewCordY(node.left, value);
                if (node.right != null)
                    NewCordY(node.right, value);
                return;
            }
            public void NewCordX(Trees node, int value)
            {
                node.x += value;
                if (node.left != null)
                    NewCordX(node.left, value);
                if (node.right != null)
                    NewCordX(node.right, value);
                return;
            }
        }
        List<double> list = new List<double>();
        Trees tree;
        Trees root;
        DataSet ds = new DataSet();


        public void Draw() 
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(538, 335);
            Graphics graph = Graphics.FromImage(bmp);
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Color.Black);
            Font font = new Font("Calibri", 10, FontStyle.Bold);
            pictureBox1.Image = bmp;
            bool key = true;
            if (textBox1.Text == "")
                    key = false;
            if (key == false)
            {
                MessageBox.Show("Значение заполнено неверно. Заполните еще раз!!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if (list.Count() == 0)
                {
                    tree = new Trees(Convert.ToDouble(textBox1.Text));
                    tree.x = 260;
                    tree.y = 10;
                    tree.View(tree, graph);
                    list.Add(tree.value);
                }
                else
                {
                    tree.Add(Convert.ToDouble(textBox1.Text), graph);
                    list.Add(Convert.ToDouble(textBox1.Text));
                    tree.View(tree, graph);
                }
                button2.Enabled = true;
            }catch(Exception ex)
            {
                MessageBox.Show("Ошибка!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool deep = false;
            try
            {
                for(int i = 0; i < list.Count; i++)
                    if(Convert.ToInt32(textBox2.Text.ToString()) == list[i])
                    {
                        deep = true;
                        break;
                    }
                if (deep == true)
                {
                    tree.Deep(Convert.ToInt32(textBox2.Text.ToString()), 0);
                    label3.Text = tree.Deep(Convert.ToInt32(textBox2.Text.ToString()), 0).ToString();
                }
                else
                {
                    MessageBox.Show("Нет такого значения в дереве!", "Ошибка!",
                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!", "Ошибка!",
               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (textBox3.Text.ToString() == "") {
                MessageBox.Show("Не все ячейки заполнены верно. Заполните еще раз!!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graph = Graphics.FromImage(bmp);
            pictureBox1.InitialImage = null;
            graph.Clear(Color.White);
            pictureBox1.Image = bmp;
            try
            {
                double a = Convert.ToDouble(textBox3.Text);
                if (tree.delete(tree, a) == null)
                {
                    tree.value = 0;
                }
                if (tree.View(tree, graph) == 0)
                {
                    pictureBox1.InitialImage = null;
                    graph.Clear(Color.White);
                    pictureBox1.Image = bmp;
                    tree = null;
                    list.Clear();
                }
            }
            catch (Exception ex) {
                MessageBox.Show("Не все ячейки заполнены верно. Заполните еще раз!!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graph = Graphics.FromImage(bmp);
            pictureBox1.InitialImage = null;
            graph.Clear(Color.White);
            pictureBox1.Image = bmp;
            tree = null;
            list.Clear();

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W && tree != null)
            {
                Bitmap bmp = new Bitmap(538, 335);
                Graphics graph = Graphics.FromImage(bmp);
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                pictureBox1.Image = bmp;
                tree.NewCordY(tree, 30);
                tree.View(tree, graph);
            }
            else if (e.KeyCode == Keys.S && tree != null)
            {
                Bitmap bmp = new Bitmap(538, 335);
                Graphics graph = Graphics.FromImage(bmp);
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                pictureBox1.Image = bmp;
                tree.NewCordY(tree, -30);
                tree.View(tree, graph);
            }else if(e.KeyCode == Keys.D && tree != null)
            {
                Bitmap bmp = new Bitmap(538, 335);
                Graphics graph = Graphics.FromImage(bmp);
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                pictureBox1.Image = bmp;
                tree.NewCordX(tree, 30);
                tree.View(tree, graph);
            }else if(e.KeyCode == Keys.A && tree != null)
            {
                Bitmap bmp = new Bitmap(538, 335);
                Graphics graph = Graphics.FromImage(bmp);
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                pictureBox1.Image = bmp;
                tree.NewCordX(tree, -30);
                tree.View(tree, graph);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                label1.Text = "Hello world";
               
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                label1.Text = "Hello world";

            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
