using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using ServerRegister.Expansion.Consul.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServerRegister.Expansion.Consul
{
    public class ConsulRegister
    {

        public void Register(
            IApplicationBuilder app,
            IApplicationLifetime appLife,
            IOptions<ServiceDisvoveryOptions> serviceOptions,
            IConsulClient consul
            )
        {
            var features = app.Properties["server.Features"] as FeatureCollection;
            var addresses = features.Get<IServerAddressesFeature>()
                .Addresses
                .Select(p => new Uri(p));

            foreach (var address in addresses)
            {
                var serviceId = $"{serviceOptions.Value.ServiceName}_{address.Host}:{address.Port}";

                var httpCheck = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = new Uri(address, "HealthCheck").OriginalString
                };

                var registration = new AgentServiceRegistration()
                {
                    Checks = new[] { httpCheck },
                    Address = address.Host,
                    ID = serviceId,
                    Name = serviceOptions.Value.ServiceName,
                    Port = address.Port,
                    Tags = new string[] { "TEST", "USER API SERVICE" },                   
                };

                consul.Agent.ServiceRegister(registration).GetAwaiter().GetResult();
                 
                appLife.ApplicationStopping.Register(() =>
                {
                    consul.Agent.ServiceDeregister(serviceId).GetAwaiter().GetResult();
                });              
            }
        }
    }
}
