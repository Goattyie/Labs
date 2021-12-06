using DotOS.Models;
using DotOS.Services;
using DotOS.Services.SystemCall;
using DotOS.Utils;
using DotOS.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
            serivces.AddSingleton<Session>();
            serivces.AddSingleton<SystemCallHandler>();
            serivces.AddSingleton<Superblock>();
            serivces.AddTransient<ISystemCall, CreateFileCall>();
            serivces.AddTransient<ISystemCall, ReadDirectoryCall>();
            serivces.AddTransient<ISystemCall, CreateDirCall>();
            serivces.AddTransient<ISystemCall, ReadFileCall>();

            //wpf register
            serivces.AddSingleton<MainWindowViewModel>();
            serivces.AddSingleton<FilesPageViewModel>();
            serivces.AddTransient<CreateFileViewModel>();
            serivces.AddSingleton<PageService>();
            serivces.AddSingleton<MessageBus>();

            _provider = serivces.BuildServiceProvider();
        }
        public static T Resolve<T>() => _provider.GetService<T>();
    }
}
