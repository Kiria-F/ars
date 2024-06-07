using ARS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ARS.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options)
    : IdentityDbContext<User, UserRole, long>(options);