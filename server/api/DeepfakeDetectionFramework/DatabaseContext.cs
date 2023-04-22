using DeepfakeDetectionFramework.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepfakeDetectionFramework;

public class DatabaseContext : DbContext
{
    public DbSet<Request> Requests { get; set; }
    public DbSet<Response> Responses { get; set; }
    public DbSet<Feedback> Feedback { get; set; }


    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}
