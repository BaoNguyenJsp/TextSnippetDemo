using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TextSnippetDemo.Application.Dtos;

namespace TextSnippetDemo.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public AuthService(IConfiguration configuration, UserManager<IdentityUser<int>> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<LoginDto> Login(string username, string password)
        {
            // To reduce complexity. This demo won't check on password
            var user = await _userManager.FindByEmailAsync(username);
            var roles = await _userManager.GetRolesAsync(user);

            return new LoginDto
            {
                JwtToken = GenerateJwtToken(user, roles),
                Roles = roles
            };
        }

        private string GenerateJwtToken(IdentityUser<int> user, IEnumerable<string> roles)
        {
            var claims = roles.Select(role => new Claim("Role", role)).ToList();
            claims.Add(new Claim("UserId", user.Id.ToString()));

            var jwtOptions = _configuration.GetSection("JwtOptions");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtOptions["ExpireDays"]));
            var token = new JwtSecurityToken(
                jwtOptions["Issuer"],
                jwtOptions["Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
