using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4Lab
{
    static class ValueConverter
    {
        public static string DemicalToBinary(string value)
        {
            Console.WriteLine($"Переводим {value} в двоичную сс: ");
            var binary = string.Empty;
            var number = int.Parse(value);
            Console.WriteLine($"{value} в двоичную сс: ");

            while(number > 1)
            {
                //Console.Write($"делим {number} на 2: ");
                binary = binary.Insert(0, (number % 2).ToString());
                number = number / 2;
                    
                //Console.WriteLine($"резульат {number}, остаток {binary[0]}");
            }
            //Console.WriteLine($"результат деления: {number}. Записываем в начало и получим: ");
            binary = binary.Insert(0, number.ToString());
            Console.WriteLine(binary);
            return binary;    
        } 
        public static int BinaryToDemical(string numb)
        {
            Console.WriteLine($"Переводим {numb} в десятичную сс: ");
            int result = 0;
            for(int i = 0; i < numb.Length; i++)
            {
                var res = (int)(int.Parse(numb[i].ToString()) * (int)Math.Pow(2, 7 - i));
                Console.WriteLine($"{numb[i]} * 2^{7-i} = {res}");
                Console.WriteLine($"Прибавляем к результату: {result} + {res} = {result + res}");
                result += res;
            }
            Console.WriteLine(result);
            return result;
        }
    }
}
