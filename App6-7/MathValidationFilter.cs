using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App6_7
{
    public class MathValidationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cookies = context.HttpContext.Request.Cookies;
            var headers = context.HttpContext.Request.Headers;
            if (!(cookies.TryGetValue("arg1", out string arg1) && cookies.TryGetValue("arg2", out string arg2) && headers.TryGetValue("MathAddResult", out var result)))
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
            if (!(int.TryParse(arg1, out int a) && int.TryParse(arg2, out int b) && int.TryParse(result, out int c))) 
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
            if (a + b != c)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }
        }
    }
}
