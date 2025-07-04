using System.Net.Http.Json;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.Web.Clients
{
    /// <summary>
    /// Helper class for making HTTP requests and handling responses.
    /// </summary>
    public static class HttpRequestHelper
    {
        #region Get
        /// <summary>
        /// Performs a GET request to the specified URL and returns a ResultModel with a ResultObject.
        /// </summary>
        public static async Task<ResultModel<T>> GetAsync<T>(this HttpClient httpClient, string url)
        {
            try
            {
                var response = await httpClient.GetAsync(url);

                var result = await ReadFromJsonAsync<T>(response);

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
            catch(Exception ex)
            {
                return new ResultModel<T>(ex);
            }
            
        }
        #endregion


        #region Post
        /// <summary>
        /// Performs a POST request to the specified URL and returns a ResultModel.
        /// </summary>
        public static async Task<ResultModel> PostAsync(this HttpClient httpClient, string url)
        {
            try
            {
                var response = await httpClient.PostAsync(url, null);

                var result = await ReadFromJsonAsync(response);

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
            catch(Exception ex)
            {
                return new ResultModel(ex);
            }
        }

        /// <summary>
        /// Performs a POST request to the specified URL with a HttpContent and returns a ResultModel with a ResultObject.
        /// </summary>
        public static async Task<ResultModel<T>> PostAsync<T>(this HttpClient httpClient, string url, HttpContent content)
        {
            try
            {
                var response = await httpClient.PostAsync(url, content);

                var result = await ReadFromJsonAsync<T>(response);

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
                        Success = false
                    };
                }

                return result;
            }
            catch (Exception ex)
            {
                return new ResultModel<T>(ex);
            }
        }

        /// <summary>
        /// Performs a POST request to the specified URL with a JSON content and returns a ResultModel with a ResultObject.
        /// </summary>
        public static async Task<ResultModel<T>> PostAsJsonAsync<T, U>(this HttpClient httpClient, string url, U content)
        {
            try
            {
                var response = await HttpClientJsonExtensions.PostAsJsonAsync(httpClient, url, content);

                var result = await ReadFromJsonAsync<T>(response);

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
            catch (Exception ex)
            {
                return new ResultModel<T>(ex);
            }
        }

        /// <summary>
        /// Performs a POST request to the specified URL with a JSON content and returns a ResultModel.
        /// </summary>
        public static async Task<ResultModel> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T content)
        {
            try
            {
                var response = await HttpClientJsonExtensions.PostAsJsonAsync(httpClient, url, content);

                var result = await ReadFromJsonAsync<ResultModel>(response);

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
            catch (Exception ex)
            {
                return new ResultModel<T>(ex);
            }
        }
        #endregion


        #region Put
        /// <summary>
        /// Performs a PUT request to the specified URL and returns a ResultModel with a ResultObject.
        /// </summary>
        public static async Task<ResultModel<T>> PutAsJsonAsync<T, U>(this HttpClient httpClient, string url, U content)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync(url, content);

                var result = await ReadFromJsonAsync<T>(response);

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
            catch (Exception ex)
            {
                return new ResultModel<T>(ex);
            }
        }

        /// <summary>
        /// Performs a PUT request to the specified URL and returns a ResultModel with a ResultObject.
        /// </summary>
        public static async Task<ResultModel<T>> PutAsync<T, U>(this HttpClient httpClient, string url, HttpContent content)
        {
            try
            {
                var response = await httpClient.PutAsync(url, content);

                var result = await ReadFromJsonAsync<T>(response);

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
            catch (Exception ex)
            {
                return new ResultModel<T>(ex);
            }
        }
        #endregion


        #region Delete
        /// <summary>
        /// Performs a DELETE request to the specified URL and returns a ResultModel.
        /// </summary>
        public static async Task<ResultModel> DeleteAsync(this HttpClient httpClient, string url)
        {
            try
            {
                var response = await httpClient.DeleteAsync(url);

                var result = await ReadFromJsonAsync(response);

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
            catch (Exception ex)
            {
                return new ResultModel(ex);
            }
        }
        #endregion

        /// <summary>
        /// Reads the response message as a ResultModel from Json.
        /// </summary>
        private static async Task<ResultModel> ReadFromJsonAsync(HttpResponseMessage message)
        {
            try
            {
                return await message.Content.ReadFromJsonAsync<ResultModel>();
            }
            catch (Exception ex)
            {
                return new ResultModel($"Error reading response: {ex.Message}");
            }
        }

        /// <summary>
        /// Reads the response message as a ResultModel of type T from Json.
        /// </summary>
        private static async Task<ResultModel<T>> ReadFromJsonAsync<T>(HttpResponseMessage message)
        {
            try
            {
                return await message.Content.ReadFromJsonAsync<ResultModel<T>>();
            }
            catch (Exception ex)
            {
                return new ResultModel<T>($"Error reading response: {ex.Message}");
            }
        }
    }
}
