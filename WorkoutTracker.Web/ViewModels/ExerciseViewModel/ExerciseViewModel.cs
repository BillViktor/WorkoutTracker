using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.Clients.ExerciseClient;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    /// <summary>
    /// ViewModel for managing exercises in the Workout Tracker application.
    /// </summary>
    public class ExerciseViewModel : EntityViewModel<ExerciseDto>, IExerciseViewModel
    {
        private readonly IExerciseClient exerciseClient;

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
        public override async Task<ExerciseDto> Add(ExerciseDto exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.AddExercise(exercise),
                AppendErrorList,
                message => SuccessMessages.Add(message), "Exercise added successfully");
        }

        /// <summary>
        /// Deletes an exercise.
        /// </summary>
        public override async Task<bool> Delete(ExerciseDto exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.DeleteExercise(exercise.Id),
                AppendErrorList,
                message => SuccessMessages.Add(message), "Exercise deleted successfully");
        }

        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        public override async Task<ExerciseDto> Update(ExerciseDto exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.UpdateExercise(exercise),
                AppendErrorList,
                message => SuccessMessages.Add(message), "Exercise updated successfully");
        }
        #endregion
    }
}
