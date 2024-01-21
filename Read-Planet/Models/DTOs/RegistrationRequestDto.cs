using System.ComponentModel.DataAnnotations;

namespace Read_Planet.Models.DTOs
{
    public class RegistrationRequestDto
    {
        //[Required]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required]
        public string FirstName { get; set; }

        //[Required]
        public string UserName { get; set; }

        //[Required]
        public string LastName { get; set; }

        //[Required]
        public string Password { get; set; }
    }
}
