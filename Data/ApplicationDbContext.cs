using ARS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ARS.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User, UserRole, long>(options) {
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
};