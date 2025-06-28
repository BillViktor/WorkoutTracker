using WorkoutTracker.API.Models.Exceptions;
using WorkoutTracker.Data.Models.Exceptions;
using WorkoutTracker.Shared.Dto.Result;

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
            catch(ArgumentOutOfRangeException ex)
            {
                logger.LogWarning(ex, "Argument out of range.");
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
            }
            catch (EmailAlreadyConfirmedException ex)
            {
                logger.LogWarning(ex, "Email already confirmed.");
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest);
            }
            catch (EntityNotFoundException ex)
            {
                logger.LogWarning(ex, "Entity not found.");
                await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound);
            }
            catch(UserNotFoundException ex)
            {
                logger.LogWarning(ex, "User not found.");
                await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception.");
                await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Converts the exception into a structured HTTP response.
        /// </summary>
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = new ResultModel(ex);

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}
