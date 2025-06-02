using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.Clients
{
    public class ExerciseClient : IExerciseClient
    {
        private readonly HttpClient mHttpClient;

        public ExerciseClient(HttpClient aHttpClient)
        {
            mHttpClient = aHttpClient;
        }

        public async Task<List<MuscleDto>> GetMuscles()
        {
            return await HttpRequestHelper.GetAsync<List<MuscleDto>>(mHttpClient, "Muscle");
        }
    }
}
