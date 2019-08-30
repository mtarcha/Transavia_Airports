using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using Transavia.Web.MVC.Clients;

namespace Transavia.Web.MVC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var apiUrl = _configuration["AirportsApiUrl"];
            services.AddTransient(x => RestClient.For<IAirportsClient>(apiUrl));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc(configuration =>
            {
                configuration.MapRoute("Default", "{controller=Airports}/{action=Get}/{id?}");
            });
        }
    }
}
