using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data_Context.Entities;
using DataContext.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryInterfaces.Login;
using ServiceInterfaces.Login;

namespace ServiceImplementation.Login
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _loginRepository;

        public LoginService(IConfiguration configuration, ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public string GenerateJwtToken(KritiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string AuthenticateUser(LoginDto loginDto)
        {
            var user = _loginRepository.AuthenticateUser(loginDto);

            if (user == null)
            {
                return null;
            }
            if (user.IsActive == false)
            {
                throw new UnauthorizedAccessException("Please activate your account.");
            }

            return GenerateJwtToken(user);
        }
    }
}