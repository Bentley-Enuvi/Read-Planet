using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Read_Planet.Data;
using Read_Planet.Models;
using Read_Planet.Models.DTOs;
using Read_Planet.Models.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Read_Planet.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ReadPlanetDbContext _context;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMessengerService _messengerService;
        public AccountService(IConfiguration configuration, 
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            ReadPlanetDbContext context,
            ITokenGenerator tokenGenerator,
            IMessengerService messengerService)
        {
            _config = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _tokenGenerator = tokenGenerator;
            _messengerService = messengerService;
        }


        //public string GenerateJWT()
        //{
        //    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value);
        //    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        //    var securityToken = new JwtSecurityToken(expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config.GetSection("Jwt:LifeSpan").Value)));
        //    var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        //    return token;
        //}


        public async Task<string> SignUp(RegistrationRequestDto regRequestDto)
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

            try
            {
                var result = await _userManager.CreateAsync(user, regRequestDto.Password);
                if (result.Succeeded)
                {
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
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {

            }
            return "Error Encountered";
        }


        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
        {
            var newUser = _context.AppUsers.FirstOrDefault(k => k.UserName.ToLower() == loginRequest.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(newUser, loginRequest.Password);

            if (newUser == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            //Generate JWT Token if user was found
            var roles = await _userManager.GetRolesAsync(newUser);
            var token = _tokenGenerator.GenerateToken(newUser, roles);

            AppUserDto userDTO = new()
            {
                Email = newUser.Email,
                ID = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                UserName = newUser.UserName,
                Password = newUser.PasswordHash
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token
            };

            return loginResponseDto;
        }

        public bool IsLoggedInAsync(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }

       

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.AppUsers.FirstOrDefault(g => g.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }


        public async Task<List<IdentityRole>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (roles == null) throw new Exception("You have no roles created yet");

            return roles;
        }


        public async Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = $"{confirmEmailAddress}?token={token}&email={user.Email}";
            var message = new Message("Confirmation email link", new List<string>() { user.Email },
                $"<a href=\"{confirmationLink}\">Click to confirm Confirmation email</a>");

            return await _messengerService.Send(message);
        }


        public async Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress)
        {
            try
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"{confirmEmailAddress}?token={token}&email={user.Email}";

                var message = new Message(
                    "Email Confirmation",
                    new List<string> { user.Email },
                    $"Dear {user.UserName},\n\nThank you for registering with us. Please confirm your email by clicking on the link: {confirmationLink}"
                );

                var sendResult = await _messengerService.Send(message);

                return sendResult;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return false;
            }
        }


        public async Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAddress)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var link = $"{resetPasswordAddress}?token={token}&email={user.Email}";
            var message = new Message("Reset Password link", new List<string>() { user.Email }, $"<a href=\"{link}\">Reset password</a>");

            return await _messengerService.Send(message);
        }

    }
}
