using Common.AppSettingsConfig;
using Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Security.Jwt
{
    public class JwtToken(IOptions<General> options) : IJwtToken
    {
        private readonly General _settings = options.Value;

        //Basic Method to GenerateToken
        public object GenerateJWT(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = GetTokenDescriptor(user);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenValue = tokenHandler.WriteToken(token);
            return tokenValue;
        }

        //Creation de BytesKey
        public byte[] GetBytesJWTKey() => Encoding.ASCII.GetBytes(_settings?.JWTConfigurations?.SecretKey!);
        
        //Adding Claims in JwtInformation and expiration token date
        public SecurityTokenDescriptor GetTokenDescriptor(User user) => new()
        {
            Subject = new ClaimsIdentity([
                      new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                      new Claim(ClaimTypes.Name,user.UserName),
                      new Claim(ClaimTypes.Email,user.Email!)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_settings?.JWTConfigurations?.ExpirationInMinutes!)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(GetBytesJWTKey()), SecurityAlgorithms.HmacSha256Signature)
        };
    }
}
