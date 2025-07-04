using WorkoutTracker.Shared.Dto.Exercise;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Web.Clients.ExerciseClient;
using WorkoutTracker.Web.Models;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    /// <summary>
    /// ViewModel for managing exercises in the Workout Tracker application.
    /// </summary>
    public class ExerciseViewModel : EntityViewModel<ExerciseDto>, IExerciseViewModel
    {
        private readonly IExerciseClient exerciseClient;
        private ExerciseParameters exerciseParameters = new ExerciseParameters();

        public ExerciseParameters ExerciseParameters
        {
            get => exerciseParameters;
            set
            {
                SetValue(ref exerciseParameters, value);
            }
        }

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
                exerciseClient.GetExercises(exerciseParameters),
                AppendErrorList,
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Retrieves a specific exercise by its ID from the database.
        /// </summary>
        public async Task<ExerciseDto> GetExercise(long id)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.GetExercise(id),
                AppendErrorList,
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Adds a new exercise to the database.
        /// </summary>
        public async Task<ExerciseDto> Add(AddExerciseClientDto exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.AddExercise(exercise),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Exercise successfully created.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Deletes an exercise.
        /// </summary>
        public async Task<bool> Delete(ExerciseDto exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.DeleteExercise(exercise.Id),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Exercise successfully deleted.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        public async Task<ExerciseDto> Update(long id, UpdateExerciseClientDto exercise)
        {
            return await ResultHandler.HandleAsync(
                exerciseClient.UpdateExercise(id, exercise),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Exercise successfully updated.",
                setBusy: busy => IsBusy = busy);
        }
        #endregion
    }
}
