using App3.Interfaces;
using App3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Middleware
{
    public class VerifyingMiddleware
    {
        private RequestDelegate _nextMiddleware;

        public VerifyingMiddleware(RequestDelegate next)
        {
            _nextMiddleware = next;
        }
        public async Task InvokeAsync(HttpContext context, Article article, IVerifyingService verifyingService)
        {
            if (!verifyingService.Verify(article)) 
            {
                context.Response.StatusCode = 412;
                await context.Response.WriteAsync("Article is not valid");
                return;
            }
            await _nextMiddleware.Invoke(context);
        }
    }
}
