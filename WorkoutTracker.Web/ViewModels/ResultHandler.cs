using WorkoutTracker.Shared.Dto.Result;

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
            Action<List<ErrorModel>> handleErrors,
            Action<string>? handleSuccessMessage = null,
            string? messageOnSuccess = null,
            Action<bool>? setBusy = null)
        {
            setBusy?.Invoke(true);

            try
            {
                var result = await resultTask;

                if (result.Success)
                {
                    if (!string.IsNullOrEmpty(messageOnSuccess))
                    {
                        handleSuccessMessage?.Invoke(messageOnSuccess);
                    }
                    return result.ResultObject;
                }

                handleErrors(result.Errors);
                return default;
            }
            finally
            {
                setBusy?.Invoke(false);
            }
        }

        /// <summary>
        /// Handles the result of an asynchronous operation that returns a ResultModel,
        /// optionally managing success messages and a busy state.
        /// </summary>
        public static async Task<bool> HandleAsync(
            Task<ResultModel> resultTask,
            Action<List<ErrorModel>> handleErrors,
            Action<string>? handleSuccessMessage = null,
            string? messageOnSuccess = null,
            Action<bool>? setBusy = null)
        {
            setBusy?.Invoke(true);

            try
            {
                var result = await resultTask;

                if (!result.Success)
                {
                    handleErrors(result.Errors);
                    return false;
                }

                if (!string.IsNullOrEmpty(messageOnSuccess))
                {
                    handleSuccessMessage?.Invoke(messageOnSuccess);
                }

                return true;
            }
            finally
            {
                setBusy?.Invoke(false);
            }
        }
    }
}
