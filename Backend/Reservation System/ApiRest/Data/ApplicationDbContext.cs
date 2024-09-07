using ApiRest.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiRest.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuraciones adicionales
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Reservation>().ToTable("Reservations");
        modelBuilder.Entity<Customer>().ToTable("Customers");
        modelBuilder.Entity<Service>().ToTable("Services");
        
        
    }
}
