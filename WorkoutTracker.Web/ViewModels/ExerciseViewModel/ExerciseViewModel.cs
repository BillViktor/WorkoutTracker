using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;
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
        public async Task<ExerciseDto> Add(ExerciseDto exercise)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an exercise.
        /// </summary>
        public async Task<bool> Delete(ExerciseDto exercise)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates an existing exercise.
        /// </summary>
        public async Task<ExerciseDto> Update(ExerciseDto exercise)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }
        #endregion
    }
}
