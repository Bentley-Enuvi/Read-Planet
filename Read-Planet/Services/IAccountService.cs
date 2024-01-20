using Microsoft.AspNetCore.Identity;
using Read_Planet.Models;
using Read_Planet.Models.DTOs;
using System.Security.Claims;

namespace Read_Planet.Services
{
    public interface IAccountService
    {
        public string GenerateJWT();

        Task<Result<AppUserDto>> SignUp(RegistrationRequestDto regRequestDto);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<List<IdentityRole>> GetAllRoles();

        Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress);

        Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAction);
        Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress);

        Task<bool> IsLoggedInAsync(ClaimsPrincipal user);
        Task LogoutAsync();
    }
}
