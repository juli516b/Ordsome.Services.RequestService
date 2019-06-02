using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ordsome.Services.CrossCuttingConcerns.Exceptions;

namespace Ordsome.Services.CrossCuttingConcerns.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationException exception:
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(
                        exception.Failures);

                    return;
                case ArgumentNullException _:
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    context.Result =
                        new JsonResult("A command cant be null. Please provide the properties for this command");

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