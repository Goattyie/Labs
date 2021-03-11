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

namespace TaiFYA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool c = true;



        public class Turing
        {
            private StreamWriter file;
            private static int i;
            public char[] input;
            public string sinput;
            private int count = 0;
            private bool write;
            public Turing(string text, bool write)
            {
                input = new char[text.Length + 2];
                input[0] = ' ';
                for (int v = 0; v < text.Length; v++)
                {
                    input[v + 1] = text[v];
                }
                input[input.Length - 1] = ' ';
                sinput = new string(input);
                this.write = write;
                if(write == true)
                {
                    file = new StreamWriter("progress.txt");
                }
                
            }
            
            private void Write()
            {
                if (write == true)
                {
                    string sinp = new string(input);
                    string fileline = (sinp.Insert(i, "q" + count));
                    file.WriteLine(fileline);
                }
            }
            public void Run()
            {
                Algorithm();
                file.Close();
                
            }
            public void Algorithm()
            { 
                i = 1;
                q1();
            }
            
            private void q1()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q2(); break;
                    case '1': i++; q2(); break;
                    case 'c': i--; q14(); break;
                    case '*': i--; q4(); break;
                    case ' ': input[i] = 'F'; Write();
                        break;

                }
            }
            private void q2()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q1(); break;
                    case '1': i++; q1(); break;
                    case 'c': i--; q3(); break;
                    case '*': i--; q4(); break;
                    case ' ': input[i] = 'F'; Write();
                        break;

                }
            }
            private void q3()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i--; q3(); break;
                    case '1': i--; q3(); break;
                    case '*': i--; q3(); break;
                    case ' ': input[i] = 'F'; Write();
                        break;

                }
            }
            private void q4()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': input[i] = '*'; i++; q5(); break;
                    case '1': input[i] = '*'; i++; q4(); break;
                    case 'c': i++; q6(); break;
                    case '*': i++; q4(); break;
                    case ' ': i++; q14(); break;

                }
            }
            private void q5()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case 'c': i++; q8(); break;
                    case '*': i++; q5(); break;

                }
            }
            private void q6()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q7(); break;
                    case '1': i++; q7(); break;
                    case 'c': i++; q18(); break;
                    case '*': i--; q10(); break;
                    case ' ': i--; q10(); break;

                }
            }
            private void q7()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q6(); break;
                    case '1': i++; q6(); break;
                    case 'c': i++; q18(); break;
                    case '*': i--; q10(); break;
                    case ' ': input[i] = 'F'; Write();
                        break;

                }
            }
            private void q8()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q9(); break;
                    case '1': i++; q9(); break;
                    case 'c': i++; q18(); break;
                    case '*': i--; q13(); break;
                    case ' ': i--; q13(); break;

                }
            }
            private void q9()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q8(); break;
                    case '1': i++; q8(); break;
                    case 'c': i++; q18(); break;
                    case '*': i--; q13(); break;
                    case ' ': input[i] = 'F'; Write();
                        break;

                }
            }
            private void q10()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q11(); break;
                    case '1': input[i] = '*'; i--; q12(); break;
                    case 'c': i++; q11(); break;

                }
            }
            private void q11()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q11(); break;
                    case '1': i++; q11(); break;
                    case 'c': i++; q11(); break;
                    case '*': i++; q11(); break;
                    case ' ': input[i] = 'T'; Write();
                        break;

                }
            }
            private void q12()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i--; q12(); break;
                    case '1': i--; q12(); break;
                    case 'c': i--; q12(); break;
                    case '*': i--; q12(); break;
                    case ' ': i++; q1(); break;

                }
            }
            private void q13()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': input[i] = '*'; i--; q12(); break;
                    case '1': i++; q11(); break;
                    case 'c': i++; q11(); break;

                }
            }
            private void q14()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': q4(); break;
                    case '1': q4(); break;
                    case 'c': i++; q15(); break;
                    case '*': i++; q14(); break;
                    case ' ': i++; q14(); break;

                }
            }
            private void q15()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q17(); break;
                    case '1': i++; q17(); break;
                    case 'c': i++; q18(); break;
                    case '*': i++; q15(); break;
                    case ' ': input[i] = 'F'; Write();
                        break;

                }
            }
            private void q16()
            {
                Write();
                count++;
                switch (input[i])
                {
                    case '0': i++; q17(); break;
                    case '1': i++; q17(); break;
                    case 'c': i++; q18(); break;
                    case '*': i++; q17();break;
                    case ' ': input[i] = 'T'; Write();
                        break;

                }
            }
            private void q17()
            {
                if (write == true)
                {
                    string sinp = new string(input);
                    string fileline = (sinp.Insert(i, "q" + count));
                    file.WriteLine(fileline);
                }
                count++;
                switch (input[i])
                {
                    case '0': i++; q16(); break;
                    case '1': i++; q16(); break;
                    case 'c': i++; q18(); break;
                    case '*': i++; q16(); break;
                    case ' ': input[i] = 'F'; break;


                }
            }
            private void q18()
            {
                if (write == true)
                {
                    string sinp = new string(input);
                    string fileline = (sinp.Insert(i, "q" + count));
                    file.WriteLine(fileline);
                }
                count++;
                switch (input[i])
                {
                    case '0': i++; q18(); break;
                    case '1': i++; q18(); break;
                    case '*': i++; q18(); break;
                    case 'c': i++; q18(); break;
                    case ' ': input[i] = 'F'; break;


                }
            }
            public int GetCount()
            {
                return count;
            }
        }

        public void Print(int maxcount, string maxvalue, int length)
        {
            
            chart1.Series[0].Points.AddXY(length, maxcount);
            chart1.Update();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[0].Points.AddXY(0, 0);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar == '1'|| e.KeyChar == '0' || e.KeyChar == 'c' || e.KeyChar == '\b')
                e.Handled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = null;
            if( textBox1.Text.Length == 0)
            {
                MessageBox.Show("Error. Input again");
                return;
            }
            Turing machine = new Turing(textBox1.Text, true);
            machine.Run();
            for(int i = 0; i < machine.input.Length; i++)
            {
                result += machine.input[i];
            }
            textBox1.Text = null;
            label3.Text = result;
            label2.Text = "Данные записаны в файл";
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int maxcount = 0;
            string maxvalue = null;
            const int n = 3;
            int m = 1;// n = максимальная цифра
            while (true)
            {
                string col = null;
                List<string> all = new List<string>();
                char[] temp;
                temp = new char[m + 1];
                for (int i = 0; i < m + 1; i++)
                {
                    temp[i] = '0';
                }

                while (temp[0] == '0')
                {
                    char[] tmp = new char[m + 1];
                    for (int i = 1; i <= m; i++)
                    {
                        tmp[i] = temp[i];
                    }


                    for (int i = 1; i <= m; i++)
                    {
                        col += temp[i].ToString();
                    }
                    col = col.Replace('2', 'c');
                    Turing graph = new Turing(col, false);
                    graph.Algorithm();
                    if (maxcount < graph.GetCount())
                    {
                        maxcount = graph.GetCount();
                        maxvalue = col;
                    }
                    col = null;
                    temp[m]++;
                    for (int i = m; i >= 0; i--)
                    {
                        if (Convert.ToInt32(temp[i].ToString()) >= n)
                        {
                            temp[i] = '0';
                            temp[i - 1]++;
                        }
                    }
                }
                Print(maxcount, maxvalue, m);
                m++;
            }
        }
    }
}
