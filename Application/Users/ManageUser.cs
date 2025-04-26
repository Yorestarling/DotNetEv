using Common.Dtos;
using Common.Responses;
using Common.Models;
using EF.Context;
using Security.Jwt;
using Microsoft.EntityFrameworkCore;

namespace Application.Users
{
    public class ManageUser(DotNetDbContext context, IJwtToken tokenService) : IManageUser
    {
        private readonly DotNetDbContext _context = context;
        private readonly IJwtToken _tokenService = tokenService;

        public async Task<ResponseDto<object>> CreateUser(UsersDto usersDto)
        {
            try
            {
                bool UserExist = await _context.Users
                    .AnyAsync(u => u.Email == usersDto.Email);

                if (UserExist)
                    return new ResponseDto<object>()
                    { IsSuccess = false, Message = $"El correo {usersDto.Email} se encuentra registrado.", StatusCode = System.Net.HttpStatusCode.BadRequest };

                User user = new()
                {
                    FullName = usersDto.Name!,
                    Email = usersDto.Email!,
                    Password = BCrypt.Net.BCrypt.HashPassword(usersDto.Password),
                    UserName = usersDto.Username!,
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                object token = _tokenService.GenerateJWT(usersDto);

                return new ResponseDto<object>()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        Name = user.FullName,
                        Email = user.Email,
                        Guid = user.Id,
                        Token = token.ToString(),
                    },
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Usuario creado exitosamente."
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<object>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
