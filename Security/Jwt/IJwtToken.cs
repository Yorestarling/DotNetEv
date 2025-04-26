using Common.Dtos;

namespace Security.Jwt
{
    public interface IJwtToken
    {
        object GenerateJWT(UsersDto users);
    }
}
