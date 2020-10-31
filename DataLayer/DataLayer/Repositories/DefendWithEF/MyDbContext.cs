using Microsoft.EntityFrameworkCore;
using DataLayer.Models;

namespace DataLayer.Repositories.DefendWithEF
{
    public class MyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("public");

            builder.Entity<Title>(entity => {
                entity.ToTable("titles");
                entity.HasKey(t => t.Id);
            });

            builder.Entity<Customer>(entity => {
                entity.ToTable("customers");
                entity.HasKey(c => c.Id);
            });            
        }
    }    
}