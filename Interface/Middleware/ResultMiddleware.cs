using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

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

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);

        if (httpContext.Items.ContainsKey("Result"))
        {
            var result = (Result)httpContext.Items["Result"];

            if (result.IsSuccess)
            {
                _logger.LogInformation("Successfully handled the request.");
            }
            else
            {
                _logger.LogWarning($"Request failed: {result.Error}");
            }
        }
        else
        {
            _logger.LogWarning("No result was found in the response.");
        }
    }
}
}
