using Common.Dtos;
using Common.Responses;
using Common.Models;
using EF.Context;
using Security.Jwt;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Application.Users
{
    public class ManageUser(DotNetDbContext context, IJwtToken tokenService) : IManageUser
    {
        private readonly DotNetDbContext _context = context;
        private readonly IJwtToken _tokenService = tokenService;

        /// <summary>
        /// Implementation method to create User
        /// </summary>
        /// <param name="usersDto"></param>
        /// <returns></returns>
        public async Task<ResponseDto<object>> CreateUser(UsersDto usersDto)
        {
            try
            {

                //validate if user exist
                bool UserExist = await _context.Users
                    .AnyAsync(u => u.Email == usersDto.Email);

                if (UserExist)
                    return new ResponseDto<object>()
                    { IsSuccess = false, Message = $"El correo {usersDto.Email} se encuentra registrado.", StatusCode = System.Net.HttpStatusCode.BadRequest };


                //create user to save in database
                User user = new()
                {
                    FullName = usersDto.Name!,
                    Email = usersDto.Email!,
                    Password = BCrypt.Net.BCrypt.HashPassword(usersDto.Password),
                    UserName = usersDto.Username!,
                };


                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                //Method to get JWT Token
                object token = _tokenService.GenerateJWT(user);

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


        /// <summary>
        /// Implementation Methot to Validate User
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public async Task<ResponseDto<object>> ValidateUser(ValidateUserDto users)
        {
            try
            {
                //Find first user by email address
                User? userExist = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == users.Email);

                if (userExist == null)
                    return new ResponseDto<object>()
                    { IsSuccess = false, Message = $"El correo {users.Email} no se encuentra registrado.", StatusCode = System.Net.HttpStatusCode.BadRequest };

                //Verify password using Bcrypt
                if (!BCrypt.Net.BCrypt.Verify(users.Password, userExist?.Password))
                    return new ResponseDto<object>()
                    { IsSuccess = false, Message = $"La Contraseña suministrada no es la correcta.", StatusCode = System.Net.HttpStatusCode.BadRequest };


                object token = _tokenService.GenerateJWT(userExist!);

                return new ResponseDto<object>()
                {
                    IsSuccess = true,
                    Data = new
                    {
                        Token = token.ToString(),
                    },
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = "Usuario encontrado."
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
