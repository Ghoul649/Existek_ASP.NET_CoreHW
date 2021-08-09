using App3.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace App3.Middleware
{
    public class PublishedArticlesMiddleware
    {
        private RequestDelegate _nextMiddleware;

        public PublishedArticlesMiddleware(RequestDelegate next)
        {
            _nextMiddleware = next;
        }
        public async Task InvokeAsync(HttpContext context, IPublishingService publisher)
        {
            var result = JsonSerializer.Serialize(publisher.Published);
            await context.Response.WriteAsync(result);
        }
    }
}
