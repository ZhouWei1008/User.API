using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using User.Identity.Services;
using DnsClient;
using System.Net;
using ServerRegister.Expansion.Consul;
using Microsoft.Extensions.Configuration;
using ServerRegister.Expansion.Consul.Entity;
using Microsoft.Extensions.Options;

namespace User.Identity
{
    public class Startup
    {

        private readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                .AddExtensionGrantValidator<Authentication.SmsAuthCodeValidator>()
                .AddDeveloperSigningCredential()
                .AddInMemoryClients(config.GetClients())
                .AddInMemoryIdentityResources(config.GetIdentityResources())
                .AddInMemoryApiResources(config.GetApiResource())
                ;

            services.AddSingleton(new HttpClient());

            services.AddScoped<IAuthCodeService, TestAuthCodeService>()
                .AddScoped<IUserService, UserService>();

            services.AddMvc();



            services.AddConuslClient(c =>
            {
                Configuration.Bind("ServiceDiscovery", c);
            });
            //TEST
            //var _ServiceDisvoveryOption = services.BuildServiceProvider()
            //    .GetRequiredService<IOptions<ServiceDisvoveryOptions>>().Value;
            //var client = new ConsulDNSClient(_ServiceDisvoveryOption);
            //var result = client.GetHttpClient().GetAwaiter().GetResult();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();




            app.UseMvc();

        }
    }
}
