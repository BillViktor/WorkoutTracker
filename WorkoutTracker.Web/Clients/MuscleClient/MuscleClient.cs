using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients.MuscleClient
{
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
        public async Task<ResultModel<MuscleDto>> UpdateMuscle(MuscleDto muscle)
        {
            return await HttpRequestHelper.PutAsJsonAsync<MuscleDto, MuscleDto>(httpClient, "", muscle);

        }
    }
}
