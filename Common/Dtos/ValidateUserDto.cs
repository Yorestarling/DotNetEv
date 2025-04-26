using Common.AppSettingsConfig;
using FluentValidation;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Common.Dtos
{
    public class ValidateUserDto
    {
        public string? Email { get; set; }
        public string?
            Password { get; set; }

    }

    public class ValidateUserValidation : AbstractValidator<ValidateUserDto>
    {
        public ValidateUserValidation(IOptions<General> options)
        {


            RuleFor(x => x.Email)
                .NotEmpty()
                .Matches(new Regex(options.Value?.RegularExpression?.Email!))
                .WithMessage("El correo electrónico no es válido.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(new Regex(options.Value?.RegularExpression?.Password!))
                .WithMessage("La contraseña debe tener mayúsculas, minúsculas,Números, símbolos y más de 8 caracteres ");

        }
    }
}
