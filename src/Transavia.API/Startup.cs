using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Transavia.Application.Queries.Sql;
using Transavia.Infrastructure.Data;

namespace Transavia.API
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
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfiles(new Profile[]
            //    {
            //        new ViewModelsMapper(),
            //        new DomainEventsMapping(),
            //    });
            //});

            //var mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);

            services.AddMediatR();
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddScoped<IEventDispatcher, EventDispatcher>();
            //services.AddSingleton<IIntegrationEventHandler<BookRateChangedEvent>, BookRateChangedEventHandler>();
            var connectionString = Configuration.GetConnectionString("TransaviaConnectionString");
            services.AddDbContext<TransaviaDbContext>(cfg =>
            {
                cfg.UseSqlServer(connectionString);
            });
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConnectionFactory>(new ConnectionFactory(connectionString));
            services.AddMvc()
                //.AddFluentValidation(x => x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services
            //    .AddSwaggerGen(c =>
            //    {
            //        c.DescribeAllEnumsAsStrings();

            //        c.SwaggerDoc("v1", new Info
            //        {
            //            Version = "v1",
            //            Title = "Home Library Api",
            //            Contact = new Contact
            //            {
            //                Name = "Home Library",
            //                Email = "mtarcha@outlook.com",
            //                Url = "https://github.com/mtarcha/Library"
            //            }
            //        });
            //    });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
