using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Matrix:Interface
    {
        public Matrix() 
        {}
        static Matrix() { Console.WriteLine("Статический конструктор вызван\n"); }
        
        public Matrix(string text)
        {
            Console.WriteLine(text);
        }
     
        private int[] GiveSize()
        {
            int[] size = new int[2];
            Console.WriteLine("Укажите размер матрицы:");
            Console.Write("Количество столбцов:");
            size[0] = Convert.ToInt32(Console.ReadLine());
            Console.Write("Количество строк:");
            size[1] = Convert.ToInt32(Console.ReadLine());
            return size;
        }
        public virtual void Plus(float v, ref Matr mat) 
        {
                for(int i = 0; i < mat.height; i++)
                {
                    for(int j = 0; j < mat.width; j++)
                    {
                        mat.matrix[i][j] += v;
                    }
                }
        }
        public virtual void Plus2(ref Matr mat1,ref Matr mat2)
        {
            if (mat1.height == mat2.height && mat1.width == mat2.width)
            {
                for (int i = 0; i < mat1.height; i++)
                {
                    for (int j = 0; j < mat1.width; j++)
                    {
                        mat1.matrix[i][j] += mat2.matrix[i][j];
                    }
                }
            }
        }//сложение
        public virtual void Diff(float v,ref  Matr mat)
        {
            for (int i = 0; i < mat.height; i++)
            {
                for (int j = 0; j < mat.width; j++)
                {
                    mat.matrix[i][j] -= v;
                }
            }
        }
        public virtual void Diff2(ref Matr mat1,ref  Matr mat2)
        {
            if (mat1.height == mat2.height && mat1.width == mat2.width)
            {
                for (int i = 0; i < mat1.height; i++)
                {
                    for (int j = 0; j < mat1.width; j++)
                    {
                        mat1.matrix[i][j] -= mat2.matrix[i][j];
                    }
                }
            }
        }//сложение
        public virtual void Mult(float v,ref  Matr mat)
        {
            for (int i = 0; i < mat.height; i++)
            {
                for (int j = 0; j < mat.width; j++)
                {
                    mat.matrix[i][j] = v;
                }
            }
        }
        public virtual void Mult2(ref Matr mat1, ref Matr mat2)
        {

            if (mat1.width == mat2.height)
            {
                float[][] matrix3 = new float[mat1.height][];
                for (int i = 0; i < mat1.height; i++)
                {
                    matrix3[i] = new float[mat2.width];
                }
                int height = mat1.height;
                int width = mat1.width;
                int width2 = mat2.width;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width2; j++)
                    {
                        for (int k = 0; k < width; k++)
                        {
                            matrix3[i][j] += mat1.matrix[i][k] * mat2.matrix[k][j];
                        }
                    }
                }
                mat1.matrix = matrix3;
                mat1.width = width2;
            }
        }
        public virtual void Comprasion(ref Matr mat1, ref  Matr mat2) 
        {
           
            if(mat1.height == mat2.height && mat1.width == mat2.width)
            {
                for(int i = 0; i < mat1.height; i++)
                {
                    for(int j = 0; j < mat2.width; j++)
                    {
                        if(mat1.matrix[i][j] != mat2.matrix[i][j])
                        {
                            Console.WriteLine("\nМатрицы не равны.");
                            return;
                        }
                    }
                }
                Console.WriteLine("\nМатрицы равны");
                return;
            }
            Console.WriteLine("\nМатрицы не равны.");

        }//Сравнение
        public virtual void Reverse(ref Matr mat) 
        {
            float det = Determ(mat);
            if (mat.height != mat.width)
            {
                Console.WriteLine("Матрица не квадратная");
                return;
            }else if(det == 0)
            {
                Console.WriteLine("Определитель = 0.");
                return;
            }
            Trans(ref mat);
            for(int i = 0; i < mat.height; i++)
            {
                for(int j = 0; j < mat.width; j++)
                {
                    mat.matrix[i][j] /= det;
                }
            }

               
        }//Перевод в обратную
        public virtual float[][] Trans(ref Matr mat) 
        {
            float[][] matrix2 = new float[mat.width][];
            for(int i = 0; i < mat.width; i++)
            {
                matrix2[i] = new float[mat.height];
            }
            
            for (int i = 0; i < mat.height; i++)
            {
                for(int j = 0; j < mat.width; j++)
                {
                    matrix2[j][i] = mat.matrix[i][j];
                }
            }
            mat.matrix = matrix2;
            int temp = mat.width;
            mat.width = mat.height;
            mat.height = temp;
            return mat.matrix;
        }//Перевод в транспонированную 
        public virtual float Determ(Matr mat) 
        {
            if (mat.height != mat.width)
            {
                Console.WriteLine("Определитель может быть только в квадратной матрице");
                return 0;
            }
            float det = 1;
            int EPS = 0;
            float[][] a = new float[mat.width][];
            for(int i = 0; i < mat.width; i++)
            {
                a[i] = new float[mat.width];
                for (int j = 0; j < mat.width; j++)
                    a[i][j] = mat.matrix[i][j];
            }
            float[][] b = new float[1][];
            b[0] = new float[mat.width];
            for (int i = 0; i < mat.width; ++i)
            {
                //присваиваем k номер строки
                int k = i;
                //идем по строке от i+1 до конца
                for (int j = i + 1; j < mat.width; ++j)
                    //проверяем
                    if (Math.Abs(a[j][i]) > Math.Abs(a[k][i]))
                        //если равенство выполняется то k присваиваем j
                        k = j;
                //если равенство выполняется то определитель приравниваем 0 и выходим из программы
                if (Math.Abs(a[k][i]) < EPS)
                {
                    det = 0;
                    break;
                }
                //меняем местами a[i] и a[k]
                b[0] = a[i];
                a[i] = a[k];
                a[k] = b[0];
                //если i не равно k
                if (i != k)
                    //то меняем знак определителя
                    det = -det;
                //умножаем det на элемент a[i][i]
                det *= a[i][i];
                //идем по строке от i+1 до конца
                for (int j = i + 1; j < mat.width; ++j)
                    //каждый элемент делим на a[i][i]
                    a[i][j] /= a[i][i];
                //идем по столбцам
                for (int j = 0; j < mat.width; ++j)
                    //проверяем
                    if ((j != i) && (Math.Abs(a[j][i]) > EPS))
                        //если да, то идем по k от i+1
                        for (k = i + 1; k < mat.width; ++k)
                            a[j][k] -= a[i][k] * a[j][i];
            }
            Console.WriteLine("Определитель: " + det.ToString());
            return det;
        }//Детерминанта матрицы
        public virtual float Norm(Matr mat) 
        {
            
                float max = float.MinValue;
                float temp;
                for (int i = 0; i < mat.height; i++)
                {
                    temp = 0;
                    for (int j = 0; j < mat.width; j++)
                    {
                        temp = temp + mat.matrix[i][j];
                    }
                    if (temp > max) max = temp;
                }

            Console.WriteLine("Норма: " + max);
            return max;
            
        }//Норма матрицы
        public virtual void Type(Matr mat) 
        {
            if (mat.height == mat.width)
                Console.WriteLine("Матрица квадратная");
            bool diag = true, ed = true, nul = true;
            for(int i = 0; i < mat.height; i++)
            {
                for (int j = 0; j < mat.height; j++)
                {
                    if (i != j && diag == true && mat.matrix[i][j] != 0)
                    {
                        diag = false;
                        ed = false;
                    }
                    if (i == j && mat.matrix[i][j] != 1)
                        ed = false;
                    if (mat.matrix[i][j] != 0)
                        nul = false;
                }
            }
            if(diag == true)
                Console.WriteLine("Матрица диагональная");
            if(ed == true)
                Console.WriteLine("Матрица единичная");
            if(nul == true)
                Console.WriteLine("Матрица нулевая");
        }//Тип матрицы

    }
}
