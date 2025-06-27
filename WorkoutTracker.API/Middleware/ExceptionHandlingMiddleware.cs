using WorkoutTracker.Data.Models.Exceptions;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.API.Middleware
{
    /// <summary>
    /// Middleware to handle exceptions globally in the API.
    /// It converts exceptions into appropriate HTTP responses.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        /// <summary>
        /// Main method that gets called when the middleware is invoked.
        /// It wraps the request in a try-catch block to intercept exceptions.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context); // Pass control to next middleware
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogWarning(ex, "Entity not found.");
                await HandleExceptionAsync(context, ex.Message, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception.");
                await HandleExceptionAsync(context, "An unexpected error occurred.", StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Converts the exception into a structured HTTP response.
        /// </summary>
        private static async Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = new ResultModel(new List<string> { message });

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
