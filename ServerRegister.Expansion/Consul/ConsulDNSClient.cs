using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using DnsClient; 
using ServerRegister.Expansion.Consul.Entity;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ServerRegister.Expansion.Consul
{
    public class ConsulDNSClient
    {

        public HttpClient Client { get; private set; }
        private readonly ServiceDisvoveryOptions _ServiceDisvoveryOptions;
        private readonly String GetHttpClientKey = "_CONSUL_HTTPCLIENT_";

        public ConsulDNSClient(string address, string serverName, int port = 8600)
        {
            _ServiceDisvoveryOptions = new ServiceDisvoveryOptions
            {
                ServiceName = serverName,
                Consul = new ConsulOptions
                {
                    DnsEndpoint = new DnsEndpoint
                    {
                        Address = address,
                        Port = port
                    }
                }
            };
        }
        public ConsulDNSClient(ServiceDisvoveryOptions serviceDisvoveryOptions) {
            _ServiceDisvoveryOptions = serviceDisvoveryOptions;
        }


        public IDnsQuery GetDnsQuery()
        {
            return new LookupClient(IPAddress.Parse(_ServiceDisvoveryOptions.Consul.DnsEndpoint.Address)
                , _ServiceDisvoveryOptions.Consul.DnsEndpoint.Port);
        }


        public async Task<HttpClient> GetHttpClient()
        {
            var result = await this.GetDnsQuery().ResolveServiceAsync("service.consul", _ServiceDisvoveryOptions.ServiceName);
            if (result.Any())
            {
                var serverHost = result.First();
                if (serverHost != null && serverHost.AddressList.Any())
                {
                    ConsulContext.Services.AddHttpClient(
                        $"{GetHttpClientKey}{_ServiceDisvoveryOptions.ServiceName}_{serverHost.AddressList.FirstOrDefault()}_{serverHost.Port}",
                        c =>
                        {
                            //c.BaseAddress = new Uri(serverHost.AddressList);
                        }
                        );
                }
            }


            return null;
            //_dns.ResolveServiceAsync("service.consul", _options.Value.ServiceName);
            //Client.BaseAddress=
            //HttpClientFactory httpClientFactory = HttpClientFactory.Create(new DelegatingHandler() {

            //})             
        }


    }
}
