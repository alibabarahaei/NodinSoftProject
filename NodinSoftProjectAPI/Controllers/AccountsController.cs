using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NodinSoftProject.Application.DTOs.Account;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Domain.Models.User;
using NodinSoftProjectAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NodinSoftProjectAPI.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        private readonly IConfigurationSection _jwtSettings;

        public AccountsController(IMapper mapper, IConfiguration configuration, IUserService userService)
        {
            _mapper = mapper;
            _jwtSettings = configuration.GetSection("JwtSettings");
            _userService = userService;
        }



        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserRegistrationModel userModel)
        {
            var user = _mapper.Map<ApplicationUser>(userModel);
            user.EmailConfirmed = true;
            var registerUserDTO = _mapper.Map<RegisterUserDTO>(userModel);
            var result = await _userService.RegisterUserAsync(registerUserDTO);
            if (!result.Succeeded)
            {
                return Ok(result.Errors);
            }
            await _userService.AddRoleWithEmailUserAsync(new AddRoleWithEmailUserDTO()
            {
                EmailUser = userModel.Email,
                Role = "AuthorizedUser"
            });

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel userModel)
        {
            var user = await _userService.GetUserWithEmailAsync(userModel.Email);
            var checkUserWithEmailDTO = new CheckUserWithEmailDTO()
            {
                Email = userModel.Email,
                Password = userModel.Password
            };
            if (user != null && await _userService.CheckUserWithEmailAsync(checkUserWithEmailDTO))
            {
                var signingCredentials = GetSigningCredentials();
                var claims = GetClaims(user);
                var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(token);
            }
            return Unauthorized("Invalid Authentication");

        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("validIssuer").Value,
                audience: _jwtSettings.GetSection("validAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var roles = await _userService.GetRolesWithEmailAsync(user.Email);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.Add(new Claim("Email", user.Email));
            return claims;
        }
    }
}
