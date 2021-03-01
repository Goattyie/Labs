using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Matrix:Interface
    {
        public Matrix() 
        {
            this.matrix = new float[3][];
            this.matrix[0] = new float[3] { 0, 0, 0 };
            this.matrix[1] = new float[3] { 0, 0, 0 };
            this.matrix[2] = new float[3] { 0, 0, 0 };
        }
        static Matrix() { Console.WriteLine("Статический конструктор вызван\n"); }
        public Matrix(int[] size) { this.matrix = matrix; }
        private float[][] matrix;
        public void Plus() { }//сложение
        public void Diff() { }//Вычитание
        public void Mult() { }//Умножение
        public void Comprasion() { }//Сравнение
        public void Reverse() { }//Перевод в обратную
        public void Trans() { }//Перевод в транспонированную 
        public void Determ() { }//Детерминанта матрицы
        public void Norm() { }//Норма матрицы
        public void Type() { }//Тип матрицы

        public override string ToString() { return "ToString"; }

    }
}
