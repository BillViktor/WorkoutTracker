using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.Clients.Muscle
{
    public class MuscleClient : IMuscleClient
    {
        private readonly HttpClient mHttpClient;

        public MuscleClient(HttpClient httpClient)
        {
            mHttpClient = httpClient;
        }

        public async Task<List<MuscleDto>> GetMuscles()
        {
            return await mHttpClient.GetAsync<List<MuscleDto>>("Muscle");
        }
    }
}
