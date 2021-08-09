using App3.Interfaces;
using App3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Middleware
{
    public class HandlingMiddleware
    {
        private RequestDelegate _nextMiddleware;

        public HandlingMiddleware(RequestDelegate next)
        {
            _nextMiddleware = next;
        }
        public async Task InvokeAsync(HttpContext context, Article article, IArticleHandlerService articleHandlerService)
        {
            try
            {
                articleHandlerService.Handle(article);
            }
            catch(ArgumentException e)
            {
                context.Response.StatusCode = 412;
                await context.Response.WriteAsync(e.Message);
                return;
            }
            await _nextMiddleware.Invoke(context);
        }
    }
}
