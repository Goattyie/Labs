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
        public static void ErrorShow(string msg) { MessageBox.Show(msg, "Ошибка 010", MessageBoxButtons.OK, MessageBoxIcon.Error); }


        public static void Success() { MessageBox.Show("Операция выполнена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        public static DialogResult DeleteWarning()
        {
            return MessageBox.Show("ВНИМЕНИЕ! Все записи из других таблиц, которые связаны с этой записью будут удалены. Хотите удалить?", "Успех", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
    }
}
