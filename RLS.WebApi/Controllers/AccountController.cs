using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RLS.BLL.DTOs.Users;
using RLS.BLL.Services.Contracts.Users;
using RLS.Domain.Users;
using RLS.WebApi.Configurations;
using RLS.WebApi.Extensions;
using RLS.WebApi.Models;
using RLS.WebApi.Models.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using RLS.Domain.Enums;

namespace RLS.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly AuthenticationConfiguration _configuration;

        private readonly IUserService _customerService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            AuthenticationConfiguration configuration,
            IUserService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _customerService = customerService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<JsonResultData> Login([FromBody] UserLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _customerService.GetUserByTermAsync(model.Email);
                var token = GenerateToken(user);

                var tokenResponse = new TokenResponseModel
                {
                    User = user,
                    AccessToken = token,
                    ExpiresIn = (int)TimeSpan.FromDays(1).TotalSeconds
                };

                return JsonResultData.Success(tokenResponse);
            }

            return JsonResultData.Error("Username or password isn't correct.");
        }

        [HttpPost]
        [Route("register")]
        public async Task<JsonResultData> Register([FromBody] UserRegistrationModel data)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = data.Email,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Role = Role.User
                };

                var result = await _userManager.CreateAsync(user, data.Password);

                if (result.Succeeded)
                {
                    return JsonResultData.Success();
                }
            }

            return JsonResultData.Error("ApiUser already exists.");
        }

        private string GenerateToken(GetUserDto user)
        {
            var claims = new[]
            {
                new Claim(nameof(Domain.Users.User.Id), user.Id),
                new Claim(nameof(Domain.Users.User.Email), user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(_configuration.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}