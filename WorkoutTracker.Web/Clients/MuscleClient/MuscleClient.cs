using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

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
        /// Get a paginated, sorted and filtered list of exercises.
        /// </summary>
        public async Task<ResultModel<EntityResult<Muscle>>> GetMuscles(EntityParameters entityParameters)
        {
            return await HttpRequestHelper.PostAsJsonAsync<EntityResult<Muscle>, EntityParameters>(httpClient, "muscle/list", entityParameters);
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
        public async Task<ResultModel<Muscle>> UpdateMuscle(Muscle muscle)
        {
            return await HttpRequestHelper.PutAsJsonAsync<Muscle, Muscle>(httpClient, "", muscle);

        }
    }
}
