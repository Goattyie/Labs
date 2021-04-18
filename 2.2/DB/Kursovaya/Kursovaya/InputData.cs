using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursovaya
{
    static class InputData
    {
        public static bool CheckString(string line, string name)
        {
            if (line == null || line.Length == 0)
            {
                Message.FieldError(name);
                return false;
            }
            return true;
        }
        public static bool CheckInt(int value, string name, int minValue, int maxValue)
        {
            if (value < minValue || value > maxValue)
            {
                Message.FieldError(name);
                return false;
            }
            return true;
        }
        public static bool CheckInt(string value, string name, int minValue, int maxValue)
        {
            try
            {
                if (Convert.ToInt32(value) < minValue || Convert.ToInt32(value) > maxValue)
                {
                    Message.FieldError(name);
                    return false;
                }
                return true;
            }
            catch 
            {
                Message.FieldError(name);
                return false; 
            }
        }
    }
}
