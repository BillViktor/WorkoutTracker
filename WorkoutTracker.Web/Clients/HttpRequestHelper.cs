using System.Net.Http.Json;

namespace WorkoutTracker.Web.Clients
{
    public static class HttpRequestHelper
    {
        #region Get
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion


        #region Post
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.PostAsync(url, null);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PostAsJsonAsync<T, U>(this HttpClient httpClient, string url, U aContent)
        {
            var response = await httpClient.PostAsJsonAsync(url, aContent);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion


        #region Put
        public static async Task<T> PutAsync<T>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.PutAsync(url, null);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PutAsJsonAsync<T, U>(this HttpClient httpClient, string url, U aContent)
        {
            var response = await httpClient.PutAsJsonAsync(url, aContent);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion


        #region Delete
        public static async Task<bool> DeleteAsync(this HttpClient httpClient, string url)
        {
            var response = await httpClient.DeleteAsync(url);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        #endregion
    }
}
