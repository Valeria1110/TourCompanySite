using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Models;

public class TourContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=1234")
            .UseSnakeCaseNamingConvention()
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())).EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(p => p.IdOrder).ValueGeneratedOnAdd();
        modelBuilder.Entity<Schedule>().Property(p => p.IdS).ValueGeneratedOnAdd();
        modelBuilder.Entity<Tour>().Property(p => p.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>().HasOne(s => s.Schedule).WithMany(o => o.Orders);
        modelBuilder.Entity<Schedule>().HasOne(t => t.Tour).WithMany(sch => sch.Schedules);
    }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Tour> Tours { get; set; }
}