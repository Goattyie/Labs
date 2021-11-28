using DevExpress.Mvvm;
using DotOS.Models;
using DotOS.Services.SystemCall;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.ViewModels
{
    class FilesPageViewModel : BindableBase
    {
        private readonly IEnumerable<ISystemCall> _systemCalls;

        public FilesPageViewModel(IEnumerable<ISystemCall> systemCalls) 
        {
            _systemCalls = systemCalls;
            var call = _systemCalls.FirstOrDefault(x => x.CanExecute("create file1.dot"));
            call?.Execute();
        }
      
        public ObservableCollection<FileInfo> FilesInfo { get; set; }
    }
}
