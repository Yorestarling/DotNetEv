using Application.Users;
using Common.Dtos;
using Common.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Manage User Information
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class UsersController(IManageUser manageUser) : ControllerBase
    {
        private readonly IManageUser _manageUser = manageUser;

        /// <summary>
        /// Create New User
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        /// <remarks>
        /// Ejemplo de información del usuario:
        /// {
        ///   "name": "Yorestarling Mejia", 
        ///   "username": "yorestarling",
        ///   "email": "Yorestarlingdev@hotmail.com", 
        ///   "password": "Aleatorio*1324" 
        /// }
        /// </remarks>

        [HttpPost("CreateUser")]
        public async Task<ActionResult<ResponseDto<object>>> CreateUserAsync([FromBody] UsersDto users)
        => HttpStatusResponseUtils.HttpResponse(await _manageUser.CreateUser(users));

        /// <summary>
        /// Endpoint to validate user
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        [HttpPost("ValidateUser")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<object>>> ValidateUserAsync([FromBody] ValidateUserDto users)
        => HttpStatusResponseUtils.HttpResponse(await _manageUser.ValidateUser(users));
    }
}
