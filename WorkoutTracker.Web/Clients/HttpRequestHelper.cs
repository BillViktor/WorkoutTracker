using System.Net.Http.Json;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.Web.Clients
{
    public static class HttpRequestHelper
    {
        #region Get
        public static async Task<ResultModel<T>> GetAsync<T>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);

            var result = await response.Content.ReadFromJsonAsync<ResultModel<T>>();

            if (!response.IsSuccessStatusCode)
            {
                if(result == null)
                {
                    return new ResultModel<T>($"Non-success status code: {response.StatusCode}");
                }

                // If the result is not null, it may contain errors
                return new ResultModel<T>
                {
                    Errors = result.Errors,
                    Message = result.Message,
                    ResultObject = result.ResultObject,
                    Success = false
                };
            }

            return result;
        }
        #endregion


        #region Post
        public static async Task<ResultModel> PostAsync(this HttpClient httpClient, string url)
        {
            var response = await httpClient.PostAsync(url, null);

            var result = await response.Content.ReadFromJsonAsync<ResultModel>();

            if (!response.IsSuccessStatusCode)
            {
                if (result == null)
                {
                    return new ResultModel($"Non-success status code: {response.StatusCode}");
                }

                // If the result is not null, it may contain errors
                return new ResultModel
                {
                    Errors = result.Errors,
                    Message = result.Message,
                    Success = false
                };
            }

            return result;
        }

        public static async Task<ResultModel<T>> PostAsJsonAsync<T, U>(this HttpClient httpClient, string url, U aContent)
        {
            var response = await httpClient.PostAsJsonAsync(url, aContent);

            var result = await response.Content.ReadFromJsonAsync<ResultModel<T>>();

            if (!response.IsSuccessStatusCode)
            {
                if (result == null)
                {
                    return new ResultModel<T>($"Non-success status code: {response.StatusCode}");
                }

                // If the result is not null, it may contain errors
                return new ResultModel<T>
                {
                    Errors = result.Errors,
                    Message = result.Message,
                    ResultObject = result.ResultObject,
                    Success = false
                };
            }

            return result;
        }
        #endregion

        #region Delete
        public static async Task<ResultModel> DeleteAsync(this HttpClient httpClient, string url)
        {
            var response = await httpClient.DeleteAsync(url);

            var result = await response.Content.ReadFromJsonAsync<ResultModel>();

            if (!response.IsSuccessStatusCode)
            {
                if (result == null)
                {
                    return new ResultModel($"Non-success status code: {response.StatusCode}");
                }

                // If the result is not null, it may contain errors
                return new ResultModel
                {
                    Errors = result.Errors,
                    Message = result.Message,
                    Success = false
                };
            }

            return result;
        }
        #endregion
    }
}
