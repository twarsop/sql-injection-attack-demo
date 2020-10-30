using DataLayer.Models;
using DataLayer.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLayer.Repositories
{
    public class CustomerRepositoryEF : ICustomerRepository
    {
        private readonly ITitleRepository _titleRepository;

        public CustomerRepositoryEF()
        {
            _titleRepository = new TitleRepository();
        }

        public Customer Get(int id)
        {
            return new Customer();
        }

        public List<Customer> GetAll()
        {
            List<Title> titles = _titleRepository.GetAll();
            var titleLookup = new Dictionary<int, Title>();
            foreach (Title title in titles)
            {
                titleLookup.Add(title.Id, title);
            }

            var customers = new List<Customer>();
            
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=postgres;Database=sqlinjectionattackdemo");
            using (var context = new MyDbContext(optionsBuilder.Options))
            {
                var customersEF = context.Customers.Include(c => c.Title).ToList();
                foreach (DataLayer.Models.Customer customer in customersEF)
                {
                    customers.Add(new Customer
                    {
                        Id = customer.Id,
                        Title = new Title{ Id = customer.Title.Id, Name = customer.Title.Name },
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        AddressLine1 = customer.AddressLine1,
                        AddressPostcode = customer.AddressPostcode
                    });
                }
            }

            return customers;
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