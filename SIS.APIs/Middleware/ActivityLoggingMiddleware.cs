using SIS.Application.DTOs.Logging;
using SIS.Application.Services.Interfaces;

namespace SIS.APIs.Middleware
{
    public class ActivityLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ActivityLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IActivityLogClient activityLogClient)
        {
            await _next(context);

            if (context.Request.Path.StartsWithSegments("/openapi") ||
                context.Request.Path.StartsWithSegments("/scalar"))
            {
                return;
            }

            await activityLogClient.WriteAsync(new ActivityLogRequest
            {
                EventType = "HttpRequestCompleted",
                LogLevel = context.Response.StatusCode >= 500 ? "Error" : "Information",
                Message = $"{context.Request.Method} {context.Request.Path} completed with {context.Response.StatusCode}.",
                ResourceType = "HttpRequest",
                ResourceId = context.TraceIdentifier,
                CorrelationId = context.TraceIdentifier,
                CreatedBy = context.User.Identity?.Name ?? "anonymous"
            }, context.RequestAborted);
        }
    }
}
