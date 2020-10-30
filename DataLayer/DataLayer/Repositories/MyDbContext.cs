using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class MyDbContext : DbContext
    {
        public DbSet<DataLayer.Models.EF.Customer> Customers { get; set; }
        public DbSet<DataLayer.Models.EF.Title> Titles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");

            builder.Entity<DataLayer.Models.EF.Title>(entity => {
                entity.ToTable("titles");
                entity.HasKey(t => t.id);
            });

            builder.Entity<DataLayer.Models.EF.Customer>(entity => {
                entity.ToTable("customers");
                entity.HasKey(c => c.id);
            });            
        }
    }    
}