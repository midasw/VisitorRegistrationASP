using Microsoft.EntityFrameworkCore;
using VisitorRegistration_Models;

namespace VisitorRegistration.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company);

            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Registration>().ToTable("Registrations");
        }
    }
}
