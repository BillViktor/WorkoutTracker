using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.Clients;

namespace WorkoutTracker.Web.ViewModels
{
    public class ExerciseViewModel : IExerciseViewModel
    {
        private readonly IExerciseClient mMuscleClient;

        //Fields
        private List<MuscleDto> mMuscles;

        //Properties
        public List<MuscleDto> Muscles { get { return mMuscles; } set { mMuscles = value; } }

        //Constructor
        public ExerciseViewModel(IExerciseClient aMuscleClient)
        {
            mMuscleClient = aMuscleClient;
        }

        #region Methods
        public async Task GetMuscles()
        {
            mMuscles = await mMuscleClient.GetMuscles();
        }
        #endregion
    }
}
