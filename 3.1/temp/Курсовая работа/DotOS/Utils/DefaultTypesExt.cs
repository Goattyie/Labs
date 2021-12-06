using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.Utils
{
    static class DefaultTypesExt
    {
        public static byte[] ToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        public static byte[] ToByteArray(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        public static string ArrayToString(this byte[] value)
        {
            
            return Encoding.Default.GetString(value);
        }

        public static short ArrayToShort(this byte[] value)
        {
            return BitConverter.ToInt16(value);
        }

        public static int ArrayToInt(this byte[] value)
        {
            return BitConverter.ToInt32(value);
        }

        public static byte[] ToByteArray(this short value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}
