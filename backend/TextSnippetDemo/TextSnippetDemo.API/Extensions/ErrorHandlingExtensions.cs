using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Constants;

namespace TextSnippetDemo.API.Extensions
{
    public class HttpExceptionGlobalFilter : IExceptionFilter
    {
        private readonly ILogger<HttpExceptionGlobalFilter> _logger;

        public HttpExceptionGlobalFilter(ILogger<HttpExceptionGlobalFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            // This should be changed depend on what type of error is
            context.Result = new ObjectResult(ErrorResponseConstants.UnexpectedError);
        }
    }
}
