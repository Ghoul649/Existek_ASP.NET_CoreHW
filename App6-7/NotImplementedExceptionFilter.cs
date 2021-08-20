using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App6_7
{
    public class NotImplementedExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is NotImplementedException) 
            {
                context.Result = new ContentResult() { Content = "Action is not implemented"};
            }
        }
    }
}
