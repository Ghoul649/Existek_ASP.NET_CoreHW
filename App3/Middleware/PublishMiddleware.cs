using App3.Interfaces;
using App3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Middleware
{
    public class PublishMiddleware
    {
        private RequestDelegate _nextMiddleware;

        public PublishMiddleware(RequestDelegate next) 
        {
            _nextMiddleware = next;
        }
        public async Task InvokeAsync(HttpContext context, Article article, IPublishingService publishingService) 
        {
            publishingService.Publish(article);
        }
    }
}
