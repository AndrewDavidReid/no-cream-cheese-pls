using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NoCreamCheesePls.Business.Exceptions;

namespace NoCreamCheesePls.Filters
{
  public class ApiExceptionFilter : ExceptionFilterAttribute
  {
    public override void OnException(ExceptionContext context)
    {
      ApiError api_error = null;

      // Handle deliberately thrown 400 errors
      if (context.Exception is BadRequestException ex)
      {
        // handle explicit 'known' API errors
        context.Exception = null;
        api_error = new ApiError(ex.ErrorMessages);

        context.HttpContext.Response.StatusCode = 400;
      }
      else if (context.Exception is UnauthorizedAccessException)
      {
        context.Exception = null;

        api_error = new ApiError(new List<string>{"Unauthorized Access"});
        context.HttpContext.Response.StatusCode = 403;
        // TODO: handle unauthorized access logging here.
      }
      else
      {
        api_error = new ApiError(new List<string>{context.Exception.GetBaseException().Message});
        api_error.StackTrace = context.Exception.StackTrace;
        context.HttpContext.Response.StatusCode = 500;
        context.Exception = null;
        // TODO: handle exception logging here.
      }

      // Return the json result
      context.Result = new JsonResult(api_error);

      base.OnException(context);
    }
  }
}
