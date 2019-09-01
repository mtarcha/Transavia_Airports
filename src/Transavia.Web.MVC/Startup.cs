using System;
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

            var apiUrl = _configuration["TransaviaApiUrl"];
            services
                .AddHttpClient("airport", c => { c.BaseAddress = new Uri(apiUrl); })
                .AddTypedClient(c => RestClient.For<IAirportsClient>(c));

            services
                .AddHttpClient("countries", c => { c.BaseAddress = new Uri(apiUrl); })
                .AddTypedClient(c => RestClient.For<ICountriesClient>(c));

            services.AddMvc();
        }

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
