using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace _7Lab
{
    class Synthesizer
    {
        Formuls Data { get; set; }
        private int CurrentLine { get; set; }//Текущая строка
        private List<string> Program { get; set; }//Шаблонный код
        private List<string> Variable { get; set; } //Синтезированный код
        public Synthesizer(string[] inputItems, string[] OutputItems)
        {
            Data = new Formuls(inputItems, OutputItems);
            Synthesize();
        }
        private void Synthesize()
        {
            if(Data.Formule == null)
            {
                MessageBox.Show("Найти выходной данное с помощью исходных невозможно. Используйте другие данные.");
                return;
            }
            CurrentLine = 8;
            Program = File.ReadAllLines("Template.cs").ToList();
            for (int i = 0; i < Data.InputData.Count; i++)
            {
                Program.Insert(CurrentLine++, $"\t\t\tConsole.WriteLine(\"Введите {Data.InputData[i][1]}:\");");
                Program.Insert(CurrentLine++, Data.InputData[i][0]);
            }
            Program.Insert(CurrentLine++, Data.OutputData[0][0]);
            Program.Insert(CurrentLine++, "\t\t\t" + Data.OutputData[0][1] + "=" + Data.Formule);
            Program.Insert(CurrentLine++, $"\t\t\tConsole.WriteLine(\"{Data.OutputData[0][1]}:\"+ {Data.OutputData[0][1]});");

            File.WriteAllLines("template\\Template\\Template\\Template.cs", Program);

            //System.Diagnostics.Process.Start("C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\csc.exe", "D:\\Labs\\2.2\\MPO\\7Lab\\7Lab\\7Lab\\bin\\Debug\\net5.0-windows\\template\\Template\\Template\\Program.cs");
            //Thread.Sleep(600);

            //.Copy("templa", "template\\Template\\Template\\Template.exe", true);

        }
    }
}
