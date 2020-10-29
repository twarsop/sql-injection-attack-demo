using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class MyDbContext : DbContext
    {
        public DbSet<DataLayer.Models.EF.Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");

            builder.Entity<DataLayer.Models.EF.Customer>(entity => {
                entity.ToTable("customers");
            });
        }
    }    
}