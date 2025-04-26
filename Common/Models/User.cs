using System.ComponentModel.DataAnnotations;

namespace Common.Models
{

    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(64)]
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [StringLength(32)]
        public string? UserName { get; set; } = string.Empty;
        [StringLength(128)]
        public string FullName { get; set; } = string.Empty;

    }
}
