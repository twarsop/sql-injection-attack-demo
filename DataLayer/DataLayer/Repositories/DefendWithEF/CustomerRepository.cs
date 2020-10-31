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
            return new Customer();
        }

        public List<Customer> GetAll()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
            optionsBuilder.UseNpgsql(this._connstr);
            using (var context = new CustomerDbContext(optionsBuilder.Options))
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
            
        }

        public void Update(Customer c)
        {
            
        }

        public void Delete(int id)
        {
            
        }
    }
}