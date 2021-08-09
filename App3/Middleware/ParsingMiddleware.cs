using App3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Middleware
{
    public class ParsingMiddleware
    {
        private RequestDelegate _nextMiddleware;

        public ParsingMiddleware(RequestDelegate next)
        {
            _nextMiddleware = next;
        }
        public async Task InvokeAsync(HttpContext context, Article article)
        {
            article.Title = context.Request.Query["Title"].FirstOrDefault();
            article.Content = context.Request.Query["Content"].FirstOrDefault();
            article.Author = context.Request.Query["Author"].FirstOrDefault();

            await _nextMiddleware.Invoke(context);
        }
    }
}
