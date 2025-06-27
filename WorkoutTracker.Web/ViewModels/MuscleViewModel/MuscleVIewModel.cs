using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.Clients.Muscle;

namespace WorkoutTracker.Web.ViewModels.Muscle
{
    public class MuscleViewModel : IMuscleViewModel
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
            var result = await muscleClient.GetMuscles();

            if (result.Success)
            {
                muscles = result.ResultObject;
            }
        }
        #endregion
    }
}
