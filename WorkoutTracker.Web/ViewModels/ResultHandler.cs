using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.ViewModels
{
    /// <summary>
    /// Handles the results of asynchronous operations, processing errors if they occur.
    /// </summary>
    public static class ResultHandler
    {
        /// <summary>
        /// Handles the result of an asynchronous operation that returns a ResultModel with a ResultObject.
        /// </summary>
        public static async Task<T?> HandleAsync<T>(
        Task<ResultModel<T>> resultTask,
        Action<List<ErrorModel>> handleErrors)
        {
            var result = await resultTask;

            if (result.Success)
            {
                return result.ResultObject;
            }

            handleErrors(result.Errors);
            return default;
        }

        /// <summary>
        /// Handles the result of an asynchronous operation that returns a ResultModel.
        /// </summary>
        public static async Task<bool> HandleAsync(
        Task<ResultModel> resultTask,
        Action<List<ErrorModel>> handleErrors)
        {
            var result = await resultTask;

            if(!result.Success)
            {
                handleErrors(result.Errors);
                return false;
            }

            return true;
        }
    }
}
