using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class MyDbContext : DbContext
    {
        public DbSet<DataLayer.Models.Customer> Customers { get; set; }
        public DbSet<DataLayer.Models.Title> Titles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");

            builder.Entity<DataLayer.Models.Title>(entity => {
                entity.ToTable("titles");
                entity.HasKey(t => t.Id);
            });

            builder.Entity<DataLayer.Models.Customer>(entity => {
                entity.ToTable("customers");
                entity.HasKey(c => c.Id);
            });            
        }
    }    
}