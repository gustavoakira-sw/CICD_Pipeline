using FiapApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FiapApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
}