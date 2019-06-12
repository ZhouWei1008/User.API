using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using User.Api.Data;
using User.Api.Filters;     


namespace User.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>{
                options.Filters.Add(typeof(GlobalExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<UserContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("MysqlUser"));
            });

             
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseMvc();
            InitUserDataBase(app);
        }

        public void InitUserDataBase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userContext = scope.ServiceProvider.GetRequiredService<UserContext>();
                userContext.Database.Migrate();
                if (userContext.Users.Count() <= 0)
                {
                    userContext.Users.Add(new Models.AppUser()
                    {
                        UserName = "ian",
                        Company = "TEST",
                        ID = 1,
                        Title = string.Empty
                    });
                    userContext.SaveChanges();
                }
            }
        }
    }
}
