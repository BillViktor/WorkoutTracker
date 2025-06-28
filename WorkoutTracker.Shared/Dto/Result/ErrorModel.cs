namespace WorkoutTracker.Shared.Dto.Result
{
    public class ErrorModel
    {
        public string ErrorText { get; set; }
        public string ExceptionDetails { get; set; }
        public string ExceptionStackTrace { get; set; }

        #region Constructors
        public ErrorModel() { }

        public ErrorModel(string errorText)
        {
            ErrorText = errorText;
        }
        #endregion
    }
}
