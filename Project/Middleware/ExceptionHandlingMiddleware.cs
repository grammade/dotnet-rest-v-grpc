using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILog _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
        _logger = LogManager.GetLogger(typeof(ExceptionHandlingMiddleware));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error("An unhandled exception occurred.", ex);
            throw;
        }
    }
}
