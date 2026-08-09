using BillMaxAPP.Helpers;
using BillMaxAPP.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BillMaxAPP.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
#if ANDROID
            var handler = new HttpClientHandler
            {
                // Development only
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            _client = new HttpClient(handler);
#else
            _client = new HttpClient();
#endif

            _client.Timeout = TimeSpan.FromSeconds(60);
        }

        public async Task<T?> PostAsync<T>(string url, object request)
        {
            try
            {
                var json = JsonSerializer.Serialize(request);

                var content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

                var response = await _client.PostAsync(
                    ApiRoutes.BaseUrl + url,
                    content);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    throw new Exception(
                        $"Status : {response.StatusCode}\n\n{error}");
                }

                var result = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(
                    result,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await _client.GetAsync(
                    ApiRoutes.BaseUrl + url);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    throw new Exception(
                        $"Status : {response.StatusCode}\n\n{error}");
                }

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                throw;
            }
        }

        public async Task<T?> GetAsync<T>(
    string url,
    Dictionary<string, string>? queryParams = null)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");

                if (!string.IsNullOrWhiteSpace(token))
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);
                }

                // Build query string
                if (queryParams != null && queryParams.Count > 0)
                {
                    var query = string.Join(
                        "&",
                        queryParams.Select(x =>
                            $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"));

                    url = $"{url}?{query}";
                }

                var response = await _client.GetAsync(
                    ApiRoutes.BaseUrl + url);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    throw new Exception(
                        $"Status : {response.StatusCode}\n\n{error}");
                }

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
            catch
            {
                throw;
            }
        }
    }
}