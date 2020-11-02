using DataLayer.Models;
using DataLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Repositories.DefendWithEF
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository() : base() { }

        public Customer Get(int id)
        {
            using (var context = this.GetContext())
            {
                return context.Customers.Include(c => c.Title).SingleOrDefault(c => c.Id == id);
            }
        }

        public List<Customer> GetAll()
        {
            using (var context = this.GetContext())
            {
                return context.Customers.Include(c => c.Title).ToList();
            }
        }

        public List<Customer> Search(Customer c)
        {
            return new List<Customer>();
        }

        public void Add(Customer c)
        {
            using (var context = this.GetContext())
            {
                c.TitleId = c.Title.Id;
                c.Title = null;
                context.Customers.Add(c);
                context.SaveChanges();
            }
        }

        public void Update(Customer c)
        {
            using (var context = this.GetContext())
            {
                c.TitleId = c.Title.Id;
                c.Title = null;
                context.Customers.Update(c);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = this.GetContext())
            {
                context.Remove(this.Get(id));
                context.SaveChanges();
            }
        }

        private CustomerDbContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
            optionsBuilder.UseNpgsql(this._connstr);
            return new CustomerDbContext(optionsBuilder.Options);
        }
    }
}