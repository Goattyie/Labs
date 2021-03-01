using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    interface Interface
    {
        
        void Plus(float type, ref Matr mat);//сложение
        void Plus2(ref Matr mat1, ref Matr mat2);//сложение
        void Diff(float type, ref Matr mat);//Вычитание
        void Diff2(ref Matr mat1, ref Matr mat2);
        void Mult(float type, ref Matr mat);//Умножение
        void Mult2(ref Matr mat1, ref Matr mat2);
        void Comprasion(ref Matr mat1, ref Matr mat2);//Сравнение
        void Reverse(ref Matr mat1);//Перевод в обратную
        float[][] Trans(ref Matr mat1);//Перевод в транспонированную 
        float Determ(Matr mat1);//Детерминанта матрицы
        float Norm(Matr mat1);//Норма матрицы
        void Type(Matr mat1);//Тип матрицы

    }
}
