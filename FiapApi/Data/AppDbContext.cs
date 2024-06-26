using FiapApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiapApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> User { get; set; }
    public DbSet<ColetaDeLixo> ColetaDeLixo { get; set; }
}