using System;

namespace Template
{
    class Template
    {
        public void Start()
        {

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
                catch
                {
                    Console.WriteLine("Значение введено неверно. Введите корректное значение.");
                }
            }
        }
        private int InputInt()
        {
            while (true)
            {
                try
                {
                    int a = Convert.ToInt32(Console.ReadLine());
                    if (a <= 0)
                        throw new Exception();
                    return a;
                }
                catch
                {
                    Console.WriteLine("Значение введено неверно. Введите корректное значение.");
                }
            }
        }
    }
}
