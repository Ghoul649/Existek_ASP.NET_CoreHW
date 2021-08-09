using App3.Interfaces;
using App3.Middleware;
using App3.Models;
using App3.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3
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
            services.AddScoped<Article>();
            services.AddSingleton<IArticleHandlerService, ToHTMLHandler>();
            services.AddSingleton<IVerifyingService, ContentLengthVerificator>();
            services.AddSingleton<IPublishingService, Publisher>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.Map("/publish", app => 
            {
                app.UseMiddleware<ParsingMiddleware>();
                app.UseMiddleware<HandlingMiddleware>();
                app.UseMiddleware<VerifyingMiddleware>();
                app.UseMiddleware<PublishMiddleware>();
            });

            app.Map("/list", app =>
            {
                app.UseMiddleware<PublishedArticlesMiddleware>();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
