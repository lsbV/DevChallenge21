﻿using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MainServer.Infrastructure.Filters;

public class AppExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case EntityNotExistException entityNotExistException:
                context.Result = new NotFoundObjectResult(new { Error = entityNotExistException.Message });
                context.ExceptionHandled = true;
                break;
            case EntityAlreadyExistsException entityAlreadyExistsException:
                context.Result = new ConflictObjectResult(new { Error = entityAlreadyExistsException.Message });
                context.ExceptionHandled = true;
                break;
            default:
                context.Result = new StatusCodeResult(500);
#if DEBUG
                context.Result = new ObjectResult(new { Error = context.Exception.Message });
#endif
                context.ExceptionHandled = true;
                break;
        }
    }
}