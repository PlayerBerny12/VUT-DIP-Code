using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DeepfakeDetectionFramework.Filters;

public class ExceptionFilter : ExceptionFilterAttribute
{
    public string Message { get; set; } = "Unexpected error occured.";

    public ExceptionFilter()
    {
    }

    public override void OnException(ExceptionContext context)
    {
        Log.ForContext("RequestPath", context.HttpContext.Request.Path)
            .Error(context.Exception, Message);

        context.Result = new BadRequestObjectResult(Message);
        context.ExceptionHandled = true;
    }
}