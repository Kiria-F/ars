using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ARS.Models;
using Microsoft.IdentityModel.Tokens;

namespace ARS.Services;

public class AuthService(IConfiguration config) {
    public UserModel? Authenticate(UserLoginDto userLogin) {
        return userLogin is { Username: "username", Password: "password" }
            ? new UserModel { Name = "name", Role = RoleType.Admin }
            : null;
    }
    
    public string GenerateToken(UserLoginDto userLogin) {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
            new Claim("username", userLogin.Username),
            new Claim("test", "some text")
        };
        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}