using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    static class InputData
    {
        public static string CheckString(string line)
        {
            if (line == null || line == "")
                return "NULL";
                
            return line.Insert(0, "'").Insert(line.Length + 1, "'");
        }
        public static bool CheckInt(string value, string column)
        {
            try
            {
                Convert.ToInt32(value);
                return true;
            }
            catch {
                SQL.ErrorShow($"Поле {column} должно быть числом.");
                return false; 
            };
        }
    }
}
