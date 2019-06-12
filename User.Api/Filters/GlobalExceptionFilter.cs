using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
namespace User.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;
        private readonly ILogger<GlobalExceptionFilter>  _logger;

        public GlobalExceptionFilter(IHostingEnvironment env,ILogger<GlobalExceptionFilter>  logger)
        {
            _env=env;
            _logger=logger;
        }
        public void OnException(ExceptionContext context)
        {
            var json = new JsonErrorResponse
            {
                Message = context.Exception.Message
            };
            if(_env.IsDevelopment()){
                json.DeveloperMessage=context.Exception.StackTrace;
            }
            if (context.Exception is UserOperationException)
            {
                context.Result = new BadRequestObjectResult(json);
            }
            else
            {
                json.Message="系统繁忙";
                context.Result = new InternalServerErrorObjectResult(json);
            }
            _logger.LogError(context.Exception,context.Exception.Message);
            context.ExceptionHandled=true;
        }
    }

    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult(object error) : base(error)
        {
            base.StatusCode = StatusCodes.Status500InternalServerError;
        }

    }
}
