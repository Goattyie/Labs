using DotOS.Utils;
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

            serivces.AddSingleton<SystemSettings>();
            serivces.AddSingleton<SuperBlockInfo>();
            serivces.AddSingleton<Formatter>();

            _provider = serivces.BuildServiceProvider();
        }
        public static T Resolve<T>() => _provider.GetService<T>();
    }
}
