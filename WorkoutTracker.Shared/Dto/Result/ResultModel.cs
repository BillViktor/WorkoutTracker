namespace WorkoutTracker.Shared.Dto.Result
{
    /// <summary>
    /// Represents a result model used to encapsulate the outcome of an operation, including success status, messages, and errors.
    /// </summary>
    public class ResultModel
    {
        public string? Message { get; set; } = null;
        public bool Success { get; set; } = true;
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        #region Constructors
        public ResultModel() { }

        public ResultModel(string errorText)
        {
            Success = false;
            AddError(errorText);
        }

        public ResultModel(List<string> errors)
        {
            Success = false;
            AppendErrors(errors);
        }

        public ResultModel(Exception ex)
        {
            Success = false;
            AddExceptionError(ex);
        }
        #endregion

        #region Errors
        /// <summary>
        /// Adds an error text to the errors list
        /// </summary>
        public void AddError(string errorText)
        {
            Errors.Add(new ErrorModel { ErrorText = errorText });
        }

        public void AddExceptionError(Exception ex)
        {
            Errors.Add(new ErrorModel
            {
                ErrorText = ex.Message,
                ExceptionDetails = ex.Source ?? "No source available",
                ExceptionStackTrace = ex.StackTrace ?? "No stack trace available"
            });
        }

        /// <summary>
        /// Appends a list of errors to the errors list
        /// </summary>
        public void AppendErrors(List<ErrorModel> errorModelList)
        {
            Errors.AddRange(errorModelList);
        }

        /// <summary>
        /// Adds a list of strings to the errors list
        /// </summary>
        public void AppendErrors(List<string> errorStringList)
        {
            foreach (var error in errorStringList)
            {
                Errors.Add(new ErrorModel { ErrorText = error });
            }
        }
        #endregion
    }

    /// <summary>
    /// ResultModel with a generic type for the result object.
    /// </summary>
    public class ResultModel<T> : ResultModel
    {
        public T? ResultObject { get; set; } = default;


        #region Constructors
        public ResultModel() { }

        public ResultModel(bool success)
        {
            Success = success;
        }

        public ResultModel(string errorText)
        {
            Success = false;
            AddError(errorText);
        }

        public ResultModel(List<string> errors)
        {
            Success = false;
            AppendErrors(errors);
        }

        public ResultModel(Exception ex)
        {
            Success = false;
            AddExceptionError(ex);
        }
        #endregion
    }
}
