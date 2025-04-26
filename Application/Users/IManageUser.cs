using Common.Dtos;
using Common.Responses;

namespace Application.Users;

public interface IManageUser
{
    /// <summary>
    /// Method to create New User an return Object
    /// </summary>
    /// <param name="usersDto"></param>
    /// <returns></returns>
    Task<ResponseDto<object>> CreateUser(UsersDto usersDto);

    /// <summary>
    /// Method To validate Users an return Object
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    Task<ResponseDto<object>> ValidateUser(ValidateUserDto users);
}
