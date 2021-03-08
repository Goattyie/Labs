using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;


namespace ImageHandler
{
    public delegate void ProgressDelegate(double percent);


    public interface IImageHandler
    {
        string HandlerName { get; set; }
        Bitmap Source { get; set; }
        Bitmap Result { get; set; }

        void init(SortedList<string, object> parameters);
        void startHandle(ProgressDelegate progress);
    }


    public class ImageHandler : IImageHandler
    {
        public string HandlerName { get; set; }
        public Bitmap Source { get; set; }
        public Bitmap Result { get; set; }

        public void init(SortedList<string, object> parameters) { }
        public void startHandle(ProgressDelegate progress) { }
    }
}
