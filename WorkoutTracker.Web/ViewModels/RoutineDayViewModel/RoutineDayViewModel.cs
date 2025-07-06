using WorkoutTracker.Shared.Dto.Routine;
using WorkoutTracker.Shared.Dto.RoutineDay;
using WorkoutTracker.Web.Clients.RoutineDayClient;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.RoutineDayViewModel
{
    public class RoutineDayViewModel : BaseViewModel, IRoutineDayViewModel
    {
        private readonly IRoutineDayClient routineDayClient;

        public RoutineDayViewModel(IRoutineDayClient routineDayClient)
        {
            this.routineDayClient = routineDayClient;
        }

        #region Methods
        /// <summary>
        /// Adds a new routine day to the database.
        /// </summary>
        public async Task<RoutineDayDto> Add(AddRoutineDayDto day)
        {
            return await ResultHandler.HandleAsync(
                routineDayClient.AddRoutineDay(day),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Day successfully added.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Deletes a routine day.
        /// </summary>
        public async Task<bool> Delete(RoutineDayDto day)
        {
            return await ResultHandler.HandleAsync(
                routineDayClient.DeleteRoutineDay(day.Id),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Day successfully deleted.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Updates an existing routine day.
        /// </summary>
        public async Task<RoutineDayDto> Update(long id, UpdateRoutineDayDto day)
        {
            return await ResultHandler.HandleAsync(
                routineDayClient.UpdateRoutineDay(id, day),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Day successfully updated.",
                setBusy: busy => IsBusy = busy);
        }
        #endregion
    }
}
