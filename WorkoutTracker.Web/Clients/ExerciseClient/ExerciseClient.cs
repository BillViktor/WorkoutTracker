using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.Web.Clients.ExerciseClient
{
    public class ExerciseClient : IExerciseClient
    {
        private readonly HttpClient httpClient;

        public ExerciseClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<EntityResult<Exercise>> GetExercises(EntityParameters entityParameters)
        {
            return await HttpRequestHelper.PostAsJsonAsync<EntityResult<Exercise>, EntityParameters>(httpClient, "Exercise", entityParameters);
        }

        public async Task<bool> DeleteExercise(long id)
        {
            return await HttpRequestHelper.DeleteAsync(httpClient, $"Exercise/{id}");
        }
    }
}
