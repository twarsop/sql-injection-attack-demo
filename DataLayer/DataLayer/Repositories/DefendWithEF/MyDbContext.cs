using Microsoft.EntityFrameworkCore;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Repositories.DefendWithEF
{
    public class MyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Title> Titles { get; set; }
        private readonly string _connstr;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(this._connstr);

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { 
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();            
            _connstr = configuration.GetValue<string>("ConnStr");
        }

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