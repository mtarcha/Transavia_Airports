using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddMediatR();
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
        }
    }
}
