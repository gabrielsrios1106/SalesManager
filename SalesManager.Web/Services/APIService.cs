using MudBlazor;
using SalesManager.Web.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace SalesManager.Web.Services
{
    public class APIService : IAPIService
    {
        private readonly ISnackbar SnackbarService;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;

        public APIService(ISnackbar snackbarService, IHttpClientFactory httpClientFactory)
        {
            SnackbarService = snackbarService;
            _httpClientFactory = httpClientFactory;

            _httpClient = _httpClientFactory.CreateClient("APIRestful");
        }

        public async Task<List<T>> GetListAsync<T>(string requestUri)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestUri);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<List<T>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    SnackbarService.Add(responseContent, Severity.Error);
                    return default;
                }
            }
            catch (Exception e)
            {
                SnackbarService.Add($"Houve um erro ao acessar a API - {e.Message}", Severity.Error);
                return default;
            }
        }

        public async Task<T> GetByIdAsync<T>(string requestUri)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestUri);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                else
                {
                    SnackbarService.Add(responseContent, Severity.Error);
                    return default;
                }
            }
            catch (Exception e)
            {
                SnackbarService.Add($"Houve um erro ao acessar a API - {e.Message}", Severity.Error);
                return default;
            }
        }

        public async Task<bool> CreateAsync<T>(string requestUri, T value)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(requestUri, value);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    SnackbarService.Add(responseContent, Severity.Error);
                    return false;
                }
            }
            catch (Exception e)
            {
                SnackbarService.Add($"Houve um erro ao acessar a API - {e.Message}", Severity.Error);
                return false;
            }
        }

        public async Task<bool> UpdateAsync<T>(string requestUri, T value)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync(requestUri, value);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    SnackbarService.Add(responseContent, Severity.Error);
                    return false;
                }
            }
            catch (Exception e)
            {
                SnackbarService.Add($"Houve um erro ao acessar a API - {e.Message}", Severity.Error);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string requestUri)
        {
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(requestUri);
                string responseContent = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    SnackbarService.Add(responseContent, Severity.Error);
                    return false;
                }
            }
            catch (Exception e)
            {
                SnackbarService.Add($"Houve um erro ao acessar a API - {e.Message}", Severity.Error);
                return false;
            }
        }
    }
}
