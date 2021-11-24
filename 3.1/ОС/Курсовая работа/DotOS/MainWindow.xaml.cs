using DotOS.Utils;
using System.Windows;

namespace DotOS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var form = IoC.IoC.Resolve<Formatter>();
            form.Formatting();
        }
    }
}
