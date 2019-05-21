using System;
using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException exception)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    exception.Failures);

                return;
            }

            var code = HttpStatusCode.InternalServerError;

            switch (context.Exception)
            {
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case ForbiddenException _:
                    code = HttpStatusCode.Forbidden;
                    break;
            }


            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(new
            {
                error = new[] {context.Exception.Message},
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}