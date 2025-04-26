using Application.Client;
using Common.Dtos;
using Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manage User Information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController(IManageClient manageClient) : ControllerBase
    {

        private readonly IManageClient _manageClient = manageClient;

        /// <summary>
        /// Read External Url
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        [HttpGet("ReadAnyEndpoint")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<object>>> ReadAnyEndpointAsync()
        => HttpStatusResponseUtils.HttpResponse(await _manageClient.ReadClientJsonAsync());



        /// <summary>
        /// Post Data to External Url
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        [HttpPost("SendDataToExternalURL")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<object>>> PostDataToExternalURLAsync([FromBody] JsonHolderDto? request)
        => HttpStatusResponseUtils.HttpResponse(await _manageClient.ClientJsonAsync(request));
    }
}
