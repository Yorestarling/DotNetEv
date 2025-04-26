using System.ComponentModel.DataAnnotations;

namespace Common.Models
{

    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Hash { get; set; }

    }
}
