﻿namespace ARS.Models;

public class UserModel : BaseModel {
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Name { get; set; } = null!;
    public byte Agents { get; set; }
    public RoleType Role { get; set; }

    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static bool VerifyPassword(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(password, passwordHash);
}

public class UserLoginDto {
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class UserRegisterDto {
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;

    public UserModel ToUserModel() {
        return new UserModel {
            Name = Name,
            Username = Username,
            PasswordHash = UserModel.HashPassword(Password)
        };
    }
}

public class UserViewDto {
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public RoleType Role { get; set; }
}

public enum AgentType {
    Resource = 0b0001,
    Consumer = 0b0010,
    Informer = 0b0100,
    Allocator = 0b1000
}

public enum RoleType {
    Admin = 1,
    User = 2
}