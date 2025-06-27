using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    /// <summary>
    /// Client for the Exercise API Controller.
    /// </summary>
    public class ExerciseClient : IExerciseClient
    {
        private readonly HttpClient httpClient;

        public ExerciseClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Get a paginated, sorted and filtered list of exercises.
        /// </summary>
        public async Task<ResultModel<EntityResult<Exercise>>> GetExercises(EntityParameters entityParameters)
        {
            return await HttpRequestHelper.PostAsJsonAsync<EntityResult<Exercise>, EntityParameters>(httpClient, "", entityParameters);
        }

        /// <summary>
        /// Delete an exercise by its ID.
        /// </summary>
        public async Task<ResultModel> DeleteExercise(long id)
        {
            return await HttpRequestHelper.DeleteAsync(httpClient, $"exercise/{id}");
        }
    }
}
