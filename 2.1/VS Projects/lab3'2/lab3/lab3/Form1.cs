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
            public int value, x, y, deep;
            public string value2;
            public Trees(int value, string value2)
            {
                this.value = value;
                this.value2 = value2;


            }

            
            public void Add(int value, string value2, Graphics graph)
            {
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                

                if (value.CompareTo(this.value) < 0)
                {
                    if (this.left == null)
                    {

                        this.left = new Trees(value, value2);
                        this.left.deep = this.deep+1;
                        this.left.x = this.x - 130 + this.left.deep * 22;
                        this.left.y = this.y + 60;
                        graph.DrawString(value.ToString(), font, brush, this.left.x, this.left.y);
                        graph.DrawLine(pen, this.x, this.y, this.left.x, this.left.y);
                    }
                    else if (this.left != null)
                    {
                        this.left.Add(value, value2, graph);
                    }
                }
                else if (value.CompareTo(this.value) > 0)
                {
                    if (this.right == null)
                    {
                        
                        this.right = new Trees(value, value2);
                        this.right.deep = this.deep+1;
                        this.right.x = this.x + 130 - this.right.deep * 22;
                        this.right.y = this.y + 60;
                        graph.DrawString(value.ToString(), font, brush, this.right.x, this.right.y);
                        graph.DrawLine(pen, this.x, this.y, this.right.x, this.right.y);
                    }
                    else if (this.right != null)
                    {
                        this.right.Add(value, value2, graph);
                    }
                }
                else
                {
                    this.x = 260;
                    this.y = 10;
                    this.value = value;
                    this.value2 = value2;
                    this.deep = 0;
                    graph.DrawString(value.ToString(), font, brush, this.x, this.y);
                }
            }
            public int Deep(int value, int deep)
            {
                if (value < this.value)
                {
                    deep++;
                    deep = this.left.Deep(value, deep);

                }
                else if (value > this.value)
                {
                    deep++;
                    deep = this.right.Deep(value, deep);

                }
                return deep;


            }
            public Trees delete(Trees node, int data)
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

                    }
                }
                return node;
            }
            public void View(Trees node, Graphics graph)
            {
                Brush brush = Brushes.Black;
                Pen pen = new Pen(Color.Black);
                Font font = new Font("Calibri", 10, FontStyle.Bold);
                if(node != null)
                    graph.DrawString(value.ToString(), font, brush, this.x, this.y);
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
            }
        }
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
            
            bool find = false, key = true;
            string value2 = null;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (textBox1.Text == dataGridView1[0, i].Value.ToString())
                {
                    find = true;
                    value2 = dataGridView1[1, i].Value.ToString();
                }
                if (dataGridView1[0, i].Value.ToString() == "")
                    key = false;
            }
            if (key == false)
            {
                MessageBox.Show("Не все ячейки заполнены верно. Заполните еще раз!!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (find == true)
            {
                tree = new Trees(Convert.ToInt32(textBox1.Text), value2);
                tree.x = 260;
                tree.y = 10;
                root = new Trees(Convert.ToInt32(textBox1.Text), value2);
                button2.Enabled = true;
                button4.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ненайден корень в списке. Введите другой корень!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Text = null;
            }
        }


    private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            ds.Tables.Add("Name");
            ds.Tables[0].Columns.Add("Id");
            ds.Tables[0].Columns.Add("Name");
            dataGridView1.DataSource = ds.Tables[0];
            button2.Enabled = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            StreamReader sr = new StreamReader("File1.txt");
            string read = sr.ReadLine();


            while (read != null)
            {

                string[] rvalue = System.Text.RegularExpressions.Regex.Split(read, " ");
                ds.Tables[0].Rows.Add(rvalue);
                string v = sr.ReadLine();
                read = v;

            }
            button3.Enabled = false;
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics graph = Graphics.FromImage(bmp);
            pictureBox1.InitialImage = null;
            graph.Clear(Color.White);
            
            
            pictureBox1.Image = bmp;
            bool find = false;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (textBox2.Text == dataGridView1[0, i].Value.ToString())
                    find = true;

            }
            if (find == false)
            {
                MessageBox.Show("Не найдено значение в списке. Введите другое значение!", "Ошибка!",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox2.Text = null;
                return;
            }
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                tree.Add(Convert.ToInt32(dataGridView1[0, i].Value), dataGridView1[1, i].Value.ToString(), graph);
            } // Создание дерева
            tree.Deep(Convert.ToInt32(textBox2.Text.ToString()), 0);
            label3.Text = tree.Deep(Convert.ToInt32(textBox2.Text.ToString()), 0).ToString();
            button2.Enabled = false;
            button4.Enabled = true;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                if (!int.TryParse(dataGridView1.CurrentCell.Value.ToString(), out int check))
                {
                    MessageBox.Show("Неправильный тип значения. Введите число!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dataGridView1.CurrentCell.Value = null;
                }
            }

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
            int a = Convert.ToInt32(textBox3.Text);
            tree.delete(tree, a);
            tree.View(tree, graph);

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
            button4.Enabled = false;
        }
    }
}
