using Common.Dtos;
using Common.Models;

namespace Security.Jwt
{
    public interface IJwtToken
    {
        object GenerateJWT(User users);
    }
}
