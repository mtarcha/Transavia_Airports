using AutoMapper;
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
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new Profile[]
                {
                    new Utilities.AutoMapper(),
                });
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            var apiUrl = _configuration["AirportsApiUrl"];
            services.AddTransient(x => RestClient.For<IAirportsClient>(apiUrl));
            services.AddTransient(x => RestClient.For<ICountriesClient>(apiUrl));
            services.AddTransient(x => RestClient.For<IStatusesClient>(apiUrl));
            services.AddTransient(x => RestClient.For<ISizesClient>(apiUrl));
            services.AddTransient(x => RestClient.For<ITypesClient>(apiUrl));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseMvc(configuration =>
            {
                configuration.MapRoute("Default", "{controller=Airports}/{action=Search}/{id?}");
            });
        }
    }
}
