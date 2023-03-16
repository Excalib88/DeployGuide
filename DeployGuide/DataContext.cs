using DeployGuide.Models;
using Microsoft.EntityFrameworkCore;

namespace DeployGuide;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<UserEntity> Users { get; set; }
}