using WorkoutTracker.Shared.Models;
using WorkoutTracker.Web.Clients.ExerciseClient;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    public class ExerciseViewModel : EntityViewModel<Exercise>, IExerciseViewModel
    {
        private readonly IExerciseClient exerciseClient;

        //Constructor
        public ExerciseViewModel(IExerciseClient exerciseClient)
        {
            this.exerciseClient = exerciseClient;
        }

        #region Methods
        /// <summary>
        /// Retrieves a list of exercises from the database based on the provided parameters.
        /// </summary>
        public override async Task GetEntities()
        {
            Entities = await ResultHandler.HandleAsync(
                exerciseClient.GetExercises(EntityParameters),
                AppendErrorList);
        }

        /// <summary>
        /// Adds a new exercise to the database.
        /// </summary>
        public override async Task<Exercise> Add(Exercise exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.AddExercise(exercise),
                AppendErrorList);
        }

        /// <summary>
        /// Deletes an exercise.
        /// </summary>
        public override async Task<bool> Delete(Exercise exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.DeleteExercise(exercise.Id),
                AppendErrorList);
        }

        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        public override async Task<Exercise> Update(Exercise exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.UpdateExercise(exercise),
                AppendErrorList);
        }
        #endregion
    }
}
