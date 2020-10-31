using DataLayer.Models;
using DataLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLayer.Repositories.DefendWithEF
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ITitleRepository _titleRepository;

        public CustomerRepository()
        {
            _titleRepository = new DataLayer.Repositories.Attack.TitleRepository();
        }

        public Customer Get(int id)
        {
            return new Customer();
        }

        public List<Customer> GetAll()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            using (var context = new MyDbContext(optionsBuilder.Options))
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