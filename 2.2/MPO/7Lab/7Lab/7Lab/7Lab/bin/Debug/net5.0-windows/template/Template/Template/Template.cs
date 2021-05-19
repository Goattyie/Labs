using System;

namespace Template
{
    class Template
    {
        public void Start()
        {
			Console.WriteLine("Введите P1:");
			float P1 = InputFloat();
			Console.WriteLine("Введите P2:");
			float P2 = InputFloat();
			Console.WriteLine("Введите L:");
			float L = InputFloat();
			float S3 = 0;
			S3=(float)0.5*(P1+P2)/L;
			Console.WriteLine("S3:"+ S3);

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
