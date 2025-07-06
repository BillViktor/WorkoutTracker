using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Routine;
using WorkoutTracker.Web.Clients.RoutineClient;
using WorkoutTracker.Web.ViewModels.EntityViewModel;

namespace WorkoutTracker.Web.ViewModels.RoutineViewModel
{
    public class RoutineViewModel : EntityViewModel<RoutineDto>, IRoutineViewModel
    {
        private readonly IRoutineClient routineClient;
        private EntityParameters parameters = new EntityParameters();

        public EntityParameters Parameters
        {
            get => parameters;
            set
            {
                SetValue(ref parameters, value);
            }
        }

        public RoutineViewModel(IRoutineClient routineClient)
        {
            this.routineClient = routineClient;
        }

        #region Methods
        /// <summary>
        /// Retrieves a list of routines from the database based on the provided parameters.
        /// </summary>
        public override async Task GetEntities()
        {
            Entities = await ResultHandler.HandleAsync(
                routineClient.GetRoutines(parameters),
                AppendErrorList,
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Retrieves a specific routine by its ID from the database.
        /// </summary>
        public async Task<RoutineDto> GetRoutine(long id)
        {
            return await ResultHandler.HandleAsync(
                routineClient.GetRoutine(id),
                AppendErrorList,
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Adds a new routine to the database.
        /// </summary>
        public async Task<RoutineDto> Add(AddRoutineDto routine)
        {
            return await ResultHandler.HandleAsync(
                routineClient.AddRoutine(routine),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Routine successfully created.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Deletes a routine.
        /// </summary>
        public async Task<bool> Delete(RoutineDto routine)
        {
            return await ResultHandler.HandleAsync(
                routineClient.DeleteRoutine(routine.Id),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Routine successfully deleted.",
                setBusy: busy => IsBusy = busy);
        }

        /// <summary>
        /// Updates an existing routine.
        /// </summary>
        public async Task<RoutineDto> Update(long id, UpdateRoutineDto routine)
        {
            return await ResultHandler.HandleAsync(
                routineClient.UpdateRoutine(id, routine),
                AppendErrorList,
                handleSuccessMessage: AddSuccessMessage,
                messageOnSuccess: "Routine successfully updated.",
                setBusy: busy => IsBusy = busy);
        }
        #endregion
    }
}
