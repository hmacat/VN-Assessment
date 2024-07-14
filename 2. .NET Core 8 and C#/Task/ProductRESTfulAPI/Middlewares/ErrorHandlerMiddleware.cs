using System.Net;
using System.Text.Json;

namespace ProductRESTfulAPI.Middlewares
{
    /// <summary>
    /// Global exception handler middleware.
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger
        )
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var exceptionGuid = Guid.NewGuid();
                this.logger.LogError($"Unhandled exception. Detail: {ex}");

                var response = context.Response;
                response.ContentType = "application/json";

                // unhandled error
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = JsonSerializer.Serialize(
                    new
                    {
                        message = "Error occured. Please contact support.",
                    }
                );

                await response.WriteAsync(result);
            }
        }
    }
}
