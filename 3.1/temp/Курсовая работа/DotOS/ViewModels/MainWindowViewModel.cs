﻿using DevExpress.Mvvm;
using DotOS.Pages;
using DotOS.Services;
using System.Windows.Controls;
using System.Windows.Input;

namespace DotOS.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly PageService _navigator;
        public MainWindowViewModel(PageService navigator, FileSystem fileSystem)
        {
            fileSystem.Formating();
            _navigator = navigator;
            _navigator.OnPageChanged += page => CurrentPage = page;
            _navigator.Navigate(new FilesPage());
        }
        public Page CurrentPage { get; set; }
        public ICommand GoToBack => new DelegateCommand(() => { _navigator.GoToBack(); });
    }
}
