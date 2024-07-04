using ARS.Models;
using Microsoft.EntityFrameworkCore;

namespace ARS.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
    public DbSet<UserModel> Users { get; set; } = null!;
}