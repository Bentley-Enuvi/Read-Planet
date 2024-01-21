using Microsoft.AspNetCore.Mvc;
using Read_Planet.Models.DTOs;
using Read_Planet.Models;
using Read_Planet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Azure;
using Microsoft.IdentityModel.Tokens;
using Read_Planet.Migrations;

namespace Read_Planet.Controllers
{
    [ApiController]
    [Route("api/acct")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        protected ResponseObjectDto _response;

        public AccountController(IAccountService accountService, 
            UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
            _response = new();
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegistrationRequestDto regRequest)
        {
            var errorMessage = await _accountService.SignUp(regRequest);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccessful = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }

            return Ok(_response);
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _accountService.LoginAsync(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccessful = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);

        }



        [HttpPost("assign-role")]
        public async Task<IActionResult> GiveRole([FromBody] AssignRoleDto model)
        {
            var assignRoleSuccessful = await _accountService.AssignRole(model.Email, model.RoleName.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccessful = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpGet("get-all-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _accountService.GetAllRoles();

            if (roles == null)
            {
                return BadRequest(new ResponseObjectDto
                {
                    Message = "Error",
                    StatusCode = 400
                });
            }

            return Ok(new ResponseObjectDto
            {
                StatusCode = 200,
                IsSuccessful = true
            });
            //return Ok(roles);
        }
    }
}
