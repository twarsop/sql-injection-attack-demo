using DataLayer.Models;
using DataLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Repositories.DefendWithEF
{
    public class TitleRepository : BaseRepository, ITitleRepository
    {
        public TitleRepository() : base() { }

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