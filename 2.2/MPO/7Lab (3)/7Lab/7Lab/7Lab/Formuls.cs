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
            if (item.Contains("S1") && status == 1)
                return new string[] { "\t\t\tfloat S1 = InputFloat();", "S1" };
            if (item.Contains("S2") && status == 1)
                return new string[] { "\t\t\tfloat S2 = InputFloat();", "S2" };
            if (item.Contains("V") && status == 1)
                return new string[] { "\t\t\tfloat V = InputFloat();", "V" };
            if (item.Contains("P1") && status == 1)
                return new string[] { "\t\t\tfloat P1 = InputFloat();", "P1" };
            if (item.Contains("P2") && status == 1)
                return new string[] { "\t\t\tfloat P2 = InputFloat();", "P2" };
            if (item.Contains("L") && status == 1)
                return new string[] { "\t\t\tfloat L = InputFloat();", "L" };
            if (item.Contains("S3") && status == 1)
                return new string[] { "\t\t\tfloat S3 = InputFloat();", "S3" };
            if (item.Contains("S4") && status == 1)
                return new string[] { "\t\t\tfloat S4 = InputFloat();", "S4" };

            if (item.Contains("H"))
                return new string[] { "\t\t\tfloat H = 0;", "H" };
            if (item.Contains("S1"))
                return new string[] { "\t\t\tfloat S1 = 0;", "S1" };
            if (item.Contains("S2"))
                return new string[] { "\t\t\tfloat S2 = 0;", "S2" };
            if (item.Contains("V"))
                return new string[] { "\t\t\tfloat V = 0;", "V" };
            if (item.Contains("P1"))
                return new string[] { "\t\t\tfloat P1 = 0;", "P1" };
            if (item.Contains("P2"))
                return new string[] { "\t\t\tfloat P2 = 0;", "P2" };
            if (item.Contains("L"))
                return new string[] { "\t\t\tfloat L = 0;", "L" };
            if (item.Contains("S3"))
                return new string[] { "\t\t\tfloat S3 = 0;", "S3" };
            if (item.Contains("S4"))
                return new string[] { "\t\t\tfloat S4 = 0;", "S4" };

            return null;
        }
        private void CreateFormule()
        {
            Formule = null;
            if (InputData.Count == 3)
            {
                if (InputData[0].Contains("H") && InputData[1].Contains("S1") && InputData[2].Contains("S2") && OutputData[0].Contains("V"))
                    Formule = "(float)1/3*H*(S1+S2+(float)Math.Sqrt(S1*S2));";
                else if (InputData[0].Contains("S1") && InputData[1].Contains("S2") && InputData[2].Contains("V") && OutputData[0].Contains("H"))
                    Formule = "(float)3*V/(S1+S2+(float)Math.Sqrt(S1*S2));";
                else if (InputData[0].Contains("H") && InputData[1].Contains("S2") && InputData[2].Contains("V") && OutputData[0].Contains("S1"))
                    Formule = "(float)(-S2 + (float)Math.Sqrt(S2*S2 + 36*V*V/H - 4*S2*S2))/2;";
                else if (InputData[0].Contains("H") && InputData[1].Contains("S1") && InputData[2].Contains("V") && OutputData[0].Contains("S2"))
                    Formule = "(float)(-S1 + (float)Math.Sqrt(S1*S1 + 36*V*V/H - 4*S1*S1))/2;";
                else if (InputData[0].Contains("P1") && InputData[1].Contains("P2") && InputData[2].Contains("L") && OutputData[0].Contains("S3"))
                    Formule = "(float)0.5*(P1+P2)/L;";
                else if (InputData[0].Contains("P2") && InputData[1].Contains("L") && InputData[2].Contains("S3") && OutputData[0].Contains("P1"))
                    Formule = "(float)2*S3/L-P2;";
                else if (InputData[0].Contains("P1") && InputData[1].Contains("L") && InputData[2].Contains("S3") && OutputData[0].Contains("P2"))
                    Formule = "(float)2*S3/L-P1;";
                else if (InputData[0].Contains("P1") && InputData[1].Contains("P2") && InputData[2].Contains("S3") && OutputData[0].Contains("L"))
                    Formule = "(float)2*S3/(P1+P2);";
                else if (InputData[0].Contains("S3") && InputData[1].Contains("S1") && InputData[2].Contains("S2") && OutputData[0].Contains("S4"))
                    Formule = "(float)S1+S2+S3;";
                else if (InputData[0].Contains("S1") && InputData[1].Contains("S2") && InputData[2].Contains("S4") && OutputData[0].Contains("S3"))
                    Formule = "(float)S4-S1-S2;";
                else if (InputData[0].Contains("S3") && InputData[1].Contains("S2") && InputData[2].Contains("S4") && OutputData[0].Contains("S1"))
                    Formule = "(float)S4-S2-S3;";
                else if (InputData[0].Contains("S3") && InputData[1].Contains("S1") && InputData[2].Contains("S4") && OutputData[0].Contains("S2"))
                    Formule = "(float)S4-S1-S3;";
            }
        }
    }
}
