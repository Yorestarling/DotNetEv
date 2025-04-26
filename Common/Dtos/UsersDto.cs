using FluentValidation;

namespace Common.Dtos
{
    public class UsersDto
    {
        public string? Name { get; set; }
        public required string Username { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }

    public class CreationUserValidation : AbstractValidator<UsersDto>
    {
        public CreationUserValidation()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("El campo de nombre no puede estar vacío.");

            RuleFor(x => x.Email)
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("El correo electrónico no es válido.");

            RuleFor(x => x.Password).NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$")
            .WithMessage("La contraseña debe tener mayúsculas, minúsculas,Números, símbolos y más de 8 caracteres ");

        }
    }
}
