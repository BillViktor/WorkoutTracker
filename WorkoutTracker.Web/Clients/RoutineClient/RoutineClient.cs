using Microsoft.AspNetCore.WebUtilities;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.Routine;

namespace WorkoutTracker.Web.Clients.RoutineClient
{
    /// <summary>
    /// Client for the Routine API Controller.
    /// </summary>
    public class RoutineClient : IRoutineClient
    {
        private readonly HttpClient httpClient;

        public RoutineClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Get a paginated, sorted and filtered list of routines.
        /// </summary>
        public async Task<ResultModel<EntityResult<RoutineDto>>> GetRoutines(EntityParameters parameters)
        {
            var queryString = new Dictionary<string, string>
            {
                { "page", parameters.Page.ToString() },
                { "pageCount", parameters.PageCount.ToString() },
                { "searchString", parameters.SearchString },
            };

            return await HttpRequestHelper.GetAsync<EntityResult<RoutineDto>>(httpClient, QueryHelpers.AddQueryString("routines/list", queryString));
        }

        /// <summary>
        /// Get a routine by its ID.
        /// </summary>
        public async Task<ResultModel<RoutineDto>> GetRoutine(long id)
        {
            return await HttpRequestHelper.GetAsync<RoutineDto>(httpClient, $"routines/{id}");
        }

        /// <summary>
        /// Adds a new routine.
        /// </summary>
        public async Task<ResultModel<RoutineDto>> AddRoutine(AddRoutineDto routine)
        {
            return await HttpRequestHelper.PostAsJsonAsync<RoutineDto, AddRoutineDto>(httpClient, $"routines", routine);
        }

        /// <summary>
        /// Updates an existing routine by its ID.
        /// </summary>
        public async Task<ResultModel<RoutineDto>> UpdateRoutine(long id, UpdateRoutineDto routine)
        {
            return await HttpRequestHelper.PutAsJsonAsync<RoutineDto, UpdateRoutineDto>(httpClient, $"routines/{id}", routine);
        }

        /// <summary>
        /// Deletes a routine by its ID.
        /// </summary>
        public async Task<ResultModel> DeleteRoutine(long id)
        {
            return await HttpRequestHelper.DeleteAsync(httpClient, $"routines/{id}");
        }
    }
}
