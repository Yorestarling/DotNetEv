using Common.Dtos;
using Common.Responses;

namespace Application.Users;

public interface IManageUser
{
    Task<ResponseDto<object>> CreateUser(UsersDto usersDto);
    Task<ResponseDto<object>> ValidateUser(ValidateUserDto users);
}
