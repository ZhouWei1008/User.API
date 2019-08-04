using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServerRegister.Expansion.Consul.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerRegister.Expansion.Consul
{
    public static class ConsulServiceCollectionExtensions
    {
        public static void AddConuslService(this IServiceCollection services, Action<ServiceDisvoveryOptions> setupAction)
        {
            services.Configure(setupAction);
            ConsulContext.SetServiceCollection(services);
            //var provider = services.BuildServiceProvider();//get an instance of IServiceProvider
            //var options = provider.GetRequiredService<IOptions<ServiceDisvoveryOptions>>().Value;
        }

        public static void AddConuslClient(this IServiceCollection services, Action<ServiceDisvoveryOptions> setupAction)
        {
            services.Configure(setupAction);
            ConsulContext.SetServiceCollection(services);
            //var provider = services.BuildServiceProvider();//get an instance of IServiceProvider
            //var options = provider.GetRequiredService<IOptions<ServiceDisvoveryOptions>>().Value;
        }
    }
}
