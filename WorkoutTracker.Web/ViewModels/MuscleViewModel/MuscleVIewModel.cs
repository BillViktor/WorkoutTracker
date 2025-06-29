using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.Clients.MuscleClient;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.MuscleViewModel
{
    /// <summary>
    /// ViewModel for managing muscle data, including retrieval and updates.
    /// </summary>
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
        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        public async Task<MuscleDto> Update(MuscleDto muscle)
        {
            return await ResultHandler.HandleAsync(
                muscleClient.UpdateMuscle(muscle),
                AppendErrorList,
                handleSuccessMessage: SuccessMessages.Add,
                messageOnSuccess: "Muscle updated successfully.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Retrieves a list of all muscles as dto from the database.
        /// </summary>
        public async Task GetMuscles()
        {
            muscles = await ResultHandler.HandleAsync(
                muscleClient.GetMuscles(),
                AppendErrorList,
                setBusy: busy => IsBusy = busy);
        }
        #endregion
    }
}
