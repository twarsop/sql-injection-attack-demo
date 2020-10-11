using System.Collections.Generic;
using datalayer.models;

namespace datalayer.interfaces
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