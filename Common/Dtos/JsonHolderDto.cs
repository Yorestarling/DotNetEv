using FluentValidation;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace Common.Dtos
{
    public class JsonHolderDto
    {
        public int UserId { get; set; } = new int();

        [JsonIgnore]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
    }

    public class JsonHolderValidation : AbstractValidator<JsonHolderDto>
    {
        public JsonHolderValidation(IOptions<JsonHolderDto> options)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("El campo de titulo no puede estar vacío.");
        }
    }
}
