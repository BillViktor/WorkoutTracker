using Microsoft.AspNetCore.WebUtilities;
using WorkoutTracker.Shared.Dto.Exercise;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Web.Models;

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
        public async Task<ResultModel<ExerciseDto>> AddExercise(AddExerciseClientDto exercise)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(exercise.Name), "Name" },
                { new StringContent(exercise.Description), "Description" },
                { new StringContent(exercise.PrimaryMuscleId.ToString()), "PrimaryMuscleId" },
            };

            if(exercise.Instructions != null)
            {
                content.Add(new StringContent(exercise.Instructions), "Instructions");
            }

            if (exercise.Image != null)
            {
                var fileContent = new StreamContent(exercise.Image.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(exercise.Image.ContentType);
                content.Add(fileContent, "Image", exercise.Image.Name);
            }

            return await HttpRequestHelper.PostAsync<ExerciseDto>(httpClient, $"", content);
        }

        /// <summary>
        /// Update an existing exercise in the database.
        /// </summary>
        public async Task<ResultModel<ExerciseDto>> UpdateExercise(long id, UpdateExerciseClientDto exercise)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(exercise.Name), "Name" },
                { new StringContent(exercise.Description), "Description" },
                { new StringContent(exercise.PrimaryMuscleId.ToString()), "PrimaryMuscleId" },
                { new StringContent(exercise.DeleteImage.ToString()), "DeleteImage" }
            };

            if (exercise.Instructions != null)
            {
                content.Add(new StringContent(exercise.Instructions), "Instructions");
            }

            if (exercise.Image != null)
            {
                var fileContent = new StreamContent(exercise.Image.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(exercise.Image.ContentType);
                content.Add(fileContent, "Image", exercise.Image.Name);
            }

            return await HttpRequestHelper.PutAsync<ExerciseDto, HttpContent>(httpClient, $"exercise/{id}", content);
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
