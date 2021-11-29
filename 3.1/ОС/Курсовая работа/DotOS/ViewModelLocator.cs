using DotOS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.WpfService
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => IoC.IoC.Resolve<MainWindowViewModel>();
        public FilesPageViewModel FilesPageViewModel => IoC.IoC.Resolve<FilesPageViewModel>();
        public CreateFileViewModel CreateFileViewModel => IoC.IoC.Resolve<CreateFileViewModel>();
    }
}
