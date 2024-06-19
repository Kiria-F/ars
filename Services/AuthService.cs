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
        var user = context.Users.FirstOrDefault(u => u.Username == userLogin.Username);
        var foundPasswordHash = user?.PasswordHash;
        if (user is not null) {
            var res = UserModel.VerifyPassword(userLogin.Password, user.PasswordHash);
        }
        if (user is null || !UserModel.VerifyPassword(userLogin.Password, user.PasswordHash)) return null;
        return user;
    }

    public UserModel? Authenticate(string jwtToken) {
        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(jwtToken, tokenValidationParameters, out var validatedToken);
        var jwtTokenData = (JwtSecurityToken) validatedToken;
        var username = jwtTokenData.Claims.First(t => t.Type == "username").Value;
        return context.Users.FirstOrDefault(u => u.Username == username);
    }

    public UserModel Register(UserRegisterDto userRegister) {
        var user = userRegister.ToUserModel();
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