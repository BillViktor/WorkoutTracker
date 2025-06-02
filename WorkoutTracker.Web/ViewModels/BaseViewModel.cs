namespace WorkoutTracker.Web.ViewModels
{
    public class BaseViewModel : IBaseViewModel
    {
        //Fields
        private bool mIsBusy = false;

        //Properties
        public bool IsBusy { get { return mIsBusy; } set { mIsBusy = value; } }
    }
}
