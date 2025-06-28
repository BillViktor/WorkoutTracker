using System.ComponentModel;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.ViewModels.Base
{
    /// <summary>
    /// IBaseViewModel defines the basic contract for view models in the application.
    /// </summary>
    public interface IBaseViewModel
    {
        bool IsBusy { get; set; }
        List<ErrorModel> Errors { get; set; }
        List<string> SuccessMessages { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void AddError(string error);
        void AddSuccessMessage(string message);
        void AppendErrorList(List<ErrorModel> errorList);
    }
}