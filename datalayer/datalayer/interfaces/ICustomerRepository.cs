using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Interfaces
{
    public interface ICustomerRepository
    {
        Customer Get(int id);
        List<Customer> GetAll();
        List<Customer> Search(Customer c);
        void Add(Customer c);
        void Update(Customer c);
        void Delete(int id);
    }    
}