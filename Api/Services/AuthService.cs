using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public class AuthService(IConfiguration configuration) : IAuthService
{
    public object GetToken(string username, string password)
    {
        if (username.Equals("Admin") && password.Equals("Admin"))
        {
            return CreateToken();
        }

        return "Usuario Invalido";
    }
    
    private Token CreateToken()
    {
        var key = Encoding.ASCII.GetBytes(configuration["Authentication:Key"]);

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, "Admin"),
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);

        return new Token(tokenHandler.WriteToken(token));
    }
}