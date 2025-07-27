using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BoutiqueApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Smockerie.DTO;
using Smockerie.Enum;
using Smockerie.Services;

namespace Smockerie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        public record RegisterRequest(string Username, string Email, string Password);
        public record LoginRequest(string Username, string Password, string? TwoFactorCode = null, string? TwoFactorRecoveryCode = null);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest req)
        {
            if (await _userService.ValidateCredentialsAsync(req.Username, req.Password) != null)
                return BadRequest("Nom d'utilisateur déjà pris");
            // à toi d'ajouter aussi la vérif d'email unique si besoin

            var user = await _userService.RegisterAsync(req.Username, req.Email, req.Password);
            return BuildTokenResponse(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _userService.ValidateCredentialsAsync(req.Username, req.Password);
            if (user is null)
                return Unauthorized("Identifiants invalides");

            return BuildTokenResponse(user);
        }

        private IActionResult BuildTokenResponse(User user)
        {
            var key = _config["Jwt:Key"]!;
            var issuer = _config["Jwt:Issuer"]!;
            var audience = _config["Jwt:Audience"]!;
            var expires = int.Parse(_config["Jwt:ExpiresInMinutes"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,       user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role,                   user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,        Guid.NewGuid().ToString())
            };

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256
            );

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expires),
                signingCredentials: creds
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

            var userDto = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Ok(new
            {
                token = tokenString,
                expiresIn = expires * 60,
                user = userDto
            });
        }
    }
}
