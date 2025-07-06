using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.RoutineDay;

namespace WorkoutTracker.Web.Clients.RoutineDayClient
{
    /// <summary>
    /// Client for the Routine Day API Controller.
    /// </summary>
    public class RoutineDayClient : IRoutineDayClient
    {
        private readonly HttpClient httpClient;

        public RoutineDayClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Adds a new routine day.
        /// </summary>
        public async Task<ResultModel<RoutineDayDto>> AddRoutineDay(AddRoutineDayDto day)
        {
            return await HttpRequestHelper.PostAsJsonAsync<RoutineDayDto, AddRoutineDayDto>(httpClient, $"routine-days", day);
        }

        /// <summary>
        /// Updates an existing routine day by its ID.
        /// </summary>
        public async Task<ResultModel<RoutineDayDto>> UpdateRoutineDay(long id, UpdateRoutineDayDto day)
        {
            return await HttpRequestHelper.PutAsJsonAsync<RoutineDayDto, UpdateRoutineDayDto>(httpClient, $"routine-days/{id}", day);
        }

        /// <summary>
        /// Deletes a routine day by its ID.
        /// </summary>
        public async Task<ResultModel> DeleteRoutineDay(long id)
        {
            return await HttpRequestHelper.DeleteAsync(httpClient, $"routine-days/{id}");
        }
    }
}
