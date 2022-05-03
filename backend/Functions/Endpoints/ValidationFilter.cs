using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using FluentValidation;
using Microsoft.Azure.WebJobs.Host;
using System.Threading;
using System.Net;

namespace Functions.Endpoints;

[Obsolete("Using preview features")]
public class ValidationFilter : IFunctionExceptionFilter {

    private readonly IHttpContextAccessor _accessor;

    public ValidationFilter(IHttpContextAccessor accessor) {
        _accessor = accessor;
    }

    [Obsolete("Using preview feature")]
    public async Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken) {

        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = string.Empty;

        switch (exceptionContext.Exception.InnerException) {
            case ValidationException validationEx:
                statusCode = HttpStatusCode.BadRequest;
                message = validationEx.Message;
                break;
            case InvalidOperationException invalidEx:
                if (invalidEx.Message.Contains("Exception binding parameter")) { 
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Could not bind request body";
                }
                break;
        }

        _accessor.HttpContext.Response.StatusCode = (int)statusCode;
        await _accessor.HttpContext.Response.WriteAsync(message, cancellationToken: cancellationToken);

    }

}