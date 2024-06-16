using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ARS.Data;
using ARS.Models;
using Microsoft.IdentityModel.Tokens;

namespace ARS.Services;

public class AuthService(
    IConfiguration config,
    ApplicationDbContext context,
    TokenValidationParameters tokenValidationParameters) {
    public UserModel? Authenticate(UserLoginDto userLogin) {
        var user = context.Users.FirstOrDefault(u =>
            u.Username == userLogin.Username && u.Password == userLogin.Password);
        return user;
    }

    public UserModel? Authenticate(string jwtToken) {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out var validatedToken);
        var jwtTokenData = (JwtSecurityToken) validatedToken;
        var userId = int.Parse(jwtTokenData.Claims.First(t => t.Type == "Username").Value);
        return context.Users.FirstOrDefault(u => u.Id == userId);
    }

    public UserModel Register(UserRegisterDto userRegister) {
        var user = new UserModel {
            Name = userRegister.Name,
            Username = userRegister.Username,
            Password = userRegister.Password
        };
        var addedEntity = context.Users.Add(user);
        user.Id = addedEntity.Entity.Id;
        context.SaveChanges();
        return user;
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