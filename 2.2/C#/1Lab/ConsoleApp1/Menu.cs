using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Menu
    {
        string text_menu = 
            "1. Добавить значение к матрице (число)\n" +
            "2. Добавить значение к матрице (матрица)\n" +
            "3. Вычитание от матрицы(числа)\n" +
            "4. Вычитание от матрицы(матрицы)\n" +
            "5. Умножение на матрицу(числа)\n" +
            "6. Умножение на матрицу(матрицы)\n" +
            "7. Сравнить матрицы\n" +
            "8. Перевести в обратную\n" +
            "9. Перевести в транспонированную\n" +
            "0. Вычислить детерминанту\n" +
            "q. Вычислить норму\n" +
            "w. Указать тип матрицы\n" +
            "Esc. Выход\n";
        public Menu() {    CreateMatrix();     }
        private void CreateMatrix() 
        {
            char key = '1';
            while (key != 27)
            {
                Console.WriteLine(
                    "1. Создать матрицу по умолчанию\n" +
                    "2. Создать матрицу вручную\n" +
                    "Esc - Выход");
                key = Console.ReadKey(true).KeyChar; ;
                switch (key)
                {
                    case '1':
                        ZagMatrix mat1 = new ZagMatrix(1);
                        View(mat1);
                        break;
                    case '2':
                        ZagMatrix mat2 = new ZagMatrix(2);
                        View(mat2);
                        break;
                }
            }
        }
        private int[] GiveSize() 
        {
            int[] size = new int[2];
            while (true)
            {
                try
                {
                    Console.WriteLine("Укажите размер матрицы:");
                    Console.Write("Количество столбцов:");
                    size[0] = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Количество строк:");
                    size[1] = Convert.ToInt32(Console.ReadLine());
                    if (size[0] < 1 || size[1] < 1)
                        throw new Exception("<1");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Значение введено неверно. Введите корректное значение.");
                }
            }
            return size;
        }
        private float InputFloat()
        {
            while (true)
            {
                try
                {
                    float a = float.Parse(Console.ReadLine());
                    return a;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Значение введено неверно. Введите корректное значение.");
                }
            }
        }
        private float[][] InputMatrix(int[] size)
        {
            float[][] matrix = new float[size[0]][];
            for (int i = 0; i < size[0]; i++)
            {
                matrix[i] = new float[size[1]];
            }
            for (int i = 0; i < size[0]; i++)
            {
                for (int j = 0; j < size[1]; j++)
                {
                    Console.Write("Введите значение matrix[" + i + "][" + j + "]: ");
                    matrix[i][j] = InputFloat();
                }
            }
            return matrix;
        }
        public void View(ZagMatrix matrix) 
        {
            char key = '0';
            int[] size = GiveSize();
            float[][] mat = InputMatrix(size);
            Matr mat1 = new Matr(size, mat);
            while (key != 27) 
            {
                Console.WriteLine(mat1.ToString());
                Console.WriteLine(text_menu);
                key = Console.ReadKey(true).KeyChar;
                switch (key)
                {
                    case '1':
                        Console.Write("Укажите значение: ");
                        matrix.Plus(InputFloat(), ref mat1);
                        break;
                    case '2':
                        int[] size2 = GiveSize();
                        float[][] matr2 = InputMatrix(size2);
                        Matr mat2 = new Matr(size2, matr2);
                        matrix.Plus2(ref mat1, ref mat2);
                        break;
                    case '3':
                        Console.Write("Укажите значение: ");
                        matrix.Diff(InputFloat(), ref mat1);
                        break;
                    case '4':
                        int[] size3 = GiveSize();
                        float[][] mat13 = InputMatrix(size3);
                        Matr mat3 = new Matr(size3, mat13);
                        matrix.Diff2(ref mat1, ref mat3);
                        break;
                    case '5':
                        Console.Write("Укажите значение: ");
                        matrix.Mult(InputFloat(), ref mat1);
                        break;
                    case '6':
                        int[] size4 = GiveSize();
                        float[][] mat14 = InputMatrix(size4);
                        Matr mat4 = new Matr(size4, mat14);
                        matrix.Mult2(ref mat1, ref mat4);
                        break;
                    case '7':
                        int[] size5 = GiveSize();
                        float[][] mat15 = InputMatrix(size5);
                        Matr mat5 = new Matr(size5, mat15);
                        matrix.Comprasion(ref mat1, ref mat5);
                        break;
                    case '8':
                        matrix.Reverse(ref mat1);
                        break;
                    case '9':
                        matrix.Trans(ref mat1);
                        break;
                    case '0':
                        matrix.Determ(mat1);
                        break;
                    case 'q':
                        matrix.Norm(mat1);
                        break;
                    case 'w':
                        matrix.Type(mat1);
                        break;
                }
            }
            
        }
    }
}
