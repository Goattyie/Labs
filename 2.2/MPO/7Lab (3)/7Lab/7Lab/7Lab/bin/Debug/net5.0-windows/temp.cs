using System;

namespace Template
{
    class Template
    {
        public void Start()
        {
			float H = InputFloat();
			float S1 = InputFloat();
			float S2 = InputFloat();
			float V = 0;
			float V = 0;
			V=1/3*H*(S1+S2+Math.Sqrt(S1*S2);
			Console.WriteLine("V:", V);
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
