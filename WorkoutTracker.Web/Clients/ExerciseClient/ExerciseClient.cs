using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;

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
        public async Task<ResultModel<EntityResult<ExerciseDto>>> GetExercises(EntityParameters entityParameters)
        {
            return await HttpRequestHelper.PostAsJsonAsync<EntityResult<ExerciseDto>, EntityParameters>(httpClient, "exercise/list", entityParameters);
        }

        /// <summary>
        /// Add a new exercise to the database.
        /// </summary>
        public async Task<ResultModel<ExerciseDto>> AddExercise(ExerciseDto exercise)
        {
            return await HttpRequestHelper.PostAsJsonAsync<ExerciseDto, ExerciseDto>(httpClient, "", exercise);
        }

        /// <summary>
        /// Update an existing exercise in the database.
        /// </summary>
        public async Task<ResultModel<ExerciseDto>> UpdateExercise(ExerciseDto exercise)
        {
            return await HttpRequestHelper.PutAsJsonAsync<ExerciseDto, ExerciseDto>(httpClient, "", exercise);

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
