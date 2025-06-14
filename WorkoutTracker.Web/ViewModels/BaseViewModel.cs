namespace WorkoutTracker.Web.ViewModels
{
    public class BaseViewModel : IBaseViewModel
    {
        //Fields
        private bool isBusy = false;

        //Properties
        public bool IsBusy { get { return isBusy; } set { isBusy = value; } }
    }
}
