﻿using AutoMapper;
using EasyCaching.Core;
using EasyCaching.Core.Configurations;
using EasyCaching.Redis;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Transavia.API.Middlewares;
using Transavia.Application.Queries.Sql;
using Transavia.Infrastructure.Cache;
using Transavia.Infrastructure.Cache.Redis;
using Transavia.Infrastructure.Data;
using Transavia.Infrastructure.EventDispatching;

namespace Transavia.API
{
    public sealed class Startup
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
            var connectionString = Configuration.GetConnectionString("TransaviaConnectionString");
            services.AddDbContext<TransaviaDbContext>(cfg =>
            {
                cfg.UseSqlServer(connectionString);
            });

            services.AddSingleton<IDistributedCache, DistributedCache>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();
            services.AddSingleton<IConnectionFactory>(new ConnectionFactory(connectionString));

            services
                .AddSwaggerGen(c =>
                {
                    c.DescribeAllEnumsAsStrings();

                    c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "Transavia Api",
                        Contact = new Contact
                        {
                            Name = "Transavia",
                            Email = "mtarcha@outlook.com",
                            Url = "https://github.com/mtarcha/Transavia_Airports"
                        }
                    });
                });

            var redisHost = Configuration["RedisHost"];
            var redisPort = int.Parse(Configuration["RedisPort"]);
            services.AddEasyCaching(options =>
            {
                options.UseRedis(config =>
                    {
                        config.DBConfig.Endpoints.Add(new ServerEndPoint(redisHost, redisPort));
                    }, "transavia.api");
            });

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

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseMiddleware<CachingMiddleware>();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transavia Api V1");
            });
        }
    }
}
