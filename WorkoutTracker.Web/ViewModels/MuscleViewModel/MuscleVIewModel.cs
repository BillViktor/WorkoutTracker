using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Web.Clients.MuscleClient;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.MuscleViewModel
{
    public class MuscleViewModel : EntityViewModel<Muscle>, IMuscleViewModel
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
        /// Retrieves a list of exercises from the database based on the provided parameters.
        /// </summary>
        public override async Task GetEntities()
        {
            Entities = await ResultHandler.HandleAsync(
                muscleClient.GetMuscles(EntityParameters),
                AppendErrorList);
        }

        /// <summary>
        /// Muscles can not be created.
        /// Therefore, this method is not implemented and will throw a NotImplementedException.
        /// </summary>
        public override async Task<Muscle> Add(Muscle muscle)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Muscles can not be deleted.
        /// Therefore, this method is not implemented and will throw a NotImplementedException.
        /// </summary>
        public override async Task<bool> Delete(Muscle muscle)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        public override async Task<Muscle> Update(Muscle muscle)
        {
            return await ResultHandler.HandleAsync(
                muscleClient.UpdateMuscle(muscle),
                AppendErrorList);
        }

        /// <summary>
        /// Retrieves a list of all muscles as dto from the database.
        /// </summary>
        public async Task GetMuscles()
        {
            muscles = await ResultHandler.HandleAsync(
                muscleClient.GetMuscles(),
                AppendErrorList);
        }
        #endregion
    }
}
