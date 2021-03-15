using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _5Lab
{
    public class Pictures
    {
        public List<PictInfo> list { get; set; } = new List<PictInfo>();
        public void Add(int id, int[,] hw, int[] prop_id, int bytes, double kbytes, Bitmap bmp)
        {
            list.Add(new PictInfo(id, hw, prop_id, bytes, kbytes, bmp));
        }
        public Pictures() { }
    }
    public class PictInfo
    {
        [XmlAttribute]
        public int id { get; set; }
        public int[] propertyId { get; set; }
        private int[,] hw { get; set; }
        public PictureSize size { get; set; }
        [XmlIgnore]
        public Bitmap image;
        public byte[] Image
        { get 
            {
                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(image, typeof(byte[]));
            } set 
            {
                var ms = new MemoryStream(value);
                image = new Bitmap(ms);
                
            } }
        public int[] Hw 
        { 
            get 
            {
                int[] res = new int[2];
                res[0] = hw[0, 0];
                res[1] = hw[0, 1];
                return res;
            } 
            set 
            {
                hw = new int[1, 2];
                hw[0, 0] = value[0];
                hw[0, 1] = value[1];
            } 
        }
        
        public PictInfo(int id, int[,] hw, int[] prop_id, int bytes, double kbytes, Bitmap bmp)
        {
            this.id = id;
            this.propertyId = prop_id;
            this.hw = hw;
            this.size = new PictureSize(bytes, kbytes);
            this.image = bmp;
            
        }
        public PictInfo() 
        {

        }
        
    }
    public class PictureSize
    {
        public int bytes { get; set; }
        public double kbytes { get; set; }
        public PictureSize(int bytes, double kbytes)
        {
            this.bytes = bytes;
            this.kbytes = kbytes;
        }
        public PictureSize() { }
    }
}
