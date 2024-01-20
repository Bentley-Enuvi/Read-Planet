using System.ComponentModel.DataAnnotations;

namespace Read_Planet.Models.DTOs
{
    public class LoginResponseDto
    {
        [Required]
        public AppUserDto User { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
