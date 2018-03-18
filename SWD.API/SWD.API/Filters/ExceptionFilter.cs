using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SWD.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var request = context.HttpContext.Request;
            if (request.Headers != null)
            {
                context.HttpContext.Response.StatusCode = 500;
                if (context.Exception is UnauthorizedAccessException)
                {
                    context.HttpContext.Response.StatusCode = 511;
                }
                context.Result = new JsonResult(new {context.Exception.Message});
            }
        }
    }
}
