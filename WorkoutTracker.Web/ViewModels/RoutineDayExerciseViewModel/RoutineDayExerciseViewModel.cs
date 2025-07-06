using WorkoutTracker.Shared.Dto.Routine;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;
using WorkoutTracker.Web.Clients.RoutineDayExerciseClient;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.RoutineDayExerciseViewModel
{
    public class RoutineDayExerciseViewModel : BaseViewModel, IRoutineDayExerciseViewModel
    {
        private readonly IRoutineDayExerciseClient routineDayExerciseClient;

        public RoutineDayExerciseViewModel(IRoutineDayExerciseClient routineDayExerciseClient)
        {
            this.routineDayExerciseClient = routineDayExerciseClient;
        }

        #region Methods
        /// <summary>
        /// Adds a new routine day exercise to the database.
        /// </summary>
        public async Task<RoutineDayExerciseDto> Add(AddRoutineDayExerciseDto exercise)
        {
            return await ResultHandler.HandleAsync(
                routineDayExerciseClient.AddRoutineDayExercise(exercise),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Day successfully added.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Deletes a routine day exercise.
        /// </summary>
        public async Task<bool> Delete(RoutineDayExerciseDto day)
        {
            return await ResultHandler.HandleAsync(
                routineDayExerciseClient.DeleteRoutineDayExercise(day.Id),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Day successfully deleted.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Updates an existing routine day exercise.
        /// </summary>
        public async Task<RoutineDayExerciseDto> Update(long id, UpdateRoutineDayExerciseDto exercise)
        {
            return await ResultHandler.HandleAsync(
                routineDayExerciseClient.UpdateRoutineDayExercise(id, exercise),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Day successfully updated.",
                setBusy: busy => IsBusy = busy);
        }
        #endregion
    }
}
