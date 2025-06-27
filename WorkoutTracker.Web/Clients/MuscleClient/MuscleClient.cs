using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients.Muscle
{
    public class MuscleClient : IMuscleClient
    {
        private readonly HttpClient httpClient;

        public MuscleClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResultModel<List<MuscleDto>>> GetMuscles()
        {
            return await HttpRequestHelper.GetAsync<List<MuscleDto>>(httpClient, "");
        }
    }
}
