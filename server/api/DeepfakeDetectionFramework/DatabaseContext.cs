using DeepfakeDetectionFramework.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepfakeDetectionFramework.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Method> Methods { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Response> Responses { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}
