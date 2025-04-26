using Common.Dtos;
using Common.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Application.Client
{
    public interface IManageClient
    {
        Task<ResponseDto<object>> ReadClientJsonAsync();

        Task<ResponseDto<object>> ClientJsonAsync(JsonHolderDto post);
    }
}
