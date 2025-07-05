using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Web.Models;

namespace WorkoutTracker.Web.Clients.MuscleClient
{
    /// <summary>
    /// Client for interacting with the Muscle API.
    /// </summary>
    public class MuscleClient : IMuscleClient
    {
        private readonly HttpClient httpClient;

        public MuscleClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Returns a list of all muscles.
        /// </summary>
        public async Task<ResultModel<List<MuscleDto>>> GetMuscles()
        {
            return await HttpRequestHelper.GetAsync<List<MuscleDto>>(httpClient, "");
        }

        /// <summary>
        /// Update an existing muscle in the database.
        /// </summary>
        public async Task<ResultModel<MuscleDto>> UpdateMuscle(long id, UpdateMuscleClientDto muscle)
        {
            var content = new MultipartFormDataContent
            {
                { new StringContent(muscle.Description.ToString()), "Description" },
                { new StringContent(muscle.DeleteImage.ToString()), "DeleteImage" }
            };

            if(muscle.Image != null)
            {
                var fileContent = new StreamContent(muscle.Image.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(muscle.Image.ContentType);
                content.Add(fileContent, "Image", muscle.Image.Name);
            }

            return await HttpRequestHelper.PutAsync<MuscleDto, MultipartFormDataContent>(httpClient, $"muscles/{id}", content);
        }
    }
}
