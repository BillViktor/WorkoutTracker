using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.Clients.Muscle;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.Muscle
{
    public class MuscleViewModel : BaseViewModel, IMuscleViewModel
    {
        private readonly IMuscleClient muscleClient;

        //Fields
        private List<MuscleDto> muscles;

        //Properties
        public List<MuscleDto> Muscles { get { return muscles; } set { muscles = value; } }

        //Constructor
        public MuscleViewModel(IMuscleClient muscleClient)
        {
            this.muscleClient = muscleClient;
        }

        #region Methods
        public async Task GetMuscles()
        {
            muscles = await ResultHandler.HandleAsync(
                muscleClient.GetMuscles(),
                AppendErrorList);
        }
        #endregion
    }
}
