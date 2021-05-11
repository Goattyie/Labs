using System;

namespace Template
{
    class Template
    {
        public void Start()
        {
			Console.WriteLine("Введите H:");
			float H = InputFloat();
			Console.WriteLine("Введите S1:");
			float S1 = InputFloat();
			Console.WriteLine("Введите S2:");
			float S2 = InputFloat();
			float V = 0;
			V=(float)1/3*H*(S1+S2+(float)Math.Sqrt(S1*S2));
			Console.WriteLine("V:"+ V);

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
