using Microsoft.AspNetCore.Mvc;
using Read_Planet.Models.DTOs;
using Read_Planet.Models;
using Read_Planet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Read_Planet.Controllers
{
    [ApiController]
    [Route("api/acct")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountService accountService, 
            IMapper mapper, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegistrationRequestDto regRequest)
        {
            var registerResult = await _accountService.SignUp(regRequest);

            if (registerResult.IsFailure)
            {
                return BadRequest(ResponseObjectDto<object>.Failure(registerResult.Errors));
            }

            //Add Token to verify the email
            var user = _mapper.Map<AppUser>(registerResult.Data);
            var appUrl = $"{Request.Scheme}://{Request.Host}";
            var confirmEmailEndpoint = $"{appUrl}/confirmemail";

            // Assuming SendConfirmationEmailAsync2 returns a boolean
            var confirmationEmailSent = await _accountService.SendConfirmationEmailAsync2(user, confirmEmailEndpoint);

            if (!confirmationEmailSent)
            {
                // Handle the case where email confirmation fails
                var errorResponse = ResponseObjectDto<object>.Failure(registerResult.Errors);
                return BadRequest(errorResponse);
            }

            return Ok(ResponseObjectDto<object>.Success(registerResult.Data));
        }
    }
}
