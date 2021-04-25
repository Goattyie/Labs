using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Kursovaya
{
    public partial class EditSup : Form
    {
        public EditSup(string type)
        {
            InitializeComponent();
            this.type = type;
        }
        string type, edit = "Создание";
        private void EditSup_Load(object sender, EventArgs e)
        {
            label1.Text = edit + " записи в таблице " + type;
            label2.Text = type + ':';
            if (edit == "Создание")
            button1.Text = "Создать";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string text = InputData.CheckString(textBox1.Text);
            Table sup = Table.ReturnTable(type);
            if (type == "Район")
                sup = new Area(text);
            else if (type == "Тип собственности")
                sup = new Own(text);
            else if (type == "Язык")
                sup = new Lang(text);
            else if (type == "Город")
                sup = new City(text);
            else if (type == "Жанр")
                sup = new Style(text);
            else if (type == "Тип переплета")
                sup = new Binding(text);

            if (sup.Insert())
                textBox1.Text = "";
        }     
        private void EditSup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason != new CloseReason())
            {
                textBox1.Text = null;
            }
        }

        public string GetResult() { return textBox1.Text; }
    }
}
