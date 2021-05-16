using System;


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
                Message.ErrorShow($"Поле {column} должно быть числом.");
                return false; 
            };
        }
        public static bool CheckDouble(string value, string column)
        {
            try
            {
                Convert.ToDouble(value);
                return true;
            }
            catch
            {
                Message.ErrorShow($"Поле {column} должно быть вещественным числом.");
                return false;
            };
        }
        public static string RemoveSymbols(string line)
        {
            if(line != null)
                return line.Trim('\'');
            return null;
        }
    }
}
