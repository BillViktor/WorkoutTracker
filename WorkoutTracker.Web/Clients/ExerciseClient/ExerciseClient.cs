using Microsoft.AspNetCore.WebUtilities;
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
        public async Task<ResultModel<EntityResult<ExerciseDto>>> GetExercises(ExerciseParameters parameters)
        {
            var queryString = new Dictionary<string, string>
            {
                { "page", parameters.Page.ToString() },
                { "pageCount", parameters.PageCount.ToString() },
                { "exerciseName", parameters.ExerciseName },
                { "primaryMuscle", parameters.PrimaryMuscle }
            };

            return await HttpRequestHelper.GetAsync<EntityResult<ExerciseDto>>(httpClient, QueryHelpers.AddQueryString("exercise/list", queryString));
        }

        /// <summary>
        /// Get an exercise by its ID.
        /// </summary>
        public async Task<ResultModel<ExerciseDto>> GetExercise(long id)
        {
            return await HttpRequestHelper.GetAsync<ExerciseDto>(httpClient, $"exercise/{id}");
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
