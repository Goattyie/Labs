using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string val = "Hello world";
            char[] chars = val.ToCharArray();
            chars[4] = 'G';
            val = new string(chars);
            Console.Write(val);

        }
    }
}
