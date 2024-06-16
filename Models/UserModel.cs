namespace ARS.Models; 

public class UserModel : BaseModel {
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public byte Agents { get; set; }
    public RoleType Role { get; set; }
}

public class UserLoginDto {
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class UserRegisterDto {
    public string Username { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Password { get; set; } = null!;
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