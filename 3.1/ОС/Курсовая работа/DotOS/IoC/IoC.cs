using DotOS.Services;
using DotOS.Services.SystemCall;
using DotOS.Utils;
using DotOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotOS.IoC
{
    class IoC
    {
        private readonly static ServiceProvider _provider;
        
        static IoC()
        {
            ServiceCollection serivces = new ServiceCollection();

            serivces.AddSingleton<DiskWorker>();
            serivces.AddSingleton<FileSystem>();
            serivces.AddTransient<ISystemCall, CreateFileCall>();
            serivces.AddTransient<ISystemCall, ReadDirectoryCall>();

            //wpf register
            serivces.AddSingleton<MainWindowViewModel>();
            serivces.AddSingleton<FilesPageViewModel>();
            serivces.AddSingleton<PageService>();
            serivces.AddSingleton<EventBus>();

            _provider = serivces.BuildServiceProvider();
        }
        public static T Resolve<T>() => _provider.GetService<T>();
    }
}
