using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace _5Lab
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            ds.Tables.Add("Изображения");
            ds.Tables[0].Columns.Add("ID");
            ds.Tables[0].Columns.Add("Высота, Ширина");
            ds.Tables[0].Columns.Add("Размер в байтах");
            ds.Tables[0].Columns.Add("Размер в киллобайтах");
            ds.Tables[0].Columns.Add("Property id[0]");
            ds.Tables[0].Columns.Add("Property id[1]");
            ds.Tables[0].Columns.Add("Изображение");
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.ReadOnly = true;
        }
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private Pictures pictures = new Pictures();
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        
        public void AddNode(int id, int[,] hw, int[] prop_id, int bytes, double kbytes, Bitmap bmp)
        {
            ds.Tables[0].Rows.Add(id, hw[0, 0] + " " + hw[0, 1], bytes, kbytes, prop_id[0], prop_id[1]);
            pictures.Add(id, hw, prop_id, bytes, kbytes, bmp);
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void SerializeXML()
        {
            XmlSerializer xml = new XmlSerializer(typeof(Pictures));
            
            using (XmlWriter xmlWriter = XmlWriter.Create("Pictures.xml"))
            {
                
                xml.Serialize(xmlWriter, pictures);
            }
        }
            private void button1_Click(object sender, EventArgs e)
        {
            SerializeXML();
        }

        private Pictures DeserializeXML()
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(Pictures));

                using (XmlReader xmlRider = XmlReader.Create("Pictures.xml"))
                {

                    return (Pictures)xml.Deserialize(xmlRider);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error. Файл заполнен неверно или не существует");
                return null;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            pictures = DeserializeXML();
            if (pictures == null)
                return;
            ds.Tables[0].Clear();
            for(int i = 0; i < pictures.list.Count(); i++)
            {
                ds.Tables[0].Rows.Add(pictures.list[i].id, pictures.list[i].Hw[0] + ", " + pictures.list[i].Hw[1],
                   pictures.list[i].size.bytes, pictures.list[i].size.kbytes, pictures.list[i].propertyId[0], pictures.list[i].propertyId[1]);
            }
        }
        public int GetId()
        {
            if (ds.Tables[0].Rows.Count == 0)
                return 1;
            else return Convert.ToInt32(dataGridView1[0, ds.Tables[0].Rows.Count - 1].Value)+1;
        }
        OpenFileDialog fileDialog = new OpenFileDialog();
        Image img;
        FileInfo file;
        private void button3_Click(object sender, EventArgs e)
        { 
                fileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                fileDialog.Multiselect = true;
                var state = fileDialog.ShowDialog();

                if (state == DialogResult.Cancel)
                    return;

                if (state == DialogResult.OK)
                {
                for(int i = 0; i < fileDialog.FileNames.Length; i++) 
                {
                    img = Image.FromFile(fileDialog.FileNames[i]);
                    file = new FileInfo(fileDialog.FileNames[i]);
                    int[,] hs = new int[1, 2];
                    hs[0, 0] = img.Size.Height;
                    hs[0, 1] = img.Size.Width;
                    AddNode(GetId(), hs, img.PropertyIdList, (int)file.Length, (float)file.Length / 1024, (Bitmap)img);
                }    
            }
        }

        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if(dataGridView1.SelectedCells[0].ColumnIndex == 6)
            {
                Form1 form1 = new Form1(pictures.list[dataGridView1.SelectedCells[0].RowIndex].image);
                form1.Show();
            }

        }
    }
}
