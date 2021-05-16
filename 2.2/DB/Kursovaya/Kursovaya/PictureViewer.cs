using System.Drawing;
using System.Windows.Forms;

namespace Kursovaya
{
    public partial class PictureViewer : Form
    {
        public PictureViewer(string filename)
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap(filename);
            if (bitmap.Width > 700 || bitmap.Height > 500)
            {
                Size size = new Size(bitmap.Width / 4, bitmap.Height / 4);
                bitmap = new Bitmap(bitmap, size);
            }
            pictureBox1.Image = (Image)bitmap;
            pictureBox1.Size = pictureBox1.Image.Size;
            this.Size = pictureBox1.Size;
        }
    }
}
