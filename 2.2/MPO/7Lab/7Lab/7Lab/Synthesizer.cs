using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

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
            CurrentLine = 8;
            Program = File.ReadAllLines("Template.cs").ToList();
            Program.Insert(CurrentLine++, $"\t\t\tConsole.WriteLine(\"Введите {Data.InputData[0][1]}:\");");
            Program.Insert(CurrentLine++, Data.InputData[0][0]);
            Program.Insert(CurrentLine++, $"\t\t\tConsole.WriteLine(\"Введите {Data.InputData[1][1]}:\");");
            Program.Insert(CurrentLine++, Data.InputData[1][0]);
            Program.Insert(CurrentLine++, $"\t\t\tConsole.WriteLine(\"Введите {Data.InputData[2][1]}:\");");
            Program.Insert(CurrentLine++, Data.InputData[2][0]);
            Program.Insert(CurrentLine++, Data.OutputData[0][0]);
            Program.Insert(CurrentLine++, "\t\t\t" + Data.OutputData[0][1] + "=" + Data.Formule);
            Program.Insert(CurrentLine++, $"\t\t\tConsole.WriteLine(\"{Data.OutputData[0][1]}:\"+ {Data.OutputData[0][1]});");

            File.WriteAllLines("template\\Template\\Template\\Template.cs", Program);
            //System.Diagnostics.Process.Start("cd template\\Template\\Template && dotnet run");

        }
    }
}
