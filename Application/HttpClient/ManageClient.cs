using Application.Client;
using Common.AppSettingsConfig;
using Common.Dtos;
using Common.Responses;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.HttpClientService
{
    public class ManageClient(IHttpClientFactory httpClient, IOptions<General> options) : IManageClient
    {
        private readonly HttpClient _httpClient = httpClient.CreateClient();

        private readonly General _settings = options.Value;

        public async Task<ResponseDto<object>> ClientJsonAsync(JsonHolderDto post)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(_settings?.ExternalURL?.JsonHolder, post);


                ClientResponse resultList = JsonSerializer.Deserialize<ClientResponse>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

                return new ResponseDto<object>()
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = $"La Respuesta del URL Externo es {response.ReasonPhrase}",
                    StatusCode = response.StatusCode,
                    Data = resultList
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,

                };
            }

        }

        public async Task<ResponseDto<object>> ReadClientJsonAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_settings?.ExternalURL?.JsonHolder)) return new ResponseDto<object>() { IsSuccess = false, Message = $"El URL no debe de estar vacio", StatusCode = System.Net.HttpStatusCode.BadRequest, };

                HttpResponseMessage response = await _httpClient.GetAsync(_settings?.ExternalURL?.JsonHolder);

                List<ClientResponse>? resultList = JsonSerializer.Deserialize<List<ClientResponse>>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return new ResponseDto<object>()
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Message = $"La Respuesta del URL Externo es {response.ReasonPhrase}",
                    StatusCode = response.StatusCode,
                    Data = resultList
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,

                };
            }


        }
    }
}
