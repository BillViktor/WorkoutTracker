using System.Net.Http.Json;

namespace WorkoutTracker.Web.Clients
{
    public static class HttpRequestHelper
    {
        #region Get
        public static async Task<T> GetAsync<T>(this HttpClient aHttpClient, string aUrl)
        {
            var response = await aHttpClient.GetAsync(aUrl);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion


        #region Post
        public static async Task<T> PostAsync<T>(this HttpClient aHttpCleint, string aUrl)
        {
            var response = await aHttpCleint.PostAsync(aUrl, null);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PostAsJsonAsync<T, U>(this HttpClient aHttpCleint, string aUrl, U aContent)
        {
            var response = await aHttpCleint.PostAsJsonAsync(aUrl, aContent);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion


        #region Put
        public static async Task<T> PutAsync<T>(this HttpClient aHttpCleint, string aUrl)
        {
            var response = await aHttpCleint.PutAsync(aUrl, null);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PutAsJsonAsync<T, U>(this HttpClient aHttpCleint, string aUrl, U aContent)
        {
            var response = await aHttpCleint.PutAsJsonAsync(aUrl, aContent);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion


        #region Delete
        public static async Task<T> DeleteAsync<T>(this HttpClient aHttpCleint, string aUrl)
        {
            var response = await aHttpCleint.DeleteAsync(aUrl);
            if (!response.IsSuccessStatusCode) return default;
            return await response.Content.ReadFromJsonAsync<T>();
        }
        #endregion
    }
}
