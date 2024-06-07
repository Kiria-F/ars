using Microsoft.AspNetCore.Identity;

namespace ARS.Models;

public class User(string name) : IdentityUser<long> {
    public string Name { get; set; } = name;
}