using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.Web.Clients.RoutineDayExerciseClient
{
    /// <summary>
    /// Client for the Routine Day API Controller.
    /// </summary>
    public class RoutineDayExerciseClient : IRoutineDayExerciseClient
    {
        private readonly HttpClient httpClient;

        public RoutineDayExerciseClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Adds a new routine day exercise.
        /// </summary>
        public async Task<ResultModel<RoutineDayExerciseDto>> AddRoutineDayExercise(AddRoutineDayExerciseDto exercise)
        {
            return await HttpRequestHelper.PostAsJsonAsync<RoutineDayExerciseDto, AddRoutineDayExerciseDto>(httpClient, $"routine-day-exercises", exercise);
        }

        /// <summary>
        /// Updates an existing routine day exercise by its ID.
        /// </summary>
        public async Task<ResultModel<RoutineDayExerciseDto>> UpdateRoutineDayExercise(long id, UpdateRoutineDayExerciseDto exercise)
        {
            return await HttpRequestHelper.PutAsJsonAsync<RoutineDayExerciseDto, UpdateRoutineDayExerciseDto>(httpClient, $"routine-day-exercises/{id}", exercise);
        }

        /// <summary>
        /// Deletes a routine day exercise by its ID.
        /// </summary>
        public async Task<ResultModel> DeleteRoutineDayExercise(long id)
        {
            return await HttpRequestHelper.DeleteAsync(httpClient, $"routine-day-exercises/{id}");
        }
    }
}
