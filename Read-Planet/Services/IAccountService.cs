using Microsoft.AspNetCore.Identity;
using Read_Planet.Models;
using Read_Planet.Models.DTOs;
using System.Security.Claims;

namespace Read_Planet.Services
{
    public interface IAccountService
    {
        //public string GenerateJWT();

        Task<string> SignUp(RegistrationRequestDto regRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest);
        Task<bool> AssignRole(string email, string roleName);
        Task<List<IdentityRole>> GetAllRoles();

        Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress);

        Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAction);
        Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress);

        bool IsLoggedInAsync(ClaimsPrincipal user);
        Task LogoutAsync();
    }
}
