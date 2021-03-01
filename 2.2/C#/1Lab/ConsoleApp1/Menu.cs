using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Menu
    {
        string text_menu = 
            "1. Вызвать статический конструктор класса\n" +
            "2. Вызвать конструктор класса по умолчанию\n" +
            "3. Вызвать конструктор класса с параметрами\n" +
            "4. Добавить значение к матрице (число)\n" +
            "5. Добавить значение к матрице (матрица)\n" +
            "6. Вычитание от матрицы(числа)\n" +
            "7. Вычитание от матрицы(матрицы)\n" +
            "8. Умножение на матрицу(числа)\n" +
            "9. Умножение на матрицу(матрицы)\n" +
            "0. Сравнить матрицы\n" +
            "q. Перевести в обратную\n" +
            "w. Перевести в транспонированную\n" +
            "e. Вычислить детерминанту\n" +
            "r. Вычислить норму\n" +
            "t. Указать тип матрицы\n" +
            "Esc. Выход\n";
        private Matrix CreateMatrix() 
        {
            char key = '0';
            while (true)
            {
                Console.WriteLine("1. Создать матрицу по умолчанию\n" +
                "2. Создать матрицу вручную\n");
                key = Console.ReadKey(false).KeyChar; ;
                switch (key)
                {
                    case '1':
                        Matrix mat1 = new Matrix();
                        return mat1;
                    case '2':
                        Matrix mat2 = new Matrix(GiveSize());
                        return mat2;
                }
            }
        }
        public int[] GiveSize() 
        {
            int[] size = new int[2];
            Console.WriteLine("Укажите размер матрицы:");
            Console.Write("Количество строк:");
            size[0] = Console.Read();
            size[1] = Console.Read();
            return size;
        }
        public void View() 
        {
            Matrix matrix = CreateMatrix();
            char key = '0';
            while(key != 27) 
            { 
                Console.WriteLine(text_menu);
                key = Console.ReadKey(true).KeyChar; ;
                switch (key)
                {
                    case '1':
                        break;
                    case '2':
                        break;
                    case '3':
                        break;
                    case '4':
                        break;
                    case '5':
                        break;
                    case '6':
                        break;
                    case '7':
                        break;
                    case '8':
                        break;
                    case '9':
                        break;
                    case '0':
                        break;
                    case 'q':
                        break;
                    case 'w':
                        break;
                    case 'e':
                        break;
                    case 'r':
                        break;
                    case 't':
                        break;
                }
            }
            
        }
    }
}
