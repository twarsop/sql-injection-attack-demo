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
                var customersEF = context.Customers.ToList();
                foreach (DataLayer.Models.EF.Customer customer in customersEF)
                {
                    customers.Add(new Customer
                    {
                        Id = customer.id,
                        Title = titleLookup[customer.titleid],
                        FirstName = customer.firstname,
                        LastName = customer.lastname,
                        AddressLine1 = customer.addressline1,
                        AddressPostcode = customer.addresspostcode
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