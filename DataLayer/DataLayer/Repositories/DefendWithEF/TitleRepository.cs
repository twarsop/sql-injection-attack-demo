using DataLayer.Models;
using DataLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Repositories.DefendWithEF
{
    public class TitleRepository : ITitleRepository
    {
        private readonly string _connstr;

        public TitleRepository()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();            
            _connstr = configuration.GetValue<string>("ConnStr");
        }

        public List<Title> GetAll()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
            optionsBuilder.UseNpgsql(this._connstr);
            using (var context = new CustomerDbContext(optionsBuilder.Options))
            {
                return context.Titles.ToList();
            }
        }
    }
}