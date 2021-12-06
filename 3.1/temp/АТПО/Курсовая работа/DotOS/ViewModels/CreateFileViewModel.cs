using DevExpress.Mvvm;
using DotOS.Services.SystemCall;
using DotOS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DotOS.ViewModels
{
    class CreateFileViewModel : BindableBase
    {
        private readonly SystemCallHandler _callHandler;
        private readonly PageService _navigator;

        public CreateFileViewModel(SystemCallHandler callHandler, PageService navigator)
        {
            _callHandler = callHandler;
            _navigator = navigator;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        
        public ICommand CreateFile => new DelegateCommand(() => 
        {
            try
            {
                _callHandler.Handle($"create {Name}.dot >{Text}");
                _navigator.GoToBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
    }
}
