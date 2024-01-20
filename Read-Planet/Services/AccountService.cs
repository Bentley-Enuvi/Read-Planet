using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Read_Planet.Data;
using Read_Planet.Models;
using Read_Planet.Models.DTOs;
using Read_Planet.Models.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Read_Planet.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ReadPlanetDbContext _context;
        public AccountService(IConfiguration configuration, 
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ReadPlanetDbContext context)
        {
            _config = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        public string GenerateJWT()
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var securityToken = new JwtSecurityToken(expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config.GetSection("Jwt:LifeSpan").Value)));
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return token;
        }


        public async Task<Result<AppUserDto>> SignUp(RegistrationRequestDto regRequestDto)
        {
            AppUser user = new()
            {
                UserName = regRequestDto.Email,
                Email = regRequestDto.Email,
                NormalizedEmail = regRequestDto.Email.ToUpper(),
                FirstName = regRequestDto.FirstName,
                LastName = regRequestDto.LastName,
                Password = regRequestDto.Password,
            };

            var result = await _userManager.CreateAsync(user, regRequestDto.Password);

            if (!result.Succeeded)
            {
                return Result.Failure<AppUserDto>(
                    result.Errors.Select(e => new Error(e.Code, e.Description))
                );

            }

            //var userToReturn = _context.AppUsers.First(u => u.UserName == regRequestDto.Email);

            await AssignRole(user.Email, RolesConstants.User);
            var roles = await _userManager.GetRolesAsync(user);

            var userDto = new AppUserDto
            {
                ID = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.PasswordHash,
                RoleName = roles
            };

            return Result.Success(userDto);
        }


        public Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
        {
            throw new NotImplementedException();
        }


        public bool IsLoggedInAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

       

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }


        public Task<bool> AssignRole(string email, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<List<IdentityRole>> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress)
        {
            throw new NotImplementedException();
        }

        

        Task<bool> IAccountService.IsLoggedInAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
