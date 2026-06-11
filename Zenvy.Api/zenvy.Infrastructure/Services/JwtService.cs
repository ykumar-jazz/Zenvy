using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using zenvy.domain.Entities;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel;
using System.IdentityModel.Tokens.Jwt;
using zenvy.application.Interfaces.Services;

namespace zenvy.infrastructure.Service;
public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly IConfiguration _configuration = configuration;

    public string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration?["Jwt:Key"]??"");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("RoleId",user.RoleId.ToString())
        };

        var token =
            new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(7),
                claims: claims,
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}