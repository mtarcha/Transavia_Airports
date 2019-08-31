using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Transavia.Infrastructure.Cache;

namespace Transavia.API.Middlewares
{
    public class CachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _distributedCache;

        public CachingMiddleware(RequestDelegate next, IDistributedCache distributedCache)
        {
            _next = next;
            _distributedCache = distributedCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == "GET")
            {
                var key = context.Request.Path + context.Request.QueryString;
                var cached = await _distributedCache.GetAsync<byte[]>(key);

                if (cached != null)
                {
                    await context.Response.Body.WriteAsync(cached, 0, cached.Length);
                }
                else
                {
                    var originalBody = context.Response.Body;

                    try
                    {
                        using (var memStream = new MemoryStream())
                        {
                            context.Response.Body = memStream;

                            context.Response.OnStarting(state => {
                                var httpContext = (HttpContext)state;
                                httpContext.Response.Headers.Add("from-database", new StringValues());
                                return Task.FromResult(0);
                            }, context);

                            await _next(context);

                            await _distributedCache.SetAsync(key, memStream.GetBuffer(), TimeSpan.FromMinutes(5));
                            
                            memStream.Position = 0;
                            await memStream.CopyToAsync(originalBody);
                        }
                    }
                    finally
                    {
                        context.Response.Body = originalBody;
                    }
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
}