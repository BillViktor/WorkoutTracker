using System.ComponentModel;
using System.Runtime.CompilerServices;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.ViewModels.Base
{
    /// <summary>
    /// BaseViewModel serves as a foundational class for view models in the application.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged, IBaseViewModel
    {
        //Fields
        private bool isBusy = false;
        private List<ErrorModel> errors = new List<ErrorModel>();
        private List<string> successMessages = new List<string>();

        //Properties
        public bool IsBusy { get { return isBusy; } set { SetValue(ref isBusy, value); } }
        public List<ErrorModel> Errors
        {
            get { return errors; }
            set
            {
                if (value != null)
                {
                    SetValue(ref errors, value);
                }
            }
        }

        public List<string> SuccessMessages
        {
            get { return successMessages; }
            set
            {
                if (value != null)
                {
                    SetValue(ref successMessages, value);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Methods
        /// <summary>
        /// Raises the PropertyChanged event for the specified property name.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the value of a backing field and raises the PropertyChanged event if the value has changed.
        /// </summary>
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
            backingField = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Appends a list of errors to the existing error list.
        /// </summary>
        public void AppendErrorList(List<ErrorModel> errorList)
        {
            errors.AddRange(errorList);
        }

        /// <summary>
        /// Adds a single error to the list of errors.
        /// </summary>
        public void AddError(string error)
        {
            errors.Add(new ErrorModel { ErrorText = error });
        }

        /// <summary>
        /// Adds a success message to the list of success messages.
        /// </summary>
        public void AddSuccessMessage(string message)
        {
            successMessages.Add(message);
        }
        #endregion
    }
}
