using Common.Responses;

namespace Application.Client
{
    public interface IManageClient
    {
        Task<ResponseDto<object>> ReadClientJsonAsync(string? Url);
    }
}
