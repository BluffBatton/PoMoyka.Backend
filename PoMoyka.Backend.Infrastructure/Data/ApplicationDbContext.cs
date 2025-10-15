using Microsoft.EntityFrameworkCore;
using PoMoyka.Backend.Domain.Entities;

namespace PoMoyka.Backend.Infrastructure.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Statement> Statements { get; set; } 
        public DbSet<CenterService> CenterServices { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TypeService> TypeServices { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
