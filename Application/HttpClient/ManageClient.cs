using Application.Client;
using Common.Responses;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.HttpClientService
{
    public class ManageClient(IHttpClientFactory httpClient) : IManageClient
    {
        private readonly HttpClient _httpClient = httpClient.CreateClient();
        public async Task<ResponseDto<object>> ReadClientJsonAsync(string? Url)
        {

            if (string.IsNullOrEmpty(Url)) return new ResponseDto<object>() { IsSuccess = false, Message = $"El URL no debe de estar vacio", StatusCode = System.Net.HttpStatusCode.BadRequest, };

            HttpResponseMessage response = await _httpClient.GetAsync(Url);

            List<ClientResponse>? resultList = JsonSerializer.Deserialize<List<ClientResponse>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new ResponseDto<object>()
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = $"La Respuesta del URL Externo es {response.ReasonPhrase}",
                StatusCode = response.StatusCode,
                Data = resultList
            };


        }
    }
}
