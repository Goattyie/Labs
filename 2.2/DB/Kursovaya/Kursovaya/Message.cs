using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya
{
    static class Message
    {
        public static void AutorizationError()
        {
            MessageBox.Show("Неверный логин или пароль.", "Ошибка 000", MessageBoxButtons.OK, MessageBoxIcon.Error);
       
        }
        public static void ConnectionError()
        {
            MessageBox.Show("Невозможно подключиться к базе данных", "Ошибка 001", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void FieldError(string type)
        {
            MessageBox.Show($"Поле {type} указано неверно.", "Ошибка 002", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
