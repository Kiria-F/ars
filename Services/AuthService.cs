using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ARS.Gateways;
using ARS.Models;
using ARS.Tools;
using Microsoft.IdentityModel.Tokens;

namespace ARS.Services;

public class AuthService(
    IConfiguration config,
    ILogger<AuthService> logger,
    UserGateway userGateway,
    TokenValidationParameters tokenValidationParameters,
    SessionService sessionService) {
    public Result<UserModel> Authenticate(UserLoginDto userLogin) {
        var user = userGateway.GetUserByUsername(userLogin.Username);
        if (user is null)
            return Result<UserModel>.Fail("User not found");
        if (!UserModel.VerifyPassword(userLogin.Password, user.PasswordHash))
            return Result<UserModel>.Fail("Wrong password");
        sessionService.OpenSession(user.Id);
        return Result.Ok(user);
    }

    public Result<UserModel> Authenticate(string token) {
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken;
        try {
            tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
        } catch (ArgumentNullException) {
            return Result<UserModel>.Fail("Missing token");
        } catch (SecurityTokenMalformedException) {
            return Result<UserModel>.Fail("Invalid token format");
        } catch (SecurityTokenExpiredException) {
            return Result<UserModel>.Fail("Token expired");
        } catch (SecurityTokenNoExpirationException) {
            return Result<UserModel>.Fail("Token has no expiration");
        } catch (Exception e) {
            logger.LogWarning(e.GetType().ToString());
            return Result<UserModel>.Fail("Invalid token");
        }
        var jwtTokenData = (JwtSecurityToken)validatedToken;
        var username = jwtTokenData.Claims.First(t => t.Type == "username").Value;
        var user = userGateway.GetUserByUsername(username);
        if (user is null) return Result<UserModel>.Fail("User not found");
        sessionService.OpenSession(user.Id);
        return Result.Ok(user);
    }

    public Result<UserModel> Register(UserRegisterDto userRegister) {
        var newUser = userRegister.ToUserModel();
        var foundUser = userGateway.GetUserByUsername(newUser.Username);
        if (foundUser is not null) return Result<UserModel>.Fail("User already exists");
        var addedEntity = userGateway.AddUser(newUser);
        sessionService.OpenSession(addedEntity.Id);
        return Result.Ok(addedEntity);
    }

    public string GenerateToken(string username) {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = new[] {
            new Claim("username", username)
        };
        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}