using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerRegister.Expansion.Consul
{
    internal class ConsulContext
    {
        internal static IServiceCollection Services { get; private set; }
        internal static void SetServiceCollection(IServiceCollection services)
        {
            Services = services;
        }

        internal static T GetProvider<T>()
        {
            //var provider = services.BuildServiceProvider();//get an instance of IServiceProvider
            //var options = provider.GetRequiredService<IOptions<ServiceDisvoveryOptions>>().Value;
            return Services.BuildServiceProvider().GetRequiredService<T>();
        }
    }
}
