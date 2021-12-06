using DevExpress.Mvvm;
using DotOS.Models;
using DotOS.Services.SystemCall;
using DotOS.Utils;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DotOS.ViewModels
{
    class FilesPageViewModel : BindableBase
    {
        private readonly MessageBus _messageBus;
        private readonly PageService _navigator;
        private readonly SystemCallHandler _commandHandler;

        public FilesPageViewModel(SystemCallHandler commandHandler, PageService navigator, MessageBus messageBus) 
        {
            _messageBus = messageBus;
            _navigator = navigator;
            _commandHandler = commandHandler;

            _messageBus.Receive<FilesMessage>(this, msg =>
            {
                FilesInfo = new ObservableCollection<FileInfo>(msg.FilesInfo);
                return Task.CompletedTask;
            });

            _messageBus.Receive<AddFileMessage>(this, msg =>
            {
                FilesInfo.Add(msg.File);
                return Task.CompletedTask;
            });

            _messageBus.Receive<FileText>(this, msg => { MessageBox.Show(msg.Text); return Task.CompletedTask; });
            //_commandHandler.Handle("show .dir");

        }
        public ObservableCollection<FileInfo> FilesInfo { get; set; }
        public FileInfo SelectedFile { get; set; }
        public ICommand CreateFile => new DelegateCommand(() => { _navigator.Navigate(new CreateFileWindow()); });
        public ICommand OpenFile => new DelegateCommand(() => 
        {
            if (SelectedFile == null)
                return;

            _commandHandler.Handle($"read {SelectedFile.Name}");
        });
        public ICommand RemoveFile => new DelegateCommand(() => 
        {
            if (SelectedFile == null)
                return;
        });
    }
}
