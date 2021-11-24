using System;
using System.Threading;

namespace Lab6
{
    interface IFileWorker
    {
        void Execute();
    }

    class FileReader : IFileWorker
    {
        public void Execute()
        {
            Console.WriteLine("Чтение данных из потока: " + Thread.CurrentThread.Name);
            Console.WriteLine(System.IO.File.ReadAllText(Program.filename));
        }
    }
    class FileWriter : IFileWorker
    {
        public void Execute()
        {
            System.IO.File.AppendAllText(Program.filename, "Данные записаны из потока: " + Thread.CurrentThread.Name + "\n");
        }
    }
    class Program
    {
        public static string filename = "lab6.txt";
        static void Main(string[] args)
        {
            System.IO.File.WriteAllText(filename, null);
            for (int i = 0; i <= 10; i++)
            {
                IFileWorker worker;
                if (i % 2 == 0)
                    worker = new FileWriter();
                else worker = new FileReader();

                var reader = new FileWorker(worker, i);
            }
            FileWorker.sem.Release(1);
        }
    }

    class FileWorker
    {
        // создаем семафор
        public static Semaphore sem = new Semaphore(0, 4);
        Thread myThread;

        public FileWorker(IFileWorker worker, int thread)
        {
            myThread = new Thread(Run);
            myThread.Name = thread.ToString();
            //Запускаем новый поток с методом Run
            myThread.Start(worker);
        }

        public void Run(object worker)
        {
            //Ждем пока освободится семафор
            sem.WaitOne();

            ((IFileWorker)worker).Execute();
            Thread.Sleep(1000);

            //Освобождаем семафор
            sem.Release();
            Thread.Sleep(1000);
        }
    }
}
