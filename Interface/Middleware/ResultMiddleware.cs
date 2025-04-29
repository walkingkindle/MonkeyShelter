using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Infrastructure.Middleware
{
public class ResultMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResultMiddleware> _logger;

    public ResultMiddleware(RequestDelegate next, ILogger<ResultMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var stopwatch = Stopwatch.StartNew();

        // Log request information
        _logger.LogInformation("Incoming request: {method} {path}", httpContext.Request.Method, httpContext.Request.Path);

        await _next(httpContext); // Call next middleware

        stopwatch.Stop();

        // Log Result if available
        if (httpContext.Items.TryGetValue("Result", out var resultObj) && resultObj is Result result)
        {
            if (result.IsSuccess)
            {
                _logger.LogInformation("Request succeeded in {duration} ms", stopwatch.ElapsedMilliseconds);
            }
            else
            {
                _logger.LogWarning("Request failed in {duration} ms. Error: {error}", stopwatch.ElapsedMilliseconds, result.Error);
            }
        }
        else
        {
            _logger.LogWarning("Request completed in {duration} ms with no Result object in context", stopwatch.ElapsedMilliseconds);
        }

        // Log response status
        _logger.LogInformation("Response Status: {statusCode}", httpContext.Response.StatusCode);
    }
}
}
}
