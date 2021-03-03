using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class ZagMatrix : Matrix
    {
        public ZagMatrix(int i)
        {
            if (i == 1)
                matrix = new Matrix();
            else if (i == 2)
                matrix = new Matrix("Конструктор с параметрами вызван");
        }
        Matrix matrix;
        public override void Plus(float type, ref Matr mat)
        {
            Console.WriteLine("Заглушка Plus");
            matrix.Plus(type, ref mat);
        }//сложение
        public override void Plus2(ref Matr mat1, ref Matr mat2)
        {
            Console.WriteLine("Заглушка Plus2");
            matrix.Plus2(ref mat1, ref mat2);
        }//сложение//сложение
        public override void Diff(float type, ref Matr mat)
        {
            Console.WriteLine("Заглушка Diff");
            matrix.Diff(type, ref mat);
        }//сложение//Вычитание
        public override void Diff2(ref Matr mat1, ref Matr mat2)
        {
            Console.WriteLine("Заглушка Diff2");
            matrix.Diff2(ref mat1, ref mat2);
        }//сложение
        public override void Mult(float type, ref Matr mat)
        {
            Console.WriteLine("Заглушка Mult");
            matrix.Mult(type, ref mat);
        }//сложение//Умножение
        public override void Mult2(ref Matr mat1, ref Matr mat2)
        {
            Console.WriteLine("Заглушка Mult2");
            matrix.Mult2(ref mat1, ref mat2);
        }//сложение
        public override void Comprasion(ref Matr mat1, ref Matr mat2)
        {
            Console.WriteLine("Заглушка Comprasion");
            matrix.Comprasion(ref mat1, ref mat2);
        }//сложение//Сравнение
        public override void Reverse(ref Matr mat1)
        {
            Console.WriteLine("Заглушка Reverse");
            matrix.Reverse(ref mat1);
        }//сложение//Перевод в обратную
        public override float[][] Trans(ref Matr mat1)
        {
            Console.WriteLine("Заглушка Trans");
            return matrix.Trans(ref mat1);
        }//сложение//Перевод в транспонированную 
        public override float Determ(Matr mat1)
        {
            Console.WriteLine("Заглушка Determ");
            return matrix.Determ(mat1);
        }//сложение//Детерминанта матрицы
        public override float Norm(Matr mat1)
        {
            Console.WriteLine("Заглушка Norm");
            return matrix.Norm(mat1);
        }//сложение//Норма матрицы
        public override void Type(Matr mat1)
        {
            Console.WriteLine("Заглушка Type");
            matrix.Type(mat1);
        }//сложение//Тип матрицы
    }
}
