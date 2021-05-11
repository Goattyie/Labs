using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7Lab
{
    class Formuls
    {
        public List<string[]> InputData { get; set; } = new List<string[]>();
        public List<string[]> OutputData { get; set; } = new List<string[]>();
        public string Formule { get; set; }
        public Formuls(string[] input, string[] output)
        {
            foreach (string item in input)
                InputData.Add(Convert(item, 1));
            foreach (string item in output)
                OutputData.Add(Convert(item, 2));

            CreateFormule();
        }
        private string[] Convert(string item, int status)
        {
            if (item.Contains("H") && status == 1)
                return new string[] { "\t\t\tfloat H = InputFloat();", "H" };
            if (item.Contains("Sон") && status == 1)
                return new string[] { "\t\t\tfloat S1 = InputFloat();", "S1" };
            if (item.Contains("Sов") && status == 1)
                return new string[] { "\t\t\tfloat S2 = InputFloat();", "S2" };
            if (item.Contains("V") && status == 1)
                return new string[] { "\t\t\tfloat V = InputFloat();", "V" };

            if (item.Contains("H"))
                return new string[] { "\t\t\tfloat H = 0;", "H" };
            if (item.Contains("Sон"))
                return new string[] { "\t\t\tfloat S1 = 0;", "S1" };
            if (item.Contains("Sов"))
                return new string[] { "\t\t\tfloat S2 = 0;", "S2" };
            if (item.Contains("V"))
                return new string[] { "\t\t\tfloat V = 0;", "V" };

            return null;
        }
        private void CreateFormule()
        {

            if (InputData[0].Contains("H") && InputData[1].Contains("S1") && InputData[2].Contains("S2") && OutputData[0].Contains("V"))
                Formule = "(float)1/3*H*(S1+S2+(float)Math.Sqrt(S1*S2));";
            else if (InputData[0].Contains("S1") && InputData[1].Contains("S2") && InputData[2].Contains("V") && OutputData[0].Contains("H"))
                Formule = "(float)3*V/(S1+S2+(float)Math.Sqrt(S1*S2));";
            else if (InputData[0].Contains("H") && InputData[1].Contains("S2") && InputData[2].Contains("V") && OutputData[0].Contains("S1"))
                Formule = "(float)(-S2 + (float)Math.Sqrt(S2*S2 + 36*V*V/H - 4*S2*S2))/2;";
            else if (InputData[0].Contains("H") && InputData[1].Contains("S1") && InputData[2].Contains("V") && OutputData[0].Contains("S2"))
                Formule = "(float)(-S1 + (float)Math.Sqrt(S1*S1 + 36*V*V/H - 4*S1*S1))/2;";
        }
    }
}
